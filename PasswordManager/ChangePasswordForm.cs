using System;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace PasswordManager
{
    public partial class ChangePasswordForm : Form
    {
        private TextBox oldPasswordTextBox;
        private TextBox newPasswordTextBox;
        private TextBox confirmNewPasswordTextBox;
        private Label oldPasswordLabel;
        private Label newPasswordLabel;
        private Label confirmNewPasswordLabel;
        private Button confirmButton;

        private static string MasterPasswordFileName = "masterpassword.txt";

        public ChangePasswordForm()
        {
            InitializeComponent();
            InitializeComponents();
            InitializeEvents();
        }

        private void InitializeComponents()
        {
            oldPasswordLabel = new Label
            {
                Location = new System.Drawing.Point(10, 10),
                AutoSize = true,
                Text = "Enter Old Password:"
            };

            oldPasswordTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 40),
                Size = new System.Drawing.Size(200, 20),
                PasswordChar = '*'
            };

            newPasswordLabel = new Label
            {
                Location = new System.Drawing.Point(10, 70),
                AutoSize = true,
                Text = "Enter New Password:"
            };

            newPasswordTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 100),
                Size = new System.Drawing.Size(200, 20),
                PasswordChar = '*'
            };

            confirmNewPasswordLabel = new Label
            {
                Location = new System.Drawing.Point(10, 130),
                AutoSize = true,
                Text = "Confirm New Password:"
            };

            confirmNewPasswordTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 160),
                Size = new System.Drawing.Size(200, 20),
                PasswordChar = '*'
            };

            confirmButton = new Button
            {
                Location = new System.Drawing.Point(10, 190),
                Text = "Confirm"
            };

            Controls.Add(oldPasswordLabel);
            Controls.Add(oldPasswordTextBox);
            Controls.Add(newPasswordLabel);
            Controls.Add(newPasswordTextBox);
            Controls.Add(confirmNewPasswordLabel);
            Controls.Add(confirmNewPasswordTextBox);
            Controls.Add(confirmButton);
        }

        private void InitializeEvents()
        {
            confirmButton.Click += ConfirmButton_Click;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            string oldPassword = oldPasswordTextBox.Text.Trim();
            string newPassword = newPasswordTextBox.Text.Trim();
            string confirmNewPassword = confirmNewPasswordTextBox.Text.Trim();

            if (VerifyMasterPassword(oldPassword))
            {
                if (newPassword == confirmNewPassword)
                {
                    // Update the password (save the new password)
                    SaveMasterPassword(newPassword);

                    MessageBox.Show("Password changed successfully.");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("New password and confirm password do not match. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Old password is incorrect. Please try again.");
            }
        }

        private void SaveMasterPassword(string password)
        {
            string hashedPassword = HashPassword(password);
            File.WriteAllText(MasterPasswordFileName, hashedPassword);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }
        }

        private bool VerifyMasterPassword(string inputPassword)
        {
            string hashedPassword = File.ReadAllText(MasterPasswordFileName);
            string enteredPasswordHash = HashPassword(inputPassword);

            Debug.WriteLine(hashedPassword);
            Debug.WriteLine(enteredPasswordHash);

            return hashedPassword == enteredPasswordHash;
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
