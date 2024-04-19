using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Buffers.Text;
using System.Windows.Forms;
using System.Security.Cryptography.Xml;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualBasic;

namespace Encryptor
{
    public partial class Form1 : Form
    {
        char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };


        public Form1()
        {
            InitializeComponent();
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                switch (comboBoxMenu.SelectedIndex)
                {
                    case 0:
                        // code césar (not mine)
                        if (textBoxJustKey.Text == "" || textBoxJustKey.Text.Contains(@"[a-zA-Z]")) { AllErrors(0); }
                        else
                        {
                            int decalage = Convert.ToInt32(textBoxJustKey.Text);

                            // Fonction de chiffrage César
                            Func<int, int, int> mod = (val, m) => val % m + (val < 0 ? m : 0);

                            Func<char, char, int, char> decal =
                                (c, offset, m) => (char)(offset + mod(c - offset + decalage, m));

                            Func<char, char> cesar =
                             c => ('a' <= c && c <= 'z') ? decal(c, 'a', 26)
                                : ('A' <= c && c <= 'Z') ? decal(c, 'A', 26)
                                : ('0' <= c && c <= '9') ? decal(c, '0', 10)
                                : c;

                            richTextBoxOutput.Text = new string(richTextBoxInput.Text.Select(cesar).ToArray());
                        }
                        break;

                    case 1:
                        // Atbash
                        if (richTextBoxInput.Text == "") { AllErrors(1); }
                        else
                        {
                            string stringTemp = "";
                            int intTemp = 0;

                            foreach (char letter in richTextBoxInput.Text)
                            {
                                if (char.IsWhiteSpace(letter))
                                {
                                    stringTemp += ' ';
                                }
                                else if (
                                    letter == '0' || letter == '1' || letter == '2' || letter == '3' || letter == '4' ||
                                    letter == '5' || letter == '6' || letter == '7' || letter == '8' || letter == '9' ||
                                    letter == ',' || letter == '.' || letter == '-' || letter == '?' || letter == '!')
                                {
                                    stringTemp += letter;
                                }
                                else
                                {
                                    intTemp = Array.IndexOf(alphabet, letter);
                                    stringTemp += Convert.ToString(alphabet[25 - intTemp]);
                                }
                            }
                            richTextBoxOutput.Text = stringTemp;
                        }
                        break;

                    case 2:
                        // Base64
                        var plainTextBytes = Encoding.UTF8.GetBytes(richTextBoxInput.Text);
                        richTextBoxOutput.Text = Convert.ToBase64String(plainTextBytes);
                        break;

                    case -3:
                        // Code Scytale
                        int stringLength = richTextBoxInput.Text.Length;
                        string output = "";
                        bool alterne = true;
                        int i = 0; // nombre de ligne
                        int ii = 0; // nombre de char par ligne
                        int iii = 0; // itération foreach
                        int iTemp = 0; // Compteur de collone
                        int iiTemp = 0; // Compteur de lignes
                        int tableNav = 0;
                        int tableTempNav = 0;
                        int tempInc = 0;

                        while ((i * ii) < stringLength)
                        {
                            if (alterne) { i++; } else { ii++; }
                            alterne = !alterne;
                        }

                        char[] tableTemp = new char[i * ii];
                        char[] tableOutput = new char[stringLength];

                        foreach (char temp in richTextBoxInput.Text)
                        {
                            tableTemp[iii] = temp;
                            iii++;
                        }


                        while (iTemp < i)
                        {
                            tableNav = tempInc;
                            iiTemp = 0;

                            while (iiTemp < ii)
                            {
                                tableOutput[tableNav] = tableTemp[tableTempNav];
                                tableTempNav += i;
                                tableNav++;
                                tempInc++;
                                iiTemp++;

                                if (tableOutput.Last() is not '\0')
                                {
                                    ii = -1;
                                }

                            }
                            tableTempNav -= i + 1;
                            iTemp++;

                        }

                        foreach (char tempChar in tableOutput)
                        {
                            output += tempChar;
                        }

                        richTextBoxOutput.Text = output;
                        break;

                    case 3:
                        // Code Scytale (not mine)
                        // Retire les espaces et met en majuscule
                        string texteBrut = richTextBoxInput.Text.Replace(" ", "").ToUpper();

                        string ciphertxt = "";
                        //define number of columns
                        int keyScytale = Convert.ToInt32(textBoxJustKey.Text);
                        //define number of rows
                        int numOfRows = texteBrut.Length / keyScytale;
                        //keep track of where in the message we are
                        int index = 0;
                        //if the number of rows isn't square or cube add 1
                        if (numOfRows % 2 != 0 || numOfRows % 3 != 0) { numOfRows++; }
                        char[,] grid = new char[numOfRows, keyScytale];

                        //loop through row, col
                        for (int row = 0; row < numOfRows; row++)
                        {
                            for (int col = 0; col < keyScytale; col++)
                            {
                                //if the current position in the string less than the length
                                if (index < texteBrut.Length)
                                {
                                    grid[row, col] = texteBrut[index];
                                    index++;

                                }
                                else
                                {
                                    //else fill in the space with X
                                    grid[row, col] = 'X';


                                }
                            }

                        }
                        //build the cipher message
                        for (int col = 0; col < keyScytale; col++)
                        {

                            for (int rows = 0; rows < numOfRows; rows++)
                            {
                                ciphertxt += grid[rows, col];

                            }
                            //Uncomment the line below
                            //If you want a space after each row
                            //ciphertxt += " ";
                        }
                        richTextBoxOutput.Text = ciphertxt;
                        break;

                    case 4:
                        // Code Rail Fencer
                        string input = richTextBoxInput.Text;
                        int rail = Convert.ToInt32(textBoxJustKey.Text);
                        int increment = 1;
                        int noDeListe = -1;
                        bool isUp = true;
                        string tempString = "";

                        // Créez une liste de listes
                        List<List<char>> listeDeListes = new List<List<char>>();

                        // Boucle pour créer et ajouter "x" listes à la liste principale
                        for (int i4 = 0; i4 < rail; i4++)
                        {
                            List<char> nouvelleListe = new List<char>();
                            listeDeListes.Add(nouvelleListe);
                        }

                        foreach (char letter in input)
                        {
                            noDeListe += increment;
                            listeDeListes[noDeListe].Add(letter);


                            if (noDeListe == rail - 1 && isUp == true)
                            {
                                increment = -1;
                                isUp = false;
                            }
                            else if (noDeListe == 0 && isUp == false)
                            {
                                increment = 1;
                                isUp = true;
                            }
                        }

                        foreach (List<char> liste in listeDeListes)
                        {
                            foreach (char letter in liste)
                            {
                                tempString += letter;
                            }
                        }

                        richTextBoxOutput.Text = tempString;

                        break;

                    case 5:
                        // Les hashages sont irréversibles  
                        // SHA-256
                        HashAlgorithm sha = SHA256.Create();

                        byte[] test = sha.ComputeHash(Encoding.ASCII.GetBytes(richTextBoxInput.Text));
                        string temp5 = "";

                        foreach (byte bytes in test)
                        {
                            temp5 += bytes;
                        }

                        richTextBoxOutput.Text = temp5;

                        break;

                    case 6:
                        // AES
                        
                        var stringAESraw = Encoding.UTF8.GetBytes(richTextBoxInput.Text);
                        byte[] encrypted = null;

                        using (Aes aesAlg = Aes.Create())
                        {
                            aesAlg.Mode = CipherMode.CBC;

                            byte[] keyAES = aesAlg.Key;
                            byte[] vectorAES = aesAlg.IV;
                            
                            textBoxIV.Text = Convert.ToBase64String(vectorAES);
                            textBoxKey.Text = Convert.ToBase64String(keyAES);

                            // Create an encryptor to perform the stream transform.
                            ICryptoTransform encryptor = aesAlg.CreateEncryptor(keyAES, vectorAES);

                            // Create the streams used for encryption.
                            using (MemoryStream msEncrypt = new MemoryStream())
                            {
                                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                                {
                                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                                    {
                                        //Write all data to the stream.
                                        swEncrypt.Write(stringAESraw);
                                        swEncrypt.Close();
                                        csEncrypt.Close();
                                    }
                                    encrypted = msEncrypt.ToArray();

                                    msEncrypt.Close();
                                }
                            }
                        }
                        richTextBoxOutput.Text = Convert.ToBase64String(encrypted);

                        break;

                    case 7:
                        // Code Playfair
                        string textBrutPF = richTextBoxInput.Text.Replace(" ", "").ToLower();
                        List<char> textPF = new();
                        List<char> tempTextPF = new();
                        string keyPF = textBoxJustKey.Text.Replace(" ", "").ToLower();
                        int iPF = 0;
                        int iiPF = 0;

                        char[,] tableauPF = new char[5, 5]; 
                        List<char> tempDupli = new List<char>();

                        // Remplis le carré 5x5 avec la key sans doublon
                        foreach (char letter in keyPF)
                        {
                            if (tempDupli.Contains(letter)) { }
                            else
                            {
                                tempDupli.Add(letter);

                                tableauPF[iPF, iiPF] = letter;
                                iiPF++;

                                if (iiPF == 5)
                                {
                                    iiPF = 0;
                                    iPF++;
                                }
                            }
                        }
                        
                        // Remplis le reste avec l'alphabet sans duplicat
                        foreach (char letter in alphabet)
                        {
                            if (tempDupli.Contains(letter) || letter == 'w') { }
                            else
                            {
                                tempDupli.Add(letter);

                                tableauPF[iPF, iiPF] = letter;
                                iiPF++;

                                if (iiPF == 5)
                                {
                                    iiPF = 0;
                                    iPF++;
                                }
                            }
                        }

                        // Générer une liste avec les lettres de notre phrase a chiffrer
                        foreach (char letter in textBrutPF)
                        {
                            textPF.Add(letter);
                        }

                        // Récupère les lettres de la phrase à chiffrer, deux par deux
                        for (int i2 = 0; i2 < textBrutPF.Length; i2 += 2)
                        {
                            int ii2 = 0;
                            foreach (char letter in textPF)
                            {
                                tempTextPF.Add(letter);
                                textPF.Remove(letter);
                                ii2++;

                                if (ii2 > 1)
                                {
                                    break;
                                }

                            }


                            // Si i et i' sont similaire -> même ligne
                            // Si ii et ii' sont similaire -> même colonne
                            // Sinon -> carré

                        }




                        // Zone test du tableau de chiffrement
                        string testString = "";
                        foreach(char letter in tableauPF)
                        {
                            testString += letter;
                        }

                        richTextBoxOutput.Text = testString;

                        break;
                    }  
                
                }
            catch 
            { 
                AllErrors(0); 
            }

        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {

            try
            {
                switch (comboBoxMenu.SelectedIndex)
                {
                    case 0:
                        // code césar
                        if (textBoxJustKey.Text == "" || textBoxJustKey.Text.Contains(@"[a-zA-Z]")) { AllErrors(0); }
                        else
                        {
                            int decalage = -Convert.ToInt32(textBoxJustKey.Text);

                            // Fonction de chiffrage César
                            Func<int, int, int> mod = (val, m) => val % m + (val < 0 ? m : 0);

                            Func<char, char, int, char> decal =
                                (c, offset, m) => (char)(offset + mod(c - offset + decalage, m));

                            Func<char, char> cesar =
                             c => ('a' <= c && c <= 'z') ? decal(c, 'a', 26)
                                : ('A' <= c && c <= 'Z') ? decal(c, 'A', 26)
                                : ('0' <= c && c <= '9') ? decal(c, '0', 10)
                                : c;

                            richTextBoxOutput.Text = new string(richTextBoxInput.Text.Select(cesar).ToArray());
                        }
                        break;

                    case 1:
                        // Atbash
                        if (richTextBoxInput.Text == "") { AllErrors(1); }
                        else
                        {
                            string stringTemp = "";
                            int intTemp = 0;

                            foreach (char letter in richTextBoxInput.Text)
                            {
                                if (char.IsWhiteSpace(letter))
                                {
                                    stringTemp += ' ';
                                }
                                else if (
                                    letter == '0' || letter == '1' || letter == '2' || letter == '3' || letter == '4' ||
                                    letter == '5' || letter == '6' || letter == '7' || letter == '8' || letter == '9' ||
                                    letter == ',' || letter == '.' || letter == '-' || letter == '?' || letter == '!')
                                {
                                    stringTemp += letter;
                                }
                                else
                                {
                                    intTemp = Array.IndexOf(alphabet, letter);
                                    stringTemp += Convert.ToString(alphabet[25 - intTemp]);
                                }
                            }
                            richTextBoxOutput.Text = stringTemp;
                        }
                        break;

                    case 2:
                        // Base64
                        var base64EncodedBytes = Convert.FromBase64String(richTextBoxInput.Text);
                        richTextBoxOutput.Text = Encoding.UTF8.GetString(base64EncodedBytes);
                        break;

                    case 4:
                        // Code Rail Fencer
                        string input = richTextBoxInput.Text;
                        int rail = Convert.ToInt32(textBoxJustKey.Text);
                        int increment = 1;
                        int noDeListe = -1;
                        bool isUp = true;


                        // Créez une liste de listes
                        List<List<char>> listeDeListes = new List<List<char>>();

                        // Boucle pour créer et ajouter "x" listes à la liste principale
                        for (int i4 = 0; i4 < rail; i4++)
                        {
                            List<char> nouvelleListe = new List<char>();
                            listeDeListes.Add(nouvelleListe);
                        }

                        foreach (char letter in input)
                        {
                            noDeListe += increment;
                            listeDeListes[noDeListe].Add('X');


                            if (noDeListe == rail - 1 && isUp == true)
                            {
                                increment = -1;
                                isUp = false;
                            }
                            else if (noDeListe == 0 && isUp == false)
                            {
                                increment = 1;
                                isUp = true;
                            }
                        }

                        int temp = 0;
                        int iterationCount = 0;
                        int longueur = 0;

                        foreach (char letter in richTextBoxInput.Text)
                        {
                            longueur = listeDeListes[temp].Count() - 1;

                            if (iterationCount < longueur)
                            {
                                listeDeListes[temp].RemoveAt(0);
                                listeDeListes[temp].Add(letter);
                                iterationCount++;
                            }
                            else
                            {
                                listeDeListes[temp].RemoveAt(0);
                                listeDeListes[temp].Add(letter);
                                temp++;
                                iterationCount = 0;
                            }
                        }

                        int iteration = 0;
                        int noList = -1;
                        int noElem = 0;
                        bool isUp2 = true;
                        int increment2 = 1;
                        string tempString = "";
                        for (int i = 0; i < richTextBoxInput.Text.Length; i++)
                        {
                            noList += increment2;
                            tempString += listeDeListes[noList].ElementAt(noElem);
                            iteration++;

                            if (iteration == rail)
                            {
                                iteration = 0;
                                noElem++;
                            }

                            if (noList == rail - 1 && isUp2 == true)
                            {
                                increment2 = -1;
                                isUp2 = false;
                                iteration += 1;
                            }
                            else if (noList == 0 && isUp2 == false)
                            {
                                increment2 = 1;
                                isUp2 = true;
                                iteration += 1;
                            }

                        }

                        richTextBoxOutput.Text = tempString;

                        break;

                    case 6:
                        // AES

                        byte[] stringAESraw = Encoding.UTF8.GetBytes(richTextBoxInput.Text);
                        byte[] encrypted = null;

                        using (Aes aesAlg = Aes.Create())
                        {
                            aesAlg.Mode = CipherMode.CBC;

                            byte[] keyAES = Convert.FromBase64String(textBoxJustKey.Text);
                            byte[] ivAES = Convert.FromBase64String(textBoxIV.Text);

                            // Create an encryptor to perform the stream transform.
                            ICryptoTransform decryptor = aesAlg.CreateDecryptor(keyAES, ivAES);

                            // Create the streams used for encryption.
                            using (MemoryStream msEncrypt = new MemoryStream())
                            {
                                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Write))
                                {
                                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                                    {
                                        //Write all data to the stream.
                                        swEncrypt.Write(stringAESraw);
                                        swEncrypt.Close();
                                        csEncrypt.Close();
                                    }
                                    encrypted = msEncrypt.ToArray();

                                    msEncrypt.Close();
                                }
                            }
                        }

                        string tempStringAES = "";
                        foreach (byte bytes in encrypted)
                        {
                            tempStringAES += bytes;
                        }

                        richTextBoxOutput.Text = tempStringAES;

                        break;
                }
            }
            catch
            {
                AllErrors(0);
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(richTextBoxOutput.Text);
            }
            catch { }
        }

        private void comboBoxMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMenu.SelectedIndex == 0)
            {
                textBoxJustKey.Visible = true;
            }
            else if (comboBoxMenu.SelectedIndex == 4)
            {
                textBoxJustKey.Visible = true;
            }
            else if (comboBoxMenu.SelectedIndex == 6)
            {
                textBoxIV.Visible = true;
                textBoxKey.Visible = true;
                textBoxIV.ReadOnly = true;
                textBoxKey.ReadOnly = true;
            }
            else if (comboBoxMenu.SelectedIndex == 7)
            {
                textBoxJustKey.Visible = true;
            }
            else 
            { 
                textBoxJustKey.Visible = false; 
                buttonDecrypt.Enabled = true; 
                textBoxIV.Visible = false; 
                textBoxKey.Visible = false; 
                textBoxIV.ReadOnly = false; 
                textBoxKey.ReadOnly = false;
                clearText();
            }

            textBoxJustKey.Text = string.Empty;
        }

        private void AllErrors(int errorNum)
        {
            switch (errorNum)
            {
                // Erreur clé/valeur de "textBoxCle" vide.
                case 0:
                    MessageBox.Show("Veuillez entrer une clé/valeur valide !");
                    textBoxJustKey.Text = "";
                    break;

                // Erreur "richTextBoxInput" vide.
                case 1:
                    MessageBox.Show("Veuillez entrer au moins un caractère valide dans la zone de texte");
                    break;
            }

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearText();
        }

        private void clearText()
        {
            textBoxJustKey.Text = "";
            richTextBoxInput.Text = "";
            richTextBoxOutput.Text = "";
            textBoxIV.Text = "";
            textBoxKey.Text = "";
        }
    }
}
