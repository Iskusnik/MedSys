namespace MedSys
{
    partial class QueryBuilderMedSys
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryBuilderMedSys));
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonToExcel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comboBoxEqual1 = new System.Windows.Forms.ComboBox();
            this.comboBox1or_and2 = new System.Windows.Forms.ComboBox();
            this.textBoxField1 = new System.Windows.Forms.TextBox();
            this.comboBoxField1 = new System.Windows.Forms.ComboBox();
            this.comboBoxEntities = new System.Windows.Forms.ComboBox();
            this.comboBoxEqual3 = new System.Windows.Forms.ComboBox();
            this.comboBoxEqual2 = new System.Windows.Forms.ComboBox();
            this.textBoxField3 = new System.Windows.Forms.TextBox();
            this.comboBoxField3 = new System.Windows.Forms.ComboBox();
            this.comboBox12_and_or3 = new System.Windows.Forms.ComboBox();
            this.textBoxField2 = new System.Windows.Forms.TextBox();
            this.comboBoxField2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(11, 39);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(224, 23);
            this.buttonSearch.TabIndex = 30;
            this.buttonSearch.Text = "Поиск";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonToExcel
            // 
            this.buttonToExcel.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonToExcel.Image = ((System.Drawing.Image)(resources.GetObject("buttonToExcel.Image")));
            this.buttonToExcel.Location = new System.Drawing.Point(241, 12);
            this.buttonToExcel.Name = "buttonToExcel";
            this.buttonToExcel.Size = new System.Drawing.Size(50, 50);
            this.buttonToExcel.TabIndex = 29;
            this.buttonToExcel.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 165);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(553, 265);
            this.dataGridView1.TabIndex = 28;
            // 
            // comboBoxEqual1
            // 
            this.comboBoxEqual1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEqual1.FormattingEnabled = true;
            this.comboBoxEqual1.Items.AddRange(new object[] {
            "<",
            "<=",
            "=",
            ">=",
            ">"});
            this.comboBoxEqual1.Location = new System.Drawing.Point(239, 85);
            this.comboBoxEqual1.Name = "comboBoxEqual1";
            this.comboBoxEqual1.Size = new System.Drawing.Size(76, 21);
            this.comboBoxEqual1.TabIndex = 25;
            // 
            // comboBox1or_and2
            // 
            this.comboBox1or_and2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1or_and2.FormattingEnabled = true;
            this.comboBox1or_and2.Items.AddRange(new object[] {
            "И",
            "ИЛИ",
            "Не выбрано"});
            this.comboBox1or_and2.Location = new System.Drawing.Point(466, 84);
            this.comboBox1or_and2.Name = "comboBox1or_and2";
            this.comboBox1or_and2.Size = new System.Drawing.Size(76, 21);
            this.comboBox1or_and2.TabIndex = 19;
            this.comboBox1or_and2.SelectedIndexChanged += new System.EventHandler(this.comboBox1or_and2_SelectedIndexChanged);
            // 
            // textBoxField1
            // 
            this.textBoxField1.Location = new System.Drawing.Point(320, 86);
            this.textBoxField1.Name = "textBoxField1";
            this.textBoxField1.Size = new System.Drawing.Size(140, 20);
            this.textBoxField1.TabIndex = 18;
            // 
            // comboBoxField1
            // 
            this.comboBoxField1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxField1.FormattingEnabled = true;
            this.comboBoxField1.Location = new System.Drawing.Point(12, 84);
            this.comboBoxField1.Name = "comboBoxField1";
            this.comboBoxField1.Size = new System.Drawing.Size(221, 21);
            this.comboBoxField1.TabIndex = 17;
            this.comboBoxField1.SelectedIndexChanged += new System.EventHandler(this.comboBoxField1_SelectedIndexChanged);
            // 
            // comboBoxEntities
            // 
            this.comboBoxEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEntities.FormattingEnabled = true;
            this.comboBoxEntities.Items.AddRange(new object[] {
            "Все персональные данные",
            "Доктора",
            "Пациенты",
            "Медицинская карта",
            "Болезнь",
            "Запись на приём",
            "Запись врача",
            "Свободное время",
            "Занятое время",
            "Документы"});
            this.comboBoxEntities.Location = new System.Drawing.Point(12, 12);
            this.comboBoxEntities.Name = "comboBoxEntities";
            this.comboBoxEntities.Size = new System.Drawing.Size(221, 21);
            this.comboBoxEntities.TabIndex = 16;
            this.comboBoxEntities.SelectedIndexChanged += new System.EventHandler(this.comboBoxEntities_SelectedIndexChanged);
            // 
            // comboBoxEqual3
            // 
            this.comboBoxEqual3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEqual3.FormattingEnabled = true;
            this.comboBoxEqual3.Items.AddRange(new object[] {
            "<",
            "<=",
            "=",
            ">=",
            ">"});
            this.comboBoxEqual3.Location = new System.Drawing.Point(239, 137);
            this.comboBoxEqual3.Name = "comboBoxEqual3";
            this.comboBoxEqual3.Size = new System.Drawing.Size(76, 21);
            this.comboBoxEqual3.TabIndex = 37;
            // 
            // comboBoxEqual2
            // 
            this.comboBoxEqual2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEqual2.FormattingEnabled = true;
            this.comboBoxEqual2.Items.AddRange(new object[] {
            "<",
            "<=",
            "=",
            ">=",
            ">"});
            this.comboBoxEqual2.Location = new System.Drawing.Point(239, 110);
            this.comboBoxEqual2.Name = "comboBoxEqual2";
            this.comboBoxEqual2.Size = new System.Drawing.Size(76, 21);
            this.comboBoxEqual2.TabIndex = 36;
            // 
            // textBoxField3
            // 
            this.textBoxField3.Location = new System.Drawing.Point(320, 139);
            this.textBoxField3.Name = "textBoxField3";
            this.textBoxField3.Size = new System.Drawing.Size(141, 20);
            this.textBoxField3.TabIndex = 35;
            // 
            // comboBoxField3
            // 
            this.comboBoxField3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxField3.FormattingEnabled = true;
            this.comboBoxField3.Location = new System.Drawing.Point(12, 138);
            this.comboBoxField3.Name = "comboBoxField3";
            this.comboBoxField3.Size = new System.Drawing.Size(221, 21);
            this.comboBoxField3.TabIndex = 34;
            this.comboBoxField3.SelectedIndexChanged += new System.EventHandler(this.comboBoxField3_SelectedIndexChanged);
            // 
            // comboBox12_and_or3
            // 
            this.comboBox12_and_or3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox12_and_or3.FormattingEnabled = true;
            this.comboBox12_and_or3.Items.AddRange(new object[] {
            "И",
            "ИЛИ",
            "Не выбрано"});
            this.comboBox12_and_or3.Location = new System.Drawing.Point(466, 112);
            this.comboBox12_and_or3.Name = "comboBox12_and_or3";
            this.comboBox12_and_or3.Size = new System.Drawing.Size(76, 21);
            this.comboBox12_and_or3.TabIndex = 33;
            // 
            // textBoxField2
            // 
            this.textBoxField2.Location = new System.Drawing.Point(321, 112);
            this.textBoxField2.Name = "textBoxField2";
            this.textBoxField2.Size = new System.Drawing.Size(141, 20);
            this.textBoxField2.TabIndex = 32;
            // 
            // comboBoxField2
            // 
            this.comboBoxField2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxField2.FormattingEnabled = true;
            this.comboBoxField2.Location = new System.Drawing.Point(12, 111);
            this.comboBoxField2.Name = "comboBoxField2";
            this.comboBoxField2.Size = new System.Drawing.Size(221, 21);
            this.comboBoxField2.TabIndex = 31;
            this.comboBoxField2.SelectedIndexChanged += new System.EventHandler(this.comboBoxField2_SelectedIndexChanged);
            // 
            // QueryBuilderMedSys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 430);
            this.Controls.Add(this.comboBoxEqual3);
            this.Controls.Add(this.comboBoxEqual2);
            this.Controls.Add(this.textBoxField3);
            this.Controls.Add(this.comboBoxField3);
            this.Controls.Add(this.comboBox12_and_or3);
            this.Controls.Add(this.textBoxField2);
            this.Controls.Add(this.comboBoxField2);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.buttonToExcel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBoxEqual1);
            this.Controls.Add(this.comboBox1or_and2);
            this.Controls.Add(this.textBoxField1);
            this.Controls.Add(this.comboBoxField1);
            this.Controls.Add(this.comboBoxEntities);
            this.MinimumSize = new System.Drawing.Size(569, 469);
            this.Name = "QueryBuilderMedSys";
            this.Text = "Конструктор запросов";
            this.Load += new System.EventHandler(this.QueryBuilderMedSys_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonToExcel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBoxEqual1;
        private System.Windows.Forms.ComboBox comboBox1or_and2;
        private System.Windows.Forms.TextBox textBoxField1;
        private System.Windows.Forms.ComboBox comboBoxField1;
        private System.Windows.Forms.ComboBox comboBoxEntities;
        private System.Windows.Forms.ComboBox comboBoxEqual3;
        private System.Windows.Forms.ComboBox comboBoxEqual2;
        private System.Windows.Forms.TextBox textBoxField3;
        private System.Windows.Forms.ComboBox comboBoxField3;
        private System.Windows.Forms.ComboBox comboBox12_and_or3;
        private System.Windows.Forms.TextBox textBoxField2;
        private System.Windows.Forms.ComboBox comboBoxField2;
    }
}