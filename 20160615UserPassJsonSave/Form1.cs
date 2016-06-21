using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _20160615UserPassSave
{
    public partial class Form1 : Form
    {
        DataTable table;
        string username = ""; string password = ""; string result = ""; string website = "";
        bool visibility;
        public Form1()
        {

            InitializeComponent();
            table = new DataTable();
            table.Columns.Add("Username", typeof(string));
            table.Columns.Add("Password", typeof(string));
            table.Columns.Add("WebSite", typeof(string));
            dataGridView1.DataSource = table;
            try
            {
                txtRead(visibility = false);
            }
            catch (Exception)
            {
            }

        }

        //Save Txt
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Are you sure you want to saved.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                foreach (DataRow row in table.Rows)
                {
                    username = row["Username"].ToString();
                    password = row["Password"].ToString();

                    website = row["WebSite"].ToString();
                    result += username + ";" + password + ";" + website + "\r\n";
                }

                if (password == "******" || password == "" || password == null || password == " ")
                {
                    MessageBox.Show("ShowPass Button click,Empty pass and try");
                }
                else
                {
                    using (TextWriter tw = new StreamWriter("file.txt"))
                    {
                        try
                        {
                            tw.WriteLine(result);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Unable to write to a text file");
                        }

                    }
                    table.Clear();
                    Application.Restart();
                }
            }

        }

        void txtRead(bool visibility = true)
        {
            string line; string[] words; string user = ""; string pass = ""; string web = "";
            using (StreamReader sr = new StreamReader("file.txt"))
            {
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        words = line.Split(';');
                        user = words[0];
                        pass = words[1];
                        web = words[2];
                        if (visibility == true)
                            table.Rows.Add(new string[] { user, pass, web });
                        else
                            table.Rows.Add(new string[] { user, "******", web });
                    }
                    catch (Exception) { }
                }
            }
        }


        #region ButtonEvent

        //Show Pass
        private void Showbutton_Click(object sender, EventArgs e)
        {
            table.Clear();
            try
            {
                txtRead();
            }
            catch (Exception)
            {

                MessageBox.Show(" empty table :) or txtRead() method failed");
            }

        }
        //hide Pass
        private void Hidebutton_Click(object sender, EventArgs e)
        {
            table.Clear();
            try
            {
                txtRead(visibility = false);
            }
            catch (Exception)
            {

                MessageBox.Show("empty table :) or txtRead() Method Failed");
            }

        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

