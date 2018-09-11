namespace MedSys
{
    partial class ChangeDoctorsJobs
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
            this.listBoxFreeJobs = new System.Windows.Forms.ListBox();
            this.listBoxCurrentJobs = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxFreeJobs
            // 
            this.listBoxFreeJobs.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxFreeJobs.FormattingEnabled = true;
            this.listBoxFreeJobs.Location = new System.Drawing.Point(0, 0);
            this.listBoxFreeJobs.Name = "listBoxFreeJobs";
            this.listBoxFreeJobs.Size = new System.Drawing.Size(150, 277);
            this.listBoxFreeJobs.TabIndex = 0;
            // 
            // listBoxCurrentJobs
            // 
            this.listBoxCurrentJobs.Dock = System.Windows.Forms.DockStyle.Right;
            this.listBoxCurrentJobs.FormattingEnabled = true;
            this.listBoxCurrentJobs.Location = new System.Drawing.Point(372, 0);
            this.listBoxCurrentJobs.Name = "listBoxCurrentJobs";
            this.listBoxCurrentJobs.Size = new System.Drawing.Size(150, 277);
            this.listBoxCurrentJobs.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonDelete);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(150, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 277);
            this.panel1.TabIndex = 4;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDelete.Location = new System.Drawing.Point(0, 135);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(222, 142);
            this.buttonDelete.TabIndex = 5;
            this.buttonDelete.Text = "<-- Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(222, 135);
            this.button1.TabIndex = 4;
            this.button1.Text = "Добавить ->";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChangeDoctorsJobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 277);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listBoxCurrentJobs);
            this.Controls.Add(this.listBoxFreeJobs);
            this.Name = "ChangeDoctorsJobs";
            this.Text = "ChangeDoctorsJobs";
            this.Load += new System.EventHandler(this.ChangeDoctorsJobs_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxFreeJobs;
        private System.Windows.Forms.ListBox listBoxCurrentJobs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button button1;
    }
}