namespace DVLD.Applications.TestAppointments
{
    partial class frmVisionTestAppointments
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dgvTestAppointmetsList = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.ctrlApplicationInfo1 = new DVLD.UserControls.ctrlApplicationInfo();
            this.btnAddTestAppointment = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestAppointmetsList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(280, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(398, 36);
            this.label1.TabIndex = 8;
            this.label1.Text = "Vision Test Appointments";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Vision_512;
            this.pictureBox1.Location = new System.Drawing.Point(366, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(249, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // dgvTestAppointmetsList
            // 
            this.dgvTestAppointmetsList.AllowUserToAddRows = false;
            this.dgvTestAppointmetsList.AllowUserToDeleteRows = false;
            this.dgvTestAppointmetsList.AllowUserToOrderColumns = true;
            this.dgvTestAppointmetsList.BackgroundColor = System.Drawing.Color.White;
            this.dgvTestAppointmetsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestAppointmetsList.Location = new System.Drawing.Point(26, 726);
            this.dgvTestAppointmetsList.MultiSelect = false;
            this.dgvTestAppointmetsList.Name = "dgvTestAppointmetsList";
            this.dgvTestAppointmetsList.ReadOnly = true;
            this.dgvTestAppointmetsList.RowHeadersWidth = 51;
            this.dgvTestAppointmetsList.RowTemplate.Height = 26;
            this.dgvTestAppointmetsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTestAppointmetsList.Size = new System.Drawing.Size(898, 167);
            this.dgvTestAppointmetsList.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::DVLD.Properties.Resources.Close_32;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Location = new System.Drawing.Point(804, 917);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 35);
            this.button1.TabIndex = 13;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(26, 923);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "# Records:";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblRecords.Location = new System.Drawing.Point(136, 923);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(21, 21);
            this.lblRecords.TabIndex = 14;
            this.lblRecords.Text = "0";
            // 
            // ctrlApplicationInfo1
            // 
            this.ctrlApplicationInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlApplicationInfo1.Location = new System.Drawing.Point(0, 290);
            this.ctrlApplicationInfo1.Name = "ctrlApplicationInfo1";
            this.ctrlApplicationInfo1.Size = new System.Drawing.Size(952, 400);
            this.ctrlApplicationInfo1.TabIndex = 15;
            // 
            // btnAddTestAppointment
            // 
            this.btnAddTestAppointment.BackgroundImage = global::DVLD.Properties.Resources.AddAppointment_32;
            this.btnAddTestAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddTestAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddTestAppointment.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnAddTestAppointment.Location = new System.Drawing.Point(892, 690);
            this.btnAddTestAppointment.Name = "btnAddTestAppointment";
            this.btnAddTestAppointment.Size = new System.Drawing.Size(32, 30);
            this.btnAddTestAppointment.TabIndex = 16;
            this.btnAddTestAppointment.UseVisualStyleBackColor = true;
            // 
            // frmVisionTestAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(958, 962);
            this.Controls.Add(this.btnAddTestAppointment);
            this.Controls.Add(this.ctrlApplicationInfo1);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.dgvTestAppointmetsList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVisionTestAppointments";
            this.ShowIcon = false;
            this.Text = "Vision Test Appointments";
            this.Load += new System.EventHandler(this.frmVisionTestAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestAppointmetsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvTestAppointmetsList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRecords;
        private UserControls.ctrlApplicationInfo ctrlApplicationInfo1;
        private System.Windows.Forms.Button btnAddTestAppointment;
    }
}