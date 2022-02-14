
namespace FileSingerApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnSign = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.txtCertificcateDn = new System.Windows.Forms.TextBox();
            this.btnSignPdf = new System.Windows.Forms.Button();
            this.btnVerifyPdf = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(12, 12);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(254, 20);
            this.txtFilePath.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFile.Location = new System.Drawing.Point(12, 38);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(23, 23);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "Select file";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnSign
            // 
            this.btnSign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSign.Location = new System.Drawing.Point(12, 94);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(23, 23);
            this.btnSign.TabIndex = 3;
            this.btnSign.Text = "Sign doc";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerify.Location = new System.Drawing.Point(12, 123);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(23, 23);
            this.btnVerify.TabIndex = 4;
            this.btnVerify.Text = "Verify doc";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // txtCertificcateDn
            // 
            this.txtCertificcateDn.Location = new System.Drawing.Point(13, 68);
            this.txtCertificcateDn.Name = "txtCertificcateDn";
            this.txtCertificcateDn.Size = new System.Drawing.Size(253, 20);
            this.txtCertificcateDn.TabIndex = 5;
            this.txtCertificcateDn.Text = "580000DF99A330B9FB0993BFBA00000000DF99";
            // 
            // btnSignPdf
            // 
            this.btnSignPdf.Location = new System.Drawing.Point(273, 94);
            this.btnSignPdf.Name = "btnSignPdf";
            this.btnSignPdf.Size = new System.Drawing.Size(207, 23);
            this.btnSignPdf.TabIndex = 6;
            this.btnSignPdf.Text = "Sign pdf";
            this.btnSignPdf.UseVisualStyleBackColor = true;
            this.btnSignPdf.Click += new System.EventHandler(this.btnSignPdf_Click);
            // 
            // btnVerifyPdf
            // 
            this.btnVerifyPdf.Location = new System.Drawing.Point(273, 124);
            this.btnVerifyPdf.Name = "btnVerifyPdf";
            this.btnVerifyPdf.Size = new System.Drawing.Size(207, 23);
            this.btnVerifyPdf.TabIndex = 7;
            this.btnVerifyPdf.Text = "Verify pdf";
            this.btnVerifyPdf.UseVisualStyleBackColor = true;
            this.btnVerifyPdf.Click += new System.EventHandler(this.btnVerifyPdf_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(11, 38);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(255, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Select file";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(11, 94);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(255, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Sign doc";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(11, 123);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(255, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Verify doc";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 159);
            this.Controls.Add(this.btnVerifyPdf);
            this.Controls.Add(this.btnSignPdf);
            this.Controls.Add(this.txtCertificcateDn);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnSign);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtFilePath);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.TextBox txtCertificcateDn;
        private System.Windows.Forms.Button btnSignPdf;
        private System.Windows.Forms.Button btnVerifyPdf;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

