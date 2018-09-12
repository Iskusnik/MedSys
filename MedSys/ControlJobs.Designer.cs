namespace MedSys
{
    partial class ControlJobs
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
            this.comboBoxJobs = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonDeleteJob = new System.Windows.Forms.Button();
            this.buttonAddJob = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNewJob = new System.Windows.Forms.TextBox();
            this.buttonRemoveDocFromJob = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxJobs
            // 
            this.comboBoxJobs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxJobs.FormattingEnabled = true;
            this.comboBoxJobs.Location = new System.Drawing.Point(6, 19);
            this.comboBoxJobs.Name = "comboBoxJobs";
            this.comboBoxJobs.Size = new System.Drawing.Size(226, 21);
            this.comboBoxJobs.TabIndex = 1;
            this.comboBoxJobs.SelectedIndexChanged += new System.EventHandler(this.comboBoxJobs_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(515, 257);
            this.dataGridView1.TabIndex = 2;
            // 
            // buttonDeleteJob
            // 
            this.buttonDeleteJob.Location = new System.Drawing.Point(6, 46);
            this.buttonDeleteJob.Name = "buttonDeleteJob";
            this.buttonDeleteJob.Size = new System.Drawing.Size(226, 23);
            this.buttonDeleteJob.TabIndex = 3;
            this.buttonDeleteJob.Text = "Удалить должность";
            this.buttonDeleteJob.UseVisualStyleBackColor = true;
            this.buttonDeleteJob.Click += new System.EventHandler(this.buttonDeleteJob_Click);
            // 
            // buttonAddJob
            // 
            this.buttonAddJob.Location = new System.Drawing.Point(6, 125);
            this.buttonAddJob.Name = "buttonAddJob";
            this.buttonAddJob.Size = new System.Drawing.Size(226, 23);
            this.buttonAddJob.TabIndex = 4;
            this.buttonAddJob.Text = "Новая должность";
            this.buttonAddJob.UseVisualStyleBackColor = true;
            this.buttonAddJob.Click += new System.EventHandler(this.buttonAddJob_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxNewJob);
            this.groupBox1.Controls.Add(this.comboBoxJobs);
            this.groupBox1.Controls.Add(this.buttonDeleteJob);
            this.groupBox1.Controls.Add(this.buttonAddJob);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 154);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Должности";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Название новой должности";
            // 
            // textBoxNewJob
            // 
            this.textBoxNewJob.Location = new System.Drawing.Point(6, 99);
            this.textBoxNewJob.Name = "textBoxNewJob";
            this.textBoxNewJob.Size = new System.Drawing.Size(226, 20);
            this.textBoxNewJob.TabIndex = 5;
            // 
            // buttonRemoveDocFromJob
            // 
            this.buttonRemoveDocFromJob.Location = new System.Drawing.Point(18, 222);
            this.buttonRemoveDocFromJob.Name = "buttonRemoveDocFromJob";
            this.buttonRemoveDocFromJob.Size = new System.Drawing.Size(226, 23);
            this.buttonRemoveDocFromJob.TabIndex = 5;
            this.buttonRemoveDocFromJob.Text = "Изменить должности врача";
            this.buttonRemoveDocFromJob.UseVisualStyleBackColor = true;
            this.buttonRemoveDocFromJob.Click += new System.EventHandler(this.buttonRemoveDocFromJob_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Врач";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonRemoveDocFromJob);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 257);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ControlJobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 257);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ControlJobs";
            this.Text = "Управление должностями";
            this.Load += new System.EventHandler(this.ChangeJobs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxJobs;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonDeleteJob;
        private System.Windows.Forms.Button buttonAddJob;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonRemoveDocFromJob;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxNewJob;
        private System.Windows.Forms.Label label2;
    }
}