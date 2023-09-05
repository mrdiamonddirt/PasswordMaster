namespace PasswordManager
{
    partial class PasswordGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordGenerator));
            LengthTextBox = new TextBox();
            IncludeSpecialCharsCheckBox = new CheckBox();
            SpecialCharsTextBox = new TextBox();
            GeneratedPasswordTextBox = new TextBox();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            showbutton = new Button();
            hidebutton = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // LengthTextBox
            // 
            LengthTextBox.Location = new Point(115, 24);
            LengthTextBox.Name = "LengthTextBox";
            LengthTextBox.Size = new Size(43, 23);
            LengthTextBox.TabIndex = 0;
            // 
            // IncludeSpecialCharsCheckBox
            // 
            IncludeSpecialCharsCheckBox.AutoSize = true;
            IncludeSpecialCharsCheckBox.Location = new Point(116, 53);
            IncludeSpecialCharsCheckBox.Name = "IncludeSpecialCharsCheckBox";
            IncludeSpecialCharsCheckBox.Size = new Size(164, 19);
            IncludeSpecialCharsCheckBox.TabIndex = 1;
            IncludeSpecialCharsCheckBox.Text = "Include Special Characters";
            IncludeSpecialCharsCheckBox.UseVisualStyleBackColor = true;
            // 
            // SpecialCharsTextBox
            // 
            SpecialCharsTextBox.Location = new Point(115, 78);
            SpecialCharsTextBox.Name = "SpecialCharsTextBox";
            SpecialCharsTextBox.Size = new Size(146, 23);
            SpecialCharsTextBox.TabIndex = 2;
            // 
            // GeneratedPasswordTextBox
            // 
            GeneratedPasswordTextBox.Location = new Point(12, 169);
            GeneratedPasswordTextBox.Name = "GeneratedPasswordTextBox";
            GeneratedPasswordTextBox.Size = new Size(164, 23);
            GeneratedPasswordTextBox.TabIndex = 3;
            GeneratedPasswordTextBox.TextChanged += GeneratedPasswordTextBox_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(128, 107);
            button1.Name = "button1";
            button1.Size = new Size(116, 23);
            button1.TabIndex = 4;
            button1.Text = "Generate Password";
            button1.UseVisualStyleBackColor = true;
            button1.Click += GenerateButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 27);
            label1.Name = "label1";
            label1.Size = new Size(97, 15);
            label1.TabIndex = 5;
            label1.Text = "Password Length";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 81);
            label2.Name = "label2";
            label2.Size = new Size(103, 15);
            label2.TabIndex = 6;
            label2.Text = "Special Characters";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(41, 151);
            label3.Name = "label3";
            label3.Size = new Size(114, 15);
            label3.TabIndex = 7;
            label3.Text = "Generated Password";
            // 
            // showbutton
            // 
            showbutton.BackColor = Color.White;
            showbutton.BackgroundImage = (Image)resources.GetObject("showbutton.BackgroundImage");
            showbutton.BackgroundImageLayout = ImageLayout.Stretch;
            showbutton.FlatStyle = FlatStyle.Flat;
            showbutton.Location = new Point(177, 168);
            showbutton.Name = "showbutton";
            showbutton.Size = new Size(25, 25);
            showbutton.TabIndex = 16;
            showbutton.UseVisualStyleBackColor = false;
            showbutton.Click += RevealGeneratedEnteredButton_Click;
            // 
            // hidebutton
            // 
            hidebutton.BackColor = Color.White;
            hidebutton.BackgroundImage = (Image)resources.GetObject("hidebutton.BackgroundImage");
            hidebutton.BackgroundImageLayout = ImageLayout.Stretch;
            hidebutton.FlatStyle = FlatStyle.Flat;
            hidebutton.Location = new Point(177, 168);
            hidebutton.Name = "hidebutton";
            hidebutton.Size = new Size(25, 25);
            hidebutton.TabIndex = 17;
            hidebutton.UseVisualStyleBackColor = false;
            hidebutton.Click += RevealGeneratedEnteredButton_Click;
            // 
            // button2
            // 
            button2.Location = new Point(208, 168);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 18;
            button2.Text = "Copy";
            button2.UseVisualStyleBackColor = true;
            button2.Click += CopyButton_Click;
            // 
            // PasswordGenerator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(317, 225);
            Controls.Add(button2);
            Controls.Add(showbutton);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(SpecialCharsTextBox);
            Controls.Add(IncludeSpecialCharsCheckBox);
            Controls.Add(LengthTextBox);
            Controls.Add(hidebutton);
            Controls.Add(GeneratedPasswordTextBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PasswordGenerator";
            Text = "Password Generator";
            Load += PasswordGenerator_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox LengthTextBox;
        private CheckBox IncludeSpecialCharsCheckBox;
        private TextBox SpecialCharsTextBox;
        private TextBox GeneratedPasswordTextBox;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button showbutton;
        private Button hidebutton;
        private Button button2;
    }
}