namespace PasswordManager
{
    partial class PasswordManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordManagementForm));
            dataGridView = new DataGridView();
            appSiteTextBox = new TextBox();
            passwordTextBox = new TextBox();
            usernameTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            AddButton = new Button();
            searchTextBox = new TextBox();
            label4 = new Label();
            button1 = new Button();
            deleteButton = new Button();
            button3 = new Button();
            RevealButton = new Button();
            label5 = new Label();
            showbutton = new Button();
            hidebutton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(35, 162);
            dataGridView.Name = "dataGridView";
            dataGridView.RowTemplate.Height = 25;
            dataGridView.ScrollBars = ScrollBars.Vertical;
            dataGridView.Size = new Size(599, 263);
            dataGridView.TabIndex = 0;
            dataGridView.CellContentClick += dataGridView1_CellContentClick;
            // 
            // appSiteTextBox
            // 
            appSiteTextBox.Location = new Point(35, 97);
            appSiteTextBox.Name = "appSiteTextBox";
            appSiteTextBox.Size = new Size(133, 23);
            appSiteTextBox.TabIndex = 1;
            appSiteTextBox.TextChanged += appSiteTextBox_TextChanged;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(388, 96);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(165, 23);
            passwordTextBox.TabIndex = 3;
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(194, 97);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(164, 23);
            usernameTextBox.TabIndex = 2;
            usernameTextBox.TextChanged += usernameTextBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(71, 79);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 4;
            label1.Text = "App/Site";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(216, 79);
            label2.Name = "label2";
            label2.Size = new Size(96, 15);
            label2.TabIndex = 5;
            label2.Text = "UserName/Email";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(429, 79);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 6;
            label3.Text = "Password";
            label3.Click += label3_Click;
            // 
            // AddButton
            // 
            AddButton.Location = new Point(579, 96);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(75, 23);
            AddButton.TabIndex = 7;
            AddButton.Text = "Add";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(534, 133);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(100, 23);
            searchTextBox.TabIndex = 8;
            searchTextBox.TextChanged += textBox1_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(486, 136);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 9;
            label4.Text = "Search";
            // 
            // button1
            // 
            button1.Location = new Point(640, 133);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 10;
            button1.Text = "Search";
            button1.UseVisualStyleBackColor = true;
            button1.Click += SearchButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(640, 162);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(75, 23);
            deleteButton.TabIndex = 11;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += DeleteButton_Click;
            // 
            // button3
            // 
            button3.Location = new Point(640, 191);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 12;
            button3.Text = "Copy";
            button3.UseVisualStyleBackColor = true;
            button3.Click += CopyButton_Click;
            // 
            // RevealButton
            // 
            RevealButton.Location = new Point(640, 220);
            RevealButton.Name = "RevealButton";
            RevealButton.Size = new Size(75, 23);
            RevealButton.TabIndex = 13;
            RevealButton.Text = "Reveal";
            RevealButton.UseVisualStyleBackColor = true;
            RevealButton.Click += RevealButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(194, 9);
            label5.Name = "label5";
            label5.Size = new Size(317, 50);
            label5.TabIndex = 14;
            label5.Text = "Password Master";
            label5.TextAlign = ContentAlignment.TopCenter;
            label5.Click += label5_Click;
            // 
            // showbutton
            // 
            showbutton.BackColor = Color.White;
            showbutton.BackgroundImage = (Image)resources.GetObject("showbutton.BackgroundImage");
            showbutton.BackgroundImageLayout = ImageLayout.Stretch;
            showbutton.FlatStyle = FlatStyle.Flat;
            showbutton.Location = new Point(548, 95);
            showbutton.Name = "showbutton";
            showbutton.Size = new Size(25, 25);
            showbutton.TabIndex = 15;
            showbutton.UseVisualStyleBackColor = false;
            showbutton.Click += RevealEnteredButton_Click;
            // 
            // hidebutton
            // 
            hidebutton.BackColor = Color.White;
            hidebutton.BackgroundImage = (Image)resources.GetObject("hidebutton.BackgroundImage");
            hidebutton.BackgroundImageLayout = ImageLayout.Stretch;
            hidebutton.FlatStyle = FlatStyle.Flat;
            hidebutton.Location = new Point(548, 95);
            hidebutton.Name = "hidebutton";
            hidebutton.Size = new Size(25, 25);
            hidebutton.TabIndex = 16;
            hidebutton.UseVisualStyleBackColor = false;
            hidebutton.Click += RevealEnteredButton_Click;
            // 
            // PasswordManagementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(731, 450);
            Controls.Add(showbutton);
            Controls.Add(label5);
            Controls.Add(RevealButton);
            Controls.Add(button3);
            Controls.Add(deleteButton);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(searchTextBox);
            Controls.Add(AddButton);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(usernameTextBox);
            Controls.Add(appSiteTextBox);
            Controls.Add(dataGridView);
            Controls.Add(hidebutton);
            Controls.Add(passwordTextBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PasswordManagementForm";
            Text = "Password Master";
            Load += PasswordManagementForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView;
        private TextBox appSiteTextBox;
        private TextBox passwordTextBox;
        private TextBox usernameTextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button AddButton;
        private TextBox searchTextBox;
        private Label label4;
        private Button button1;
        private Button deleteButton;
        private Button button3;
        private Button RevealButton;
        private Label label5;
        private Button showbutton;
        private Button hidebutton;
    }
}