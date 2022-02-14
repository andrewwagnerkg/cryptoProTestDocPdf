using CryptoPro.Sharpei.Xml;
using iTextSharp.text.io;
using iTextSharp.text.pdf;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using System.Xml;

namespace FileSingerApp
{
    public partial class Form1 : Form
    {
        string ManifestHashAlgorithm = CPSignedXml.XmlDsigGost3411Url;
        string OfficeObjectID = "idOfficeObject";

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Выбор файла для подписи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog1.FileName;
            }
        }

        private X509Certificate2 FindSertificate()
        {
            X509Store store = new X509Store("My", StoreLocation.CurrentUser);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
            X509Certificate2Collection found = store.Certificates.Find(X509FindType.FindBySerialNumber, txtCertificcateDn.Text, true);
            if (found.Count == 0)
            {
                MessageBox.Show("Секретный ключ не найден.");
                return null;
            }
            if (found.Count > 1)
            {
                MessageBox.Show("Найдено более одного секретного ключа.");
                return null;
            }
            return found[0];
        }

        /// <summary>
        /// Подписать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSign_Click(object sender, EventArgs e)
        {
            var certificate = FindSertificate();
            if (certificate == null) return;
            try
            {
                using (Package package = Package.Open(txtFilePath.Text))
                {
                    SignAllParts(package, certificate);
                }
                MessageBox.Show(String.Format("Документ {0} успешно подписан на ключе {1}.", txtFilePath.Text, certificate.Subject));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SignAllParts(Package package, X509Certificate2 certificate)
        {
            string RT_OfficeDocument = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
            string SignatureID = "idPackageSignature";
            List<Uri> PartstobeSigned = new List<Uri>();
            List<PackageRelationshipSelector> SignableReleationships = new List<PackageRelationshipSelector>();

            foreach (PackageRelationship relationship in package.GetRelationshipsByType(RT_OfficeDocument))
            {
                CreateListOfSignableItems(relationship, PartstobeSigned, SignableReleationships);
            }

            PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(package);
            dsm.CertificateOption = CertificateEmbeddingOption.InSignaturePart;
            dsm.HashAlgorithm = ManifestHashAlgorithm;
            try
            {
                System.Security.Cryptography.Xml.DataObject officeObject = CreateOfficeObject(SignatureID, dsm.HashAlgorithm);
                Reference officeObjectReference = new Reference("#" + OfficeObjectID);
                var sgn = dsm.Sign(PartstobeSigned, certificate, SignableReleationships, SignatureID, new System.Security.Cryptography.Xml.DataObject[] { officeObject }, new Reference[] { officeObjectReference });
            }
            catch (CryptographicException ex)
            {
                throw;
            }
        }

        private void CreateListOfSignableItems(PackageRelationship relationship, List<Uri> partstobeSigned, List<PackageRelationshipSelector> signableReleationships)
        {
            // This function adds the releation to SignableReleationships. And then it gets the part based on the releationship. Parts URI gets added to the PartstobeSigned list.
            PackageRelationshipSelector selector = new PackageRelationshipSelector(relationship.SourceUri, PackageRelationshipSelectorType.Id, relationship.Id);
            signableReleationships.Add(selector);
            if (relationship.TargetMode == TargetMode.Internal)
            {
                PackagePart part = relationship.Package.GetPart(PackUriHelper.ResolvePartUri(relationship.SourceUri, relationship.TargetUri));
                if (partstobeSigned.Contains(part.Uri) == false)
                {
                    partstobeSigned.Add(part.Uri);
                    // GetRelationships Function: Returns a Collection Of all the releationships that are owned by the part.
                    foreach (PackageRelationship childRelationship in part.GetRelationships())
                    {
                        CreateListOfSignableItems(childRelationship, partstobeSigned, signableReleationships);
                    }
                }
            }
        }

        private System.Security.Cryptography.Xml.DataObject CreateOfficeObject(string signatureID, string hashAlgorithm)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(String.Format(
            "<OfficeObject>" +
                "<SignatureProperties xmlns=\"http://www.w3.org/2000/09/xmldsig#\">" +
                    "<SignatureProperty Id=\"idOfficeV1Details\" Target=\"{0}\">" +
                        "<SignatureInfoV1 xmlns=\"http://schemas.microsoft.com/office/2006/digsig\">" +
                          "<SetupID></SetupID>" +
                          "<ManifestHashAlgorithm>{1}</ManifestHashAlgorithm>" +
                          "<SignatureProviderId>{2}</SignatureProviderId>" +
                        "</SignatureInfoV1>" +
                    "</SignatureProperty>" +
                "</SignatureProperties>" +
            "</OfficeObject>", signatureID, hashAlgorithm, "{F5AC7D23-DA04-45F5-ABCB-38CE7A982553}"));
            System.Security.Cryptography.Xml.DataObject officeObject = new System.Security.Cryptography.Xml.DataObject();
            // do not change the order of the following two lines
            officeObject.LoadXml(document.DocumentElement); // resets ID
            officeObject.Id = OfficeObjectID; // required ID, do not change
            return officeObject;
        }

        /// <summary>
        /// Проверить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerify_Click(object sender, EventArgs e)
        {
            var resultString = string.Empty;
            // Открываем документ
            using (Package package = Package.Open(txtFilePath.Text))
            {
                PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(package);
                if (!dsm.IsSigned)
                {
                    MessageBox.Show("Документ не подписан.");
                    return;
                }
                int count = 1;
                foreach (PackageDigitalSignature pds in dsm.Signatures)
                {
                    resultString += String.Format("Подпись {0} на сертификате {1}.", count++, pds.Signer);
                    VerifyResult result = pds.Verify();
                    if (result == VerifyResult.Success)
                        resultString = "Подпись верна.";
                    else
                        resultString += String.Format("  подпись не верна:{0}", result);
                    X509Chain chain = new X509Chain();
                    if (chain.Build(new X509Certificate2(pds.Signer)))
                        resultString += "  Сертификат действителен";
                    else
                        resultString += "  Сертификат недействителен";
                }
                VerifyResult res = dsm.VerifySignatures(false);
                if (res != VerifyResult.Success)
                {
                    resultString = "Одна или несколько подписей неверны";
                }
            }
            MessageBox.Show(resultString);
            return;
        }

        private void btnSignPdf_Click(object sender, EventArgs e)
        {
            var certificate = FindSertificate();
            PdfReader reader = new PdfReader(txtFilePath.Text);
            PdfStamper st = PdfStamper.CreateSignature(reader, new FileStream(txtFilePath.Text + "_signed.pdf", FileMode.Create, FileAccess.Write), '\0');
            PdfSignatureAppearance sap = st.SignatureAppearance;

            // Загружаем сертификат в объект iTextSharp
            X509CertificateParser parser = new X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] {
                parser.ReadCertificate(certificate.RawData)
            };

            sap.Certificate = parser.ReadCertificate(certificate.RawData);
            sap.Reason = "I like to sign";
            sap.Location = "Universe";
            sap.Acro6Layers = true;

            //sap.Render = PdfSignatureAppearance.SignatureRender.NameAndDescription;
            sap.SignDate = DateTime.Now;

            // Выбираем подходящий тип фильтра
            PdfName filterName = new PdfName("CryptoPro PDF");

            // Создаем подпись
            PdfSignature dic = new PdfSignature(filterName, PdfName.ADBE_PKCS7_DETACHED);
            dic.Date = new PdfDate(sap.SignDate);
            dic.Name = "PdfPKCS7 signature";
            if (sap.Reason != null)
                dic.Reason = sap.Reason;
            if (sap.Location != null)
                dic.Location = sap.Location;
            sap.CryptoDictionary = dic;

            int intCSize = 4000;
            Dictionary<PdfName, int> hashtable = new Dictionary<PdfName, int>();
            hashtable[PdfName.CONTENTS] = intCSize * 2 + 2;
            sap.PreClose(hashtable);
            Stream s = sap.GetRangeStream();
            MemoryStream ss = new MemoryStream();
            int read = 0;
            byte[] buff = new byte[8192];
            while ((read = s.Read(buff, 0, 8192)) > 0)
            {
                ss.Write(buff, 0, read);
            }

            // Вычисляем подпись
            ContentInfo contentInfo = new ContentInfo(ss.ToArray());
            SignedCms signedCms = new SignedCms(contentInfo, true);
            CmsSigner cmsSigner = new CmsSigner(certificate);
            signedCms.ComputeSignature(cmsSigner, false);
            byte[] pk = signedCms.Encode();

            // Помещаем подпись в документ
            byte[] outc = new byte[intCSize];
            PdfDictionary dic2 = new PdfDictionary();
            Array.Copy(pk, 0, outc, 0, pk.Length);
            dic2.Put(PdfName.CONTENTS, new PdfString(outc).SetHexWriting(true));
            sap.Close(dic2);

            MessageBox.Show(String.Format("Документ {0} успешно подписан на ключе {1} => {2}.",
                txtFilePath.Text, certificate.Subject, txtFilePath.Text + "_signed.pdf"));

        }

        private void btnVerifyPdf_Click(object sender, EventArgs e)
        {
            // Открываем документ
            PdfReader reader = new PdfReader(txtFilePath.Text);

            // Получаем подписи из документа
            AcroFields af = reader.AcroFields;
            List<string> names = af.GetSignatureNames();
            foreach (string name in names)
            {
                string message = "Signature name: " + name;
                message += "\nSignature covers whole document: " + af.SignatureCoversWholeDocument(name);
                message += "\nDocument revision: " + af.GetRevision(name) + " of " + af.TotalRevisions;

                // Проверяем подпись
                PdfDictionary singleSignature = af.GetSignatureDictionary(name);
                PdfString asString1 = singleSignature.GetAsString(PdfName.CONTENTS);
                byte[] signatureBytes = asString1.GetOriginalBytes();

                RandomAccessFileOrArray safeFile = reader.SafeFile;

                PdfArray asArray = singleSignature.GetAsArray(PdfName.BYTERANGE);
                using (
                    Stream stream =
                        (Stream)
                        new RASInputStream(
                            new RandomAccessSourceFactory().CreateRanged(
                                safeFile.CreateSourceView(),
                                (IList<long>)asArray.AsLongArray())))
                {
                    using (MemoryStream ms = new MemoryStream((int)stream.Length))
                    {
                        stream.CopyTo(ms);
                        byte[] data = ms.GetBuffer();

                        // Создаем объект ContentInfo по сообщению.
                        // Это необходимо для создания объекта SignedCms.
                        ContentInfo contentInfo = new ContentInfo(data);

                        // Создаем SignedCms для декодирования и проверки.
                        SignedCms signedCms = new SignedCms(contentInfo, true);

                        // Декодируем подпись
                        signedCms.Decode(signatureBytes);

                        bool checkResult;

                        try
                        {
                            signedCms.CheckSignature(true);
                            checkResult = true;
                        }
                        catch (Exception)
                        {
                            checkResult = false;
                        }

                        message += "\nDocument modified: " + !checkResult;

                        foreach (var sinerInfo in signedCms.SignerInfos)
                        {
                            message += "\nCertificate " + sinerInfo.Certificate;
                            X509Certificate2 cert = signedCms.Certificates[0];
                            var isCapiValid = cert.Verify();
                            message += "\nCAPI Validation: " + isCapiValid.ToString();


                            foreach (var attribute in sinerInfo.SignedAttributes)
                            {
                                if (attribute.Oid.Value == "1.2.840.113549.1.9.5")
                                {
                                    Pkcs9SigningTime signingTime = attribute.Values[0] as Pkcs9SigningTime;
                                    message += "\nDate: " + signingTime.SigningTime;
                                }
                            }
                            message += "\n\n";
                        }

                        MessageBox.Show(message);
                    }
                }
            }
        }
    }
}
