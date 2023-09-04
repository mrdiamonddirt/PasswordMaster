using System;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace PasswordManager
{
    public partial class Form1 : Form
    {
        private Label infoLabel;
        private TextBox masterPasswordTextBox;
        private Button confirmButton;

        private static string ConfigFileName = "config.txt";
        private static string MasterPasswordFileName = "masterpassword.txt";

        public Form1()
        {
            InitializeComponent();
            InitializeComponents();
            InitializeEvents();

            if (!File.Exists(MasterPasswordFileName))
            {
                // Delete the password_data.csv file if either config.txt or masterpassword.txt exists
                if (File.Exists("password_data.csv"))
                {
                    try
                    {
                        File.Delete("password_data.csv");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting password_data.csv: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


            if (!File.Exists(ConfigFileName))
            {
                ShowInfo("Welcome to the Password Manager. You need to create a master password.");
                ShowMasterPasswordInput(true);
            }
            else
            {
                ShowInfo("Enter your master password to continue:");
                ShowMasterPasswordInput(true);
            }
        }

        private void InitializeComponents()
        {
            infoLabel = new Label
            {
                Location = new System.Drawing.Point(10, 10),
                AutoSize = true,
                Text = "Welcome to the Password Manager. You need to create a master password."
            };

            masterPasswordTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 40),
                Size = new System.Drawing.Size(200, 20),
                PasswordChar = '*',
                Visible = false
            };

            confirmButton = new Button
            {
                Location = new System.Drawing.Point(10, 70),
                Text = "Confirm",
                Visible = false
            };

            Controls.Add(infoLabel);
            Controls.Add(masterPasswordTextBox);
            Controls.Add(confirmButton);
        }

        private void InitializeEvents()
        {
            confirmButton.Click += ConfirmButton_Click;
            masterPasswordTextBox.KeyUp += MasterPasswordTextBox_KeyUp;
        }

        private void ShowInfo(string message)
        {
            infoLabel.Text = message;
        }

        private void ShowMasterPasswordInput(bool show)
        {
            masterPasswordTextBox.Visible = show;
            confirmButton.Visible = show;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            string enteredPassword = masterPasswordTextBox.Text.Trim(); // Trim input to remove leading/trailing spaces

            if (!File.Exists(ConfigFileName))
            {
                string masterPassword = enteredPassword;
                if (!string.IsNullOrEmpty(masterPassword))
                {
                    SaveMasterPassword(masterPassword);
                    File.WriteAllText(ConfigFileName, "masterpasswordset");
                    ShowInfo("Master password set successfully.");
                    ShowMasterPasswordInput(false);

                    // Open the PasswordManagementForm
                    PasswordManagementForm passwordManagementForm = new PasswordManagementForm();
                    passwordManagementForm.ShowDialog();

                    // Close the Form1 (password input form)
                    this.Hide();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please enter a valid master password.");
                }
            }
            else
            {
                if (VerifyMasterPassword(enteredPassword))
                {
                    ShowInfo("Master password is correct. Proceed with password management.");
                    ShowMasterPasswordInput(false);
                    // Continue with password management functionality
                    // Create and show the PasswordManagementForm
                    PasswordManagementForm passwordManagementForm = new PasswordManagementForm();
                    passwordManagementForm.ShowDialog();

                    // Close the Form1 (password input form)
                    this.Hide();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect master password. Please try again.");
                }
            }
        }

        private void MasterPasswordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConfirmButton_Click(sender, e);
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
