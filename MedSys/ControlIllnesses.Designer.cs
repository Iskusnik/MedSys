namespace MedSys
{
    partial class ControlIllnesses
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
            this.buttonDeleteIllness = new System.Windows.Forms.Button();
            this.buttonAddIllness = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonRemoveIllnesses = new System.Windows.Forms.Button();
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
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridView1.Location = new System.Drawing.Point(256, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(315, 352);
            this.dataGridView1.TabIndex = 2;
            // 
            // buttonDeleteIllness
            // 
            this.buttonDeleteIllness.Location = new System.Drawing.Point(6, 46);
            this.buttonDeleteIllness.Name = "buttonDeleteIllness";
            this.buttonDeleteIllness.Size = new System.Drawing.Size(226, 23);
            this.buttonDeleteIllness.TabIndex = 3;
            this.buttonDeleteIllness.Text = "Удалить болезнь";
            this.buttonDeleteIllness.UseVisualStyleBackColor = true;
            this.buttonDeleteIllness.Click += new System.EventHandler(this.buttonDeleteJob_Click);
            // 
            // buttonAddIllness
            // 
            this.buttonAddIllness.Location = new System.Drawing.Point(6, 164);
            this.buttonAddIllness.Name = "buttonAddIllness";
            this.buttonAddIllness.Size = new System.Drawing.Size(226, 23);
            this.buttonAddIllness.TabIndex = 4;
            this.buttonAddIllness.Text = "Добавить болезнь";
            this.buttonAddIllness.UseVisualStyleBackColor = true;
            this.buttonAddIllness.Click += new System.EventHandler(this.buttonAddJob_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxInfo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.comboBoxJobs);
            this.groupBox1.Controls.Add(this.buttonDeleteIllness);
            this.groupBox1.Controls.Add(this.buttonAddIllness);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 196);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Болезни";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Описание";
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(6, 138);
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(226, 20);
            this.textBoxInfo.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Название новой болезни";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(6, 99);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(226, 20);
            this.textBoxName.TabIndex = 5;
            // 
            // buttonRemoveIllnesses
            // 
            this.buttonRemoveIllnesses.Location = new System.Drawing.Point(12, 269);
            this.buttonRemoveIllnesses.Name = "buttonRemoveIllnesses";
            this.buttonRemoveIllnesses.Size = new System.Drawing.Size(226, 23);
            this.buttonRemoveIllnesses.TabIndex = 5;
            this.buttonRemoveIllnesses.Text = "Изменить болезни пациента";
            this.buttonRemoveIllnesses.UseVisualStyleBackColor = true;
            this.buttonRemoveIllnesses.Click += new System.EventHandler(this.buttonRemoveDocFromJob_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 253);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Пациент";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonRemoveIllnesses);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 352);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ControlIllnesses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 352);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ControlIllnesses";
            this.Text = "Управление болезнями";
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
        private System.Windows.Forms.Button buttonDeleteIllness;
        private System.Windows.Forms.Button buttonAddIllness;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonRemoveIllnesses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxInfo;
    }
}