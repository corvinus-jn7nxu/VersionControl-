using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            lblFullName.Text = Resource1.FullName; 
            btnAdd.Text = Resource1.Add;
            btnWrite.Text = Resource1.Write;
            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = txtFullName.Text

            };
            users.Add(u);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Names.txt";
            //sfd.ShowDialog();
            if (sfd.ShowDialog()==DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName, append: true);
                foreach (var line in users)
                {
                    sw.WriteLineAsync(line.ID.ToString());
                    sw.WriteLineAsync(line.FullName.ToString());
                }
                sw.Close();
            }
            
        }


    }
}
