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
using Microsoft.Extensions.Configuration;
using prn211.Repo.Models;

namespace prn211
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
             
        }
        BookPublisherContext bookPublisherContext = new BookPublisherContext();
        private void button1_Click(object sender, EventArgs e)
        {
            if(txtUserID.Text == String.Empty || txtPassword.Text == String.Empty)
            {
                MessageBox.Show("User ID or Password is not null!");
            }
            else
            {
                string cs = GetConnectionString();
                using (var db = new BookPublisherContext(cs))
                {
                    var user = db.AccountUsers.Where(a => a.UserId == txtUserID.Text && a.UserPassword == txtPassword.Text).FirstOrDefault();
                    if(user == null)
                    {
                        MessageBox.Show("Invalid UserID or Password!");
                    }
                    else
                    {
                        if(user.UserRole == 1)
                        {
                            frmManagement frmManagement = new frmManagement();
                            this.Close();
                            frmManagement.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("you are not allowed to access this function!");
                        }
                    }
                }
               
            }
        }
        public string GetConnectionString()
        {
            string connectionString;
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            connectionString = config["ConnectionStrings:BookPublisherDB"];
            return connectionString;
        }
    }
}
