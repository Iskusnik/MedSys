namespace MedSys
{
    partial class Registr
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
            this.comboBoxBlood = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.comboBoxGender = new System.Windows.Forms.ComboBox();
            this.dateTimePickerBirthDate = new System.Windows.Forms.DateTimePicker();
            this.comboBoxDocType = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.labelBloodType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxEducation = new System.Windows.Forms.TextBox();
            this.labelEducation = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxInsurance = new System.Windows.Forms.TextBox();
            this.textBoxDocumentNum = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAdress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxBlood
            // 
            this.comboBoxBlood.AutoCompleteCustomSource.AddRange(new string[] {
            "1+",
            "2+",
            "3+",
            "4+",
            "1-",
            "2-",
            "3-",
            "4-",
            "Неизвестно"});
            this.comboBoxBlood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBlood.FormattingEnabled = true;
            this.comboBoxBlood.Items.AddRange(new object[] {
            "+1",
            "+2",
            "+3",
            "+4",
            "-1",
            "-2",
            "-3",
            "-4",
            "Неизвестно"});
            this.comboBoxBlood.Location = new System.Drawing.Point(154, 165);
            this.comboBoxBlood.Name = "comboBoxBlood";
            this.comboBoxBlood.Size = new System.Drawing.Size(223, 21);
            this.comboBoxBlood.TabIndex = 189;
            this.comboBoxBlood.TextChanged += new System.EventHandler(this.ChangedInfo);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 336);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 188;
            this.label2.Text = "Пароль";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(154, 333);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(223, 20);
            this.textBoxPassword.TabIndex = 187;
            this.textBoxPassword.Text = "2";
            this.textBoxPassword.TextChanged += new System.EventHandler(this.ChangedInfo);
            // 
            // comboBoxGender
            // 
            this.comboBoxGender.AutoCompleteCustomSource.AddRange(new string[] {
            "Мужской",
            "Женский"});
            this.comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGender.FormattingEnabled = true;
            this.comboBoxGender.Items.AddRange(new object[] {
            "Мужской",
            "Женский"});
            this.comboBoxGender.Location = new System.Drawing.Point(154, 61);
            this.comboBoxGender.Name = "comboBoxGender";
            this.comboBoxGender.Size = new System.Drawing.Size(223, 21);
            this.comboBoxGender.TabIndex = 186;
            this.comboBoxGender.TextChanged += new System.EventHandler(this.ChangedInfo);
            // 
            // dateTimePickerBirthDate
            // 
            this.dateTimePickerBirthDate.Location = new System.Drawing.Point(154, 87);
            this.dateTimePickerBirthDate.MinDate = new System.DateTime(1900, 5, 1, 0, 0, 0, 0);
            this.dateTimePickerBirthDate.Name = "dateTimePickerBirthDate";
            this.dateTimePickerBirthDate.Size = new System.Drawing.Size(223, 20);
            this.dateTimePickerBirthDate.TabIndex = 185;
            this.dateTimePickerBirthDate.ValueChanged += new System.EventHandler(this.ChangedInfo);
            // 
            // comboBoxDocType
            // 
            this.comboBoxDocType.AutoCompleteCustomSource.AddRange(new string[] {
            "Пасспорт РФ",
            "Временное удостоверение личности ",
            "Военный билет",
            "Свидетельство о рождении",
            "Дипломатический паспорт"});
            this.comboBoxDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDocType.FormattingEnabled = true;
            this.comboBoxDocType.Items.AddRange(new object[] {
            "Паспорт РФ",
            "Временное удостоверение личности",
            "Военный билет",
            "Свидетельство о рождении"});
            this.comboBoxDocType.Location = new System.Drawing.Point(154, 259);
            this.comboBoxDocType.Name = "comboBoxDocType";
            this.comboBoxDocType.Size = new System.Drawing.Size(223, 21);
            this.comboBoxDocType.TabIndex = 184;
            this.comboBoxDocType.TextChanged += new System.EventHandler(this.ChangedInfo);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(33, 359);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 183;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelBloodType
            // 
            this.labelBloodType.AutoSize = true;
            this.labelBloodType.Location = new System.Drawing.Point(30, 173);
            this.labelBloodType.Name = "labelBloodType";
            this.labelBloodType.Size = new System.Drawing.Size(75, 13);
            this.labelBloodType.TabIndex = 182;
            this.labelBloodType.Text = "Группа крови";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 181;
            this.label3.Text = "Номер полиса ОМС";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(154, 359);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(223, 23);
            this.button1.TabIndex = 180;
            this.button1.Text = "Зарегистрировать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxEducation
            // 
            this.textBoxEducation.Location = new System.Drawing.Point(154, 192);
            this.textBoxEducation.Name = "textBoxEducation";
            this.textBoxEducation.Size = new System.Drawing.Size(223, 20);
            this.textBoxEducation.TabIndex = 179;
            this.textBoxEducation.Text = "3454такое-то";
            this.textBoxEducation.TextChanged += new System.EventHandler(this.ChangedInfo);
            // 
            // labelEducation
            // 
            this.labelEducation.AutoSize = true;
            this.labelEducation.Location = new System.Drawing.Point(30, 195);
            this.labelEducation.Name = "labelEducation";
            this.labelEducation.Size = new System.Drawing.Size(75, 13);
            this.labelEducation.TabIndex = 178;
            this.labelEducation.Text = "Образование";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 13);
            this.label16.TabIndex = 176;
            this.label16.Text = "Общие сведения";
            // 
            // textBoxInsurance
            // 
            this.textBoxInsurance.Location = new System.Drawing.Point(154, 139);
            this.textBoxInsurance.Name = "textBoxInsurance";
            this.textBoxInsurance.Size = new System.Drawing.Size(223, 20);
            this.textBoxInsurance.TabIndex = 175;
            this.textBoxInsurance.Text = "2345678654321";
            this.textBoxInsurance.TextChanged += new System.EventHandler(this.ChangedInfo);
            // 
            // textBoxDocumentNum
            // 
            this.textBoxDocumentNum.Location = new System.Drawing.Point(154, 286);
            this.textBoxDocumentNum.Name = "textBoxDocumentNum";
            this.textBoxDocumentNum.Size = new System.Drawing.Size(223, 20);
            this.textBoxDocumentNum.TabIndex = 174;
            this.textBoxDocumentNum.Text = "12441424241";
            this.textBoxDocumentNum.TextChanged += new System.EventHandler(this.ChangedInfo);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(30, 289);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 13);
            this.label12.TabIndex = 173;
            this.label12.Text = "Номер документа";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 230);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 13);
            this.label11.TabIndex = 172;
            this.label11.Text = "Удостоверение личности:";
            this.label11.TextChanged += new System.EventHandler(this.ChangedInfo);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 262);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 13);
            this.label10.TabIndex = 171;
            this.label10.Text = "Вид документа";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 170;
            this.label7.Text = "Место жительства";
            // 
            // textBoxAdress
            // 
            this.textBoxAdress.Location = new System.Drawing.Point(154, 113);
            this.textBoxAdress.Name = "textBoxAdress";
            this.textBoxAdress.Size = new System.Drawing.Size(223, 20);
            this.textBoxAdress.TabIndex = 169;
            this.textBoxAdress.Text = "Домовая 5, 15";
            this.textBoxAdress.TextChanged += new System.EventHandler(this.ChangedInfo);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 168;
            this.label5.Text = "Дата рождения";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 167;
            this.label4.Text = "ФИО";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(154, 35);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(223, 20);
            this.textBoxName.TabIndex = 166;
            this.textBoxName.Text = "Кириенко Кирилл Кириллович";
            this.textBoxName.TextChanged += new System.EventHandler(this.ChangedInfo);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 165;
            this.label1.Text = "Пол";
            // 
            // Registr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 400);
            this.Controls.Add(this.comboBoxBlood);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.comboBoxGender);
            this.Controls.Add(this.dateTimePickerBirthDate);
            this.Controls.Add(this.comboBoxDocType);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelBloodType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxEducation);
            this.Controls.Add(this.labelEducation);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBoxInsurance);
            this.Controls.Add(this.textBoxDocumentNum);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxAdress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Registr";
            this.Text = "Регистрация нового";
            this.Load += new System.EventHandler(this.Registr_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ComboBox comboBoxBlood;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxPassword;
        public System.Windows.Forms.ComboBox comboBoxGender;
        private System.Windows.Forms.DateTimePicker dateTimePickerBirthDate;
        public System.Windows.Forms.ComboBox comboBoxDocType;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelBloodType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox textBoxEducation;
        private System.Windows.Forms.Label labelEducation;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox textBoxInsurance;
        public System.Windows.Forms.TextBox textBoxDocumentNum;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox textBoxAdress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
    }
}