using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PasswordManager
{
    public partial class PasswordManagementForm : Form
    {
        private List<PasswordEntry> passwordEntries = new List<PasswordEntry>();
        private string filePath = "password_data.csv"; // Change the file path as needed

        public PasswordManagementForm()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadDataFromCSV(); // Load data from CSV when the form loads
            passwordTextBox.KeyUp += PasswordTextBox_KeyUp; // Add this line

            // Set the PasswordChar property to obfuscate the entered password
            passwordTextBox.PasswordChar = '*';
        }

        private void InitializeDataGridView()
        {
            // Create columns for DataGridView
            DataGridViewTextBoxColumn appSiteColumn = new DataGridViewTextBoxColumn();
            appSiteColumn.HeaderText = "App/Site";
            appSiteColumn.Name = "AppSite";

            DataGridViewTextBoxColumn usernameColumn = new DataGridViewTextBoxColumn();
            usernameColumn.HeaderText = "Username";
            usernameColumn.Name = "Username";

            DataGridViewTextBoxColumn passwordColumn = new DataGridViewTextBoxColumn();
            passwordColumn.HeaderText = "Password";
            passwordColumn.Name = "Password";

            dataGridView.Columns.AddRange(new DataGridViewColumn[] { appSiteColumn, usernameColumn, passwordColumn });

            // Calculate and set column widths
            int totalWidth = dataGridView.Width - SystemInformation.VerticalScrollBarWidth;
            int columnWidth = totalWidth / dataGridView.Columns.Count;

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.Width = columnWidth;
            }

            // Hide the header row
            dataGridView.ColumnHeadersVisible = true;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro; // Change the header background color if needed
            dataGridView.RowHeadersVisible = false; // Hide row headers

            // Disable sorting for all columns
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Make the data non-editable
            dataGridView.ReadOnly = true;

            // Handle the CellClick event to select the whole row
            dataGridView.CellClick += DataGridView_CellClick;
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a valid cell was clicked (not a header)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Select the whole row
                dataGridView.Rows[e.RowIndex].Selected = true;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string appSite = appSiteTextBox.Text;
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            if (!string.IsNullOrWhiteSpace(appSite) && !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                PasswordEntry entry = new PasswordEntry(appSite, username, password);
                passwordEntries.Add(entry);

                // Add the entry to the DataGridView
                dataGridView.Rows.Add(appSite, username, ObfuscatePassword(password));

                // Clear input fields
                appSiteTextBox.Clear();
                usernameTextBox.Clear();
                passwordTextBox.Clear();

                SaveDataToCSV(); // Save data to CSV after addition
            }
            else
            {
                MessageBox.Show("Please fill in all fields before adding an account.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Confirm deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete this account?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int selectedIndex = dataGridView.SelectedRows[0].Index;
                    dataGridView.Rows.RemoveAt(selectedIndex);
                    passwordEntries.RemoveAt(selectedIndex);
                    SaveDataToCSV(); // Update CSV after deletion
                }
            }
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            deleteButton.Visible = dataGridView.SelectedRows.Count > 0;
        }

        private bool passwordRevealed = false;

        private void RevealButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView.SelectedRows[0].Index;
                string password = passwordEntries[selectedIndex].Password;

                if (passwordRevealed)
                {
                    // Hide the password in the DataGridView
                    dataGridView.Rows[selectedIndex].Cells["Password"].Value = ObfuscatePassword(password);
                    RevealButton.Text = "Reveal";
                }
                else
                {
                    // Reveal the password in the DataGridView
                    dataGridView.Rows[selectedIndex].Cells["Password"].Value = password;
                    RevealButton.Text = "Hide";
                }

                passwordRevealed = !passwordRevealed;
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView.SelectedRows[0].Index;
                string password = passwordEntries[selectedIndex].Password;

                // Copy the password to the clipboard
                Clipboard.SetText(password);
            }
        }

        private string ObfuscatePassword(string password)
        {
            // Replace the password with asterisks for obfuscation
            return new string('*', password.Length);
        }

        private void LoadDataFromCSV()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length == 3)
                            {
                                string appSite = parts[0];
                                string username = parts[1];
                                string encryptedPassword = parts[2]; // Retrieve the encrypted password from CSV

                                // Decrypt the password
                                string password = Decrypt(encryptedPassword);

                                PasswordEntry entry = new PasswordEntry(appSite, username, password);
                                passwordEntries.Add(entry);

                                // Add the entry to the DataGridView
                                dataGridView.Rows.Add(appSite, username, ObfuscatePassword(password));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SaveDataToCSV()
        {
            try
            {
                // Serialize the passwordEntries list to CSV format with encrypted passwords
                string csvData = SerializePasswordEntriesWithEncryption();

                // Write the encrypted data to the CSV file
                File.WriteAllText(filePath, csvData);

                MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string SerializePasswordEntriesWithEncryption()
        {
            StringBuilder csvBuilder = new StringBuilder();

            foreach (PasswordEntry entry in passwordEntries)
            {
                // Encrypt the password before adding it to the CSV
                string encryptedPassword = Encrypt(entry.Password);
                csvBuilder.AppendLine($"{entry.AppSite},{entry.Username},{encryptedPassword}");
            }

            return csvBuilder.ToString();
        }

        private string Encrypt(string input)
        {
            // Replace these values with your own encryption key and initialization vector (IV)
            string encryptionKey = "YourEncryptionKey"; // Replace with your actual key
            string encryptionIV = "YourEncryptionIV";   // Replace with your actual IV

            // Ensure that the encryption key is of the correct size (128, 192, or 256 bits)
            // You can pad or truncate the key as needed to meet the size requirement
            int keySize = 256; // You can choose 128 or 192 if needed
            if (encryptionKey.Length < keySize / 8)
            {
                // Pad the key with zeros to reach the required size
                encryptionKey = encryptionKey.PadRight(keySize / 8, '\0');
            }
            else if (encryptionKey.Length > keySize / 8)
            {
                // Truncate the key if it's too long
                encryptionKey = encryptionKey.Substring(0, keySize / 8);
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(encryptionIV);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        private string Decrypt(string encryptedInput)
        {
            // Replace these values with your own encryption key and initialization vector (IV)
            string encryptionKey = "YourEncryptionKey"; // Replace with your actual key
            string encryptionIV = "YourEncryptionIV";   // Replace with your actual IV

            // Ensure that the encryption key is of the correct size (128, 192, or 256 bits)
            // You can pad or truncate the key as needed to meet the size requirement
            int keySize = 256; // You can choose 128 or 192 if needed

            // Pad or truncate the key to the correct size
            if (encryptionKey.Length < keySize / 8)
            {
                // Pad the key with zeros to reach the required size
                encryptionKey = encryptionKey.PadRight(keySize / 8, '\0');
            }
            else if (encryptionKey.Length > keySize / 8)
            {
                // Truncate the key if it's too long
                encryptionKey = encryptionKey.Substring(0, keySize / 8);
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(encryptionIV);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedInput)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();

            // Filter the DataGridView based on the search text
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                bool match = false;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchText))
                    {
                        match = true;
                        break;
                    }
                }
                row.Visible = match;
            }
        }

        private void PasswordManagementForm_Deactivate(object sender, EventArgs e)
        {
            // When the form loses focus, hide the password if it's revealed
            if (passwordRevealed)
            {
                int selectedIndex = dataGridView.SelectedRows[0].Index;
                string password = passwordEntries[selectedIndex].Password;

                // Hide the password in the DataGridView
                dataGridView.Rows[selectedIndex].Cells["Password"].Value = ObfuscatePassword(password);
                RevealButton.Text = "Reveal";
                passwordRevealed = false;
            }
        }

        private void PasswordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddButton_Click(sender, e);
            }
        }

        private void RevealEnteredButton_Click(object sender, EventArgs e)
        {
            // Toggle between displaying the obfuscated password and the actual password
            if (passwordTextBox.PasswordChar == '\0')
            {
                passwordTextBox.PasswordChar = '*'; // Obfuscate the password
                hidebutton.Visible = false;
                showbutton.Visible = true;
            }
            else
            {
                passwordTextBox.PasswordChar = '\0'; // Reveal the password
                hidebutton.Visible = true;
                showbutton.Visible = false;
            }
        }

        private void OpenPasswordGenerator_Click(object sender, EventArgs e)
        {
            // Open the PasswordGenerator
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            passwordGenerator.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordManagementForm_Load(object sender, EventArgs e)
        {

        }

        private void appSiteTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }

    public class PasswordEntry
    {
        public string AppSite { get; }
        public string Username { get; }
        public string Password { get; }

        public PasswordEntry(string appSite, string username, string password)
        {
            AppSite = appSite;
            Username = username;
            Password = password;
        }
    }
}
