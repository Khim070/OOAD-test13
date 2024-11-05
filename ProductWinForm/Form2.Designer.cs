namespace ProductWinForm
{
    partial class Form2
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
            btnSubmit = new Button();
            btnClear = new Button();
            label1 = new Label();
            txtCode = new TextBox();
            cboCat = new ComboBox();
            txtName = new TextBox();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(353, 25);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(75, 23);
            btnSubmit.TabIndex = 3;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(22, 25);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 2;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 63);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 4;
            label1.Text = "Code *";
            // 
            // txtCode
            // 
            txtCode.Location = new Point(12, 91);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(415, 23);
            txtCode.TabIndex = 5;
            // 
            // cboCat
            // 
            cboCat.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCat.FormattingEnabled = true;
            cboCat.Location = new Point(12, 234);
            cboCat.Name = "cboCat";
            cboCat.Size = new Size(180, 23);
            cboCat.TabIndex = 6;
            // 
            // txtName
            // 
            txtName.Location = new Point(12, 159);
            txtName.Name = "txtName";
            txtName.Size = new Size(415, 23);
            txtName.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 129);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 7;
            label2.Text = "Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 207);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 9;
            label3.Text = "Category";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(439, 310);
            Controls.Add(label3);
            Controls.Add(txtName);
            Controls.Add(label2);
            Controls.Add(cboCat);
            Controls.Add(txtCode);
            Controls.Add(label1);
            Controls.Add(btnSubmit);
            Controls.Add(btnClear);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSubmit;
        private Button btnClear;
        private Label label1;
        private TextBox txtCode;
        private ComboBox cboCat;
        private TextBox txtName;
        private Label label2;
        private Label label3;
    }
}