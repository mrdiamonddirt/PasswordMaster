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
        private TextBox confirmMasterPasswordTextBox;
        private Label confirmLabel;
        private Button confirmButton;

        private static string ConfigFileName = "config.txt";
        private static string MasterPasswordFileName = "masterpassword.txt";

        private bool creatingInitialMasterPassword = false; // Flag to track if creating the initial master password

        public Form1()
        {
            InitializeComponent();
            InitializeComponents();
            InitializeEvents();

        }

        private void InitializeComponents()
        {
            infoLabel = new Label
            {
                Location = new System.Drawing.Point(10, 10),
                AutoSize = true,
                Text = "Welcome to the Password Manager. \r\n You need to create a master password."
            };

            masterPasswordTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 40),
                Size = new System.Drawing.Size(200, 20),
                PasswordChar = '*',
                Visible = false
            };

            confirmMasterPasswordTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 100),
                Size = new System.Drawing.Size(200, 20),
                PasswordChar = '*',
                Visible = false
            };

            confirmLabel = new Label
            {
                Location = new System.Drawing.Point(10, 70),
                AutoSize = true,
                Text = "Confirm Master Password:",
                Visible = false
            };

            confirmButton = new Button
            {
                Location = new System.Drawing.Point(10,70),
                AutoSize = true,
                Text = "Confirm",
                Visible = false
            };

            Controls.Add(infoLabel);
            Controls.Add(masterPasswordTextBox);
            Controls.Add(confirmMasterPasswordTextBox);
            Controls.Add(confirmLabel);
            Controls.Add(confirmButton);


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
                creatingInitialMasterPassword = true; // Flag that we are creating the initial master password
            }


            if (!File.Exists(ConfigFileName))
            {
                ShowInfo("Welcome to the Password Manager. \r\nYou need to create a master password.");
                ShowMasterPasswordInput(true);
                confirmButton.Location = new System.Drawing.Point(10, 130);
            }
            else
            {
                ShowInfo("Enter your master password to continue:");
                ShowMasterPasswordInput(true);
                this.Size = new System.Drawing.Size(300, 150);
            }
        }

        private void InitializeEvents()
        {
            confirmButton.Click += ConfirmButton_Click;
            masterPasswordTextBox.KeyUp += MasterPasswordTextBox_KeyUp;
            confirmMasterPasswordTextBox.KeyUp += MasterPasswordTextBox_KeyUp;
        }

        private void ShowInfo(string message)
        {
            infoLabel.Text = message;
        }

        private void ShowMasterPasswordInput(bool show)
        {
            masterPasswordTextBox.Visible = show;
            if (creatingInitialMasterPassword) // Show the confirm password fields only when creating the initial master password
            {
                confirmMasterPasswordTextBox.Visible = show;
                confirmLabel.Visible = show;

            }
            confirmButton.Visible = show;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            string enteredPassword = masterPasswordTextBox.Text.Trim(); // Trim input to remove leading/trailing spaces
            string confirmedPassword = confirmMasterPasswordTextBox.Text.Trim();

            if (!File.Exists(ConfigFileName))
            {
                if (creatingInitialMasterPassword) // Check if creating the initial master password
                {
                    if (!string.IsNullOrEmpty(enteredPassword) && enteredPassword == confirmedPassword)
                    {
                        SaveMasterPassword(enteredPassword);
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
                        MessageBox.Show("Passwords do not match or are empty. Please enter a valid master password.");
                    }
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
