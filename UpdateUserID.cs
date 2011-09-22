using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WinMoss
{
    public partial class UpdateUserID : Form
    {
        private string mossFileString;

        public UpdateUserID(string inMossFile)
        {
            mossFileString = inMossFile;
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // hack filename for now
            FileInfo file = new FileInfo(mossFileString);    
            if(!file.Exists)
            {
                MessageBox.Show("The Mosswin.pl was not found in the specified location");
                Close();
            }

            // Open the file and read it to a string
            BinaryReader reader = new BinaryReader(file.OpenRead());
            char [] script = reader.ReadChars((int)file.Length);
            string strScript = new string(script);

            // Write the ID provided into the script string
            string searchStr = "$userid=";
            int userIDIndex = strScript.LastIndexOf(searchStr) + searchStr.Length;
            int endUserID = strScript.IndexOf(';', userIDIndex);
            strScript = strScript.Remove(userIDIndex, endUserID - userIDIndex);
            strScript = strScript.Insert(userIDIndex, userIDTextBox.Text);
            reader.Close();

            // Write the scrip string back to the script file
            BinaryWriter writer = new BinaryWriter(file.OpenWrite(), Encoding.ASCII);
            writer.Write(strScript.ToCharArray());
            writer.Close();

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void userIDTextBox_TextChanged(object sender, EventArgs e)
        {
            // Only accept digits, I'm sure there is an easier way to do this
            // but I already spent longer than writting this took to search for it
            for (int index = 0; index < userIDTextBox.Text.Length; )
            {
                if (!Char.IsDigit(userIDTextBox.Text[index]))
                {
                    userIDTextBox.Text = userIDTextBox.Text.Remove(index, 1);
                    continue;
                }
                ++index;
            }
        }
    }
}
