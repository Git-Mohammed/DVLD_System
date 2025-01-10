namespace DVLD.UserControls
{
    partial class ctrlApplicationInfo
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlApplicationInfo));
            this.gbDLAInfo = new System.Windows.Forms.GroupBox();
            this.lLShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lblPassedTests = new System.Windows.Forms.Label();
            this.lblLicenseClass = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblDLA_ID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbMale = new System.Windows.Forms.PictureBox();
            this.ctrlApplicationBasicInfo1 = new DVLD.Applications.Controls.ctrlApplicationBasicInfo();
            this.gbDLAInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMale)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDLAInfo
            // 
            this.gbDLAInfo.BackColor = System.Drawing.Color.White;
            this.gbDLAInfo.Controls.Add(this.lLShowLicenseInfo);
            this.gbDLAInfo.Controls.Add(this.pictureBox1);
            this.gbDLAInfo.Controls.Add(this.label8);
            this.gbDLAInfo.Controls.Add(this.pictureBox4);
            this.gbDLAInfo.Controls.Add(this.lblPassedTests);
            this.gbDLAInfo.Controls.Add(this.lblLicenseClass);
            this.gbDLAInfo.Controls.Add(this.label2);
            this.gbDLAInfo.Controls.Add(this.pictureBox2);
            this.gbDLAInfo.Controls.Add(this.lblDLA_ID);
            this.gbDLAInfo.Controls.Add(this.label1);
            this.gbDLAInfo.Controls.Add(this.pbMale);
            this.gbDLAInfo.Location = new System.Drawing.Point(29, 5);
            this.gbDLAInfo.Name = "gbDLAInfo";
            this.gbDLAInfo.Size = new System.Drawing.Size(895, 138);
            this.gbDLAInfo.TabIndex = 2;
            this.gbDLAInfo.TabStop = false;
            this.gbDLAInfo.Text = "Driving License Application Info";
            // 
            // lLShowLicenseInfo
            // 
            this.lLShowLicenseInfo.AutoSize = true;
            this.lLShowLicenseInfo.Enabled = false;
            this.lLShowLicenseInfo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lLShowLicenseInfo.Location = new System.Drawing.Point(166, 60);
            this.lLShowLicenseInfo.Name = "lLShowLicenseInfo";
            this.lLShowLicenseInfo.Size = new System.Drawing.Size(144, 21);
            this.lLShowLicenseInfo.TabIndex = 31;
            this.lLShowLicenseInfo.TabStop = true;
            this.lLShowLicenseInfo.Text = "Show License Info";
            this.lLShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lLShowLicenseInfo_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(124, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(375, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 21);
            this.label8.TabIndex = 9;
            this.label8.Text = "Passed Tests:";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLD.Properties.Resources.PassedTests_32;
            this.pictureBox4.Location = new System.Drawing.Point(509, 52);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(36, 26);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 23;
            this.pictureBox4.TabStop = false;
            // 
            // lblPassedTests
            // 
            this.lblPassedTests.AutoSize = true;
            this.lblPassedTests.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblPassedTests.ForeColor = System.Drawing.Color.Black;
            this.lblPassedTests.Location = new System.Drawing.Point(561, 55);
            this.lblPassedTests.Name = "lblPassedTests";
            this.lblPassedTests.Size = new System.Drawing.Size(56, 21);
            this.lblPassedTests.TabIndex = 10;
            this.lblPassedTests.Text = "[???]";
            // 
            // lblLicenseClass
            // 
            this.lblLicenseClass.AutoSize = true;
            this.lblLicenseClass.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblLicenseClass.ForeColor = System.Drawing.Color.Black;
            this.lblLicenseClass.Location = new System.Drawing.Point(561, 20);
            this.lblLicenseClass.Name = "lblLicenseClass";
            this.lblLicenseClass.Size = new System.Drawing.Size(56, 21);
            this.lblLicenseClass.TabIndex = 10;
            this.lblLicenseClass.Text = "[???]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(320, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "Applied For License:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.License_Type_32;
            this.pictureBox2.Location = new System.Drawing.Point(509, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(36, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 25;
            this.pictureBox2.TabStop = false;
            // 
            // lblDLA_ID
            // 
            this.lblDLA_ID.AutoSize = true;
            this.lblDLA_ID.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblDLA_ID.Location = new System.Drawing.Point(164, 26);
            this.lblDLA_ID.Name = "lblDLA_ID";
            this.lblDLA_ID.Size = new System.Drawing.Size(50, 21);
            this.lblDLA_ID.TabIndex = 16;
            this.lblDLA_ID.Text = "????";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 21);
            this.label1.TabIndex = 16;
            this.label1.Text = "D.LApp ID:";
            // 
            // pbMale
            // 
            this.pbMale.Image = global::DVLD.Properties.Resources.LicenseView_400;
            this.pbMale.Location = new System.Drawing.Point(124, 55);
            this.pbMale.Name = "pbMale";
            this.pbMale.Size = new System.Drawing.Size(36, 26);
            this.pbMale.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMale.TabIndex = 27;
            this.pbMale.TabStop = false;
            // 
            // ctrlApplicationBasicInfo1
            // 
            this.ctrlApplicationBasicInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlApplicationBasicInfo1.Location = new System.Drawing.Point(29, 149);
            this.ctrlApplicationBasicInfo1.Name = "ctrlApplicationBasicInfo1";
            this.ctrlApplicationBasicInfo1.Size = new System.Drawing.Size(903, 240);
            this.ctrlApplicationBasicInfo1.TabIndex = 3;
            // 
            // ctrlApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ctrlApplicationBasicInfo1);
            this.Controls.Add(this.gbDLAInfo);
            this.Name = "ctrlApplicationInfo";
            this.Size = new System.Drawing.Size(952, 400);
            this.gbDLAInfo.ResumeLayout(false);
            this.gbDLAInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMale)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDLAInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lblLicenseClass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblDLA_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbMale;
        private System.Windows.Forms.LinkLabel lLShowLicenseInfo;
        private System.Windows.Forms.Label lblPassedTests;
        private Applications.Controls.ctrlApplicationBasicInfo ctrlApplicationBasicInfo1;
    }
}
