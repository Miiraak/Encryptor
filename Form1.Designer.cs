namespace Encryptor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBoxInput = new RichTextBox();
            richTextBoxOutput = new RichTextBox();
            buttonEncrypt = new Button();
            buttonDecrypt = new Button();
            comboBoxMenu = new ComboBox();
            helpProvider1 = new HelpProvider();
            textBoxJustKey = new TextBox();
            textBoxKey = new TextBox();
            textBoxIV = new TextBox();
            buttonCopy = new Button();
            buttonClear = new Button();
            SuspendLayout();
            // 
            // richTextBoxInput
            // 
            helpProvider1.SetHelpString(richTextBoxInput, "Entrez le texte à chiffrer ou déchiffrer ici.");
            richTextBoxInput.Location = new Point(12, 41);
            richTextBoxInput.Name = "richTextBoxInput";
            helpProvider1.SetShowHelp(richTextBoxInput, true);
            richTextBoxInput.Size = new Size(353, 96);
            richTextBoxInput.TabIndex = 0;
            richTextBoxInput.Text = "";
            // 
            // richTextBoxOutput
            // 
            helpProvider1.SetHelpString(richTextBoxOutput, "Résultat de votre chiffrement/déchiffrement.");
            richTextBoxOutput.Location = new Point(12, 143);
            richTextBoxOutput.Name = "richTextBoxOutput";
            richTextBoxOutput.ReadOnly = true;
            helpProvider1.SetShowHelp(richTextBoxOutput, true);
            richTextBoxOutput.Size = new Size(353, 96);
            richTextBoxOutput.TabIndex = 1;
            richTextBoxOutput.Text = "";
            // 
            // buttonEncrypt
            // 
            buttonEncrypt.Location = new Point(13, 270);
            buttonEncrypt.Name = "buttonEncrypt";
            buttonEncrypt.Size = new Size(75, 23);
            buttonEncrypt.TabIndex = 2;
            buttonEncrypt.Text = "Encrypt";
            buttonEncrypt.UseVisualStyleBackColor = true;
            buttonEncrypt.Click += buttonEncrypt_Click;
            // 
            // buttonDecrypt
            // 
            buttonDecrypt.Location = new Point(289, 270);
            buttonDecrypt.Name = "buttonDecrypt";
            buttonDecrypt.Size = new Size(75, 23);
            buttonDecrypt.TabIndex = 3;
            buttonDecrypt.Text = "Decrypt";
            buttonDecrypt.UseVisualStyleBackColor = true;
            buttonDecrypt.Click += buttonDecrypt_Click;
            // 
            // comboBoxMenu
            // 
            comboBoxMenu.FormattingEnabled = true;
            helpProvider1.SetHelpNavigator(comboBoxMenu, HelpNavigator.Topic);
            helpProvider1.SetHelpString(comboBoxMenu, "Choisissez le chiffrement que vous désirez utiliser.");
            comboBoxMenu.Items.AddRange(new object[] { "Code César - Simple", "AtBash - Simple", "Base64 - Simple", "Code Scytale - Simple (4char max)", "Code Rail Fence - Simple", "SHA-256 - Simple", "AES - Difficile", "Code Playfair - Moyen" });
            comboBoxMenu.Location = new Point(12, 12);
            comboBoxMenu.Name = "comboBoxMenu";
            helpProvider1.SetShowHelp(comboBoxMenu, true);
            comboBoxMenu.Size = new Size(186, 23);
            comboBoxMenu.TabIndex = 4;
            comboBoxMenu.SelectedIndexChanged += comboBoxMenu_SelectedIndexChanged;
            // 
            // textBoxJustKey
            // 
            helpProvider1.SetHelpString(textBoxJustKey, "Entrez votre clé de chiffrement ou valeur dépendament du chiffrement que vous avez choisi.");
            textBoxJustKey.Location = new Point(204, 12);
            textBoxJustKey.Name = "textBoxJustKey";
            textBoxJustKey.PlaceholderText = "Clé / Valeur";
            helpProvider1.SetShowHelp(textBoxJustKey, true);
            textBoxJustKey.Size = new Size(161, 23);
            textBoxJustKey.TabIndex = 6;
            textBoxJustKey.Visible = false;
            // 
            // textBoxKey
            // 
            helpProvider1.SetHelpString(textBoxKey, "Entrez votre clé de chiffrement ou valeur dépendament du chiffrement que vous avez choisi.");
            textBoxKey.Location = new Point(204, 12);
            textBoxKey.Name = "textBoxKey";
            textBoxKey.PlaceholderText = "Clé / Valeur";
            helpProvider1.SetShowHelp(textBoxKey, true);
            textBoxKey.Size = new Size(80, 23);
            textBoxKey.TabIndex = 8;
            textBoxKey.Visible = false;
            // 
            // textBoxIV
            // 
            helpProvider1.SetHelpString(textBoxIV, "Entrez votre clé de chiffrement ou valeur dépendament du chiffrement que vous avez choisi.");
            textBoxIV.Location = new Point(289, 12);
            textBoxIV.Name = "textBoxIV";
            textBoxIV.PlaceholderText = "IV";
            helpProvider1.SetShowHelp(textBoxIV, true);
            textBoxIV.Size = new Size(76, 23);
            textBoxIV.TabIndex = 9;
            textBoxIV.Visible = false;
            // 
            // buttonCopy
            // 
            buttonCopy.Location = new Point(105, 270);
            buttonCopy.Name = "buttonCopy";
            buttonCopy.Size = new Size(75, 23);
            buttonCopy.TabIndex = 5;
            buttonCopy.Text = "Copy";
            buttonCopy.UseVisualStyleBackColor = true;
            buttonCopy.Click += buttonCopy_Click;
            // 
            // buttonClear
            // 
            buttonClear.Location = new Point(197, 270);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(75, 23);
            buttonClear.TabIndex = 7;
            buttonClear.Text = "Clear";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(377, 305);
            Controls.Add(textBoxIV);
            Controls.Add(textBoxKey);
            Controls.Add(buttonClear);
            Controls.Add(textBoxJustKey);
            Controls.Add(buttonCopy);
            Controls.Add(comboBoxMenu);
            Controls.Add(buttonDecrypt);
            Controls.Add(buttonEncrypt);
            Controls.Add(richTextBoxOutput);
            Controls.Add(richTextBoxInput);
            Cursor = Cursors.Cross;
            HelpButton = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Encryptor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBoxInput;
        private RichTextBox richTextBoxOutput;
        private Button buttonEncrypt;
        private Button buttonDecrypt;
        private ComboBox comboBoxMenu;
        private HelpProvider helpProvider1;
        private Button buttonCopy;
        private TextBox textBoxJustKey;
        private Button buttonClear;
        private TextBox textBoxKey;
        private TextBox textBoxIV;
    }
}
