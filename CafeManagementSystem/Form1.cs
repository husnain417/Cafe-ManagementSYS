using CafeManagementSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeManagementSystem
{
    public partial class Form1 : Form
    {
        DB_Access database = new DB_Access();
        public Form1()
        {
            InitializeComponent();

        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            if (EmailBox.Text == "" || PassBox.Text == "")
            {
                ErrorLabel.Text = "Please Fill all the Fields!";
                return;
            }
            String searchQuery = $"select * from Accounts where email = '{EmailBox.Text}' and password = '{PassBox.Text}'";
            DataTable dt = database.search_to_check(searchQuery);
            if (dt.Rows.Count == 0)
            {
                ErrorLabel.Text = "Invalid Credentials!";
                return;
            }
            else
            {
                String searchAdmin = $"SELECT * FROM Admin where admin_email = '{EmailBox.Text}' AND admin_password = '{PassBox.Text}'";
                dt = database.search_to_check(searchAdmin);
                if (dt.Rows.Count == 1)
                {

                    string managerName = dt.Rows[0]["admin_name"].ToString();
                    string managerEmail = dt.Rows[0]["admin_email"].ToString();
                    string managerId = dt.Rows[0]["admin_id"].ToString();

                    AdminMainScreen adminMainScreen = new AdminMainScreen(managerId);
                    adminMainScreen.Show();
                    this.Hide();
                    return;
                }
                String searchManager = $"SELECT * FROM Manager where manager_email = '{EmailBox.Text}' AND manager_password = '{PassBox.Text}'";
                dt = database.search_to_check(searchManager);
                if (dt.Rows.Count == 1)
                {
                    string managerName = dt.Rows[0]["manager_name"].ToString();

                    string managerEmail = dt.Rows[0]["manager_email"].ToString();

                    string managerId = dt.Rows[0]["manager_id"].ToString();


                    Manager mainScreen = new Manager(managerName, managerEmail, managerId);
                    mainScreen.Show();
                    this.Hide();
                    return;
                }
                String searchCashier = $"SELECT * FROM Cashier where cashier_email = '{EmailBox.Text}' AND cashier_password = '{PassBox.Text}'";
                dt = database.search_to_check(searchCashier);
                if (dt.Rows.Count == 1)
                {
                    CashierMainScreen mainScreen = new CashierMainScreen();
                    mainScreen.Show();
                    this.Hide();
                    return;
                }
                String searchBaristas = $"SELECT * FROM Baristas where baristas_email = '{EmailBox.Text}' AND baristas_password = '{PassBox.Text}'";
                dt = database.search_to_check(searchBaristas);
                if (dt.Rows.Count == 1)
                {
                    string baristaName = dt.Rows[0]["baristas_name"].ToString();
                    string baristaEmail = dt.Rows[0]["baristas_email"].ToString();
                    string baristaId = dt.Rows[0]["baristas_id"].ToString();

                    Barista mainScreen = new Barista(baristaId);
                    mainScreen.Show();
                    this.Hide();
                    return;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
