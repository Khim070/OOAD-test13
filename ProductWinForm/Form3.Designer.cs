namespace ProductWinForm
{
    partial class Form3
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
            label3 = new Label();
            txtName = new TextBox();
            label2 = new Label();
            cboCat = new ComboBox();
            txtKey = new TextBox();
            label1 = new Label();
            btnSubmit = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 204);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 17;
            label3.Text = "Category";
            // 
            // txtName
            // 
            txtName.Location = new Point(23, 156);
            txtName.Name = "txtName";
            txtName.Size = new Size(415, 23);
            txtName.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 126);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 15;
            label2.Text = "Name";
            // 
            // cboCat
            // 
            cboCat.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCat.FormattingEnabled = true;
            cboCat.Location = new Point(23, 231);
            cboCat.Name = "cboCat";
            cboCat.Size = new Size(180, 23);
            cboCat.TabIndex = 14;
            // 
            // txtKey
            // 
            txtKey.Location = new Point(23, 88);
            txtKey.Name = "txtKey";
            txtKey.Size = new Size(415, 23);
            txtKey.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 60);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 12;
            label1.Text = "ID/Code *";
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(364, 22);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(75, 23);
            btnSubmit.TabIndex = 11;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(33, 22);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 279);
            Controls.Add(label3);
            Controls.Add(txtName);
            Controls.Add(label2);
            Controls.Add(cboCat);
            Controls.Add(txtKey);
            Controls.Add(label1);
            Controls.Add(btnSubmit);
            Controls.Add(btnCancel);
            Name = "Form3";
            Text = "Form3";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private TextBox txtName;
        private Label label2;
        private ComboBox cboCat;
        private TextBox txtKey;
        private Label label1;
        private Button btnSubmit;
        private Button btnCancel;
    }
}