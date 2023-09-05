using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class PasswordGenerator : Form
    {
        public PasswordGenerator()
        {
            InitializeComponent();

            // Set default values for password length and special characters
            LengthTextBox.Text = "12"; // Default password length
            SpecialCharsTextBox.Text = "!@#$%^&*()_+[]{}|;:,.<>?"; // Default set of special characters

            // Set the PasswordChar property for obfuscation
            GeneratedPasswordTextBox.PasswordChar = '*';
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            int passwordLength;
            if (!int.TryParse(LengthTextBox.Text, out passwordLength) || passwordLength <= 0)
            {
                MessageBox.Show("Please enter a valid password length.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            if (IncludeSpecialCharsCheckBox.Checked)
            {
                allowedChars += SpecialCharsTextBox.Text;
            }

            string generatedPassword = GenerateRandomPassword(passwordLength, allowedChars);
            GeneratedPasswordTextBox.Text = generatedPassword;
        }

        private string GenerateRandomPassword(int length, string allowedChars)
        {
            Random random = new Random();
            StringBuilder password = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(allowedChars.Length);
                password.Append(allowedChars[index]);
            }

            return password.ToString();
        }

        private void RevealGeneratedEnteredButton_Click(object sender, EventArgs e)
        {
            // Toggle between displaying the obfuscated password and the actual password
            if (GeneratedPasswordTextBox.PasswordChar == '\0')
            {
                GeneratedPasswordTextBox.PasswordChar = '*'; // Obfuscate the password
                hidebutton.Visible = false;
                showbutton.Visible = true;
            }
            else
            {
                GeneratedPasswordTextBox.PasswordChar = '\0'; // Reveal the password
                hidebutton.Visible = true;
                showbutton.Visible = false;
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(GeneratedPasswordTextBox.Text))
            {
                Clipboard.SetText(GeneratedPasswordTextBox.Text);
                MessageBox.Show("Password copied to clipboard.", "Copy Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GeneratedPasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordGenerator_Load(object sender, EventArgs e)
        {

        }
    }
}
