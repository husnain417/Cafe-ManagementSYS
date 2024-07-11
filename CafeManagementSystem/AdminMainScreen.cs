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
    public partial class AdminMainScreen : Form
    {
        DB_Access database = new DB_Access();
        public String adminEmail { get; set; }
        public String adminPassword { get; set; }

        private bool viewCheck = false;
        public String adminId { get; set; }

        private bool check = false;

        public AdminMainScreen(string adminid)
        {
            InitializeComponent();
            adminId = adminid;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (NameField.Text == "" || ContactField.Text == "" || QualificationField.Text == "" || EmailField.Text == ""
                || PassowordField.Text == "" || MaleRadioButton.Checked != true && FemaleRadioButton.Checked != true)
            {
                ErrorLabel.Text = "Please Fill all Fields!";
                return;

            }
            String insertionQuery = "";
            if (MaleRadioButton.Checked == true)
            {
                insertionQuery = $"INSERT INTO Manager (admin_id, manager_name, manager_qualification, manager_email, contact_info, manager_password, gender) VALUES ('{adminId}', '{NameField.Text}', '{QualificationField.Text}', '{EmailField.Text}', '{ContactField.Text}', '{PassowordField.Text}', 'Male')";
            }
            else if (FemaleRadioButton.Checked == true)
            {
                insertionQuery = $"INSERT INTO Manager (admin_id, manager_name, manager_qualification, manager_email, contact_info, manager_password, gender) VALUES ('{adminId}', '{NameField.Text}', '{QualificationField.Text}', '{EmailField.Text}', '{ContactField.Text}', '{PassowordField.Text}', 'Female')";
            }
            database.query_data(insertionQuery);
            ErrorLabel.Text = "Manager Added Successfully!";
        }
        private void adminTabChanged(object sender, EventArgs e)
        {
            if (AdminTab.SelectedIndex == 1 && check == false)
            {
                String searchManagers = $"select m.manager_name , m.gender, m.manager_qualification , m.contact_info, m.manager_id from Manager m join [Admin] ad on m.admin_id = ad.admin_id where ad.admin_email = '{adminEmail}' and ad.admin_password = '{adminPassword}'";
                DataTable dt = database.search_to_check(searchManagers);
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    CurrentManagers managerDiv = new CurrentManagers()
                    {
                        getSetName = dt.Rows[i][0].ToString(),
                        getSetGender = dt.Rows[i][1].ToString(),
                        getSetQuali = dt.Rows[i][2].ToString(),
                        getSetContact = dt.Rows[i][3].ToString(),
                        getSetID = dt.Rows[i][4].ToString()

                    };
                  //  ManagerFlow.Controls.Add(managerDiv);
                    check = true;
                }
            }

            if (AdminTab.SelectedIndex == 2)
            {
                String numManagersQuery = $"SELECT COUNT(Manager.manager_id) AS NumberOfManagers FROM Admin LEFT JOIN Manager ON Admin.admin_id = Manager.admin_id GROUP BY Admin.admin_id HAVING Admin.admin_id = '{adminId}'";
                String searchAdmin = $"SELECT * FROM Admin where admin_email = '{adminEmail}' AND admin_password = '{adminPassword}'";
                String viewCheckQuery = "IF NOT EXISTS (SELECT * FROM sys.views WHERE name = 'AdminView')\r\nBEGIN\r\n    CREATE VIEW AdminView AS SELECT * FROM Admin WHERE admin_email = '@adminEmail' AND admin_password = '@adminPassword'\r\nEND\r\n";
                // Check if the view exists
                DataTable viewCheckResult = database.search_to_check(viewCheckQuery);
                // If the view doesn't exist, create it
                if (viewCheckResult.Rows.Count == 0)
                {
                    database.query_data(viewCheckQuery);
                }

                // Retrieve data using the view
                DataTable dt = database.search_to_check("SELECT * FROM AdminView");

                IdBox.Text = dt.Rows[0][0].ToString();
                NameBox.Text = dt.Rows[0][1].ToString();
                QualiBox.Text = dt.Rows[0][2].ToString();
                MailBox.Text = dt.Rows[0][4].ToString();
                ContactBox.Text = dt.Rows[0][5].ToString();
                dt = database.search_to_check(numManagersQuery);
                NumManagers.Text = dt.Rows[0][0].ToString();
            }

            if (AdminTab.SelectedIndex == 4)
            {
                //String query = "";
                //DataTable dt;
                //query = $"SELECT Admin.admin_id, COUNT(Orders.order_id) AS NumberOfOrders FROM Admin LEFT JOIN Manager ON Admin.admin_id = Manager.admin_id LEFT JOIN Baristas ON Manager.manager_id = Baristas.manager_id LEFT JOIN Orders ON Baristas.baristas_id = Orders.baristas_id WHERE CONVERT(DATE, Orders.order_date) = CONVERT(DATE, GETDATE()) GROUP BY Admin.admin_id HAVING Admin.admin_id = '{adminId}'";
                //dt = database.search_to_check(query);
                //OrderCompleted.Text = dt.Rows[0][0].ToString();
            }
        }

        private void EditNameButton_Click(object sender, EventArgs e)
        {
            String editQuery = $"update [Admin] set admin_name = '{NameBox.Text}' where admin_id = '{adminId}'";
            database.query_data(editQuery);
            SuccessLabel.Text = "Name Changed Successfully. Refresh!";
        }

        private void EditQualiButton_Click(object sender, EventArgs e)
        {
            String editQuery = $"update [Admin] set admin_qualification = '{QualiBox.Text}' where admin_id = '{adminId}'";
            database.query_data(editQuery);
            SuccessLabel.Text = "Qualification Changed Successfully. Refresh!";
        }

        private void EditContactButton_Click(object sender, EventArgs e)
        {
            String editQuery = $"update [Admin] set contact_info = '{ContactBox.Text}' where admin_id = '{adminId}'";
            database.query_data(editQuery);
            SuccessLabel.Text = "Qualification Changed Successfully. Refresh!";
        }

        private void FireMgButton_Click(object sender, EventArgs e)
        {
            String searchQuery = "";
            String nullQuery1 = "";
            String nullQuery2 = "";
            DataTable dt;
            String deleteQuery;
            if (FireMgIdBox.Text == "" && FireMgMailBox.Text == "")
            {
                FireTabErrorLabel.Text = "Kindly fill at least one field!";
                return;
            }
            if (FireMgIdBox.Text != "" && FireMgMailBox.Text == "")
            {

                searchQuery = $"select * from manager where manager_id = '{FireMgIdBox.Text}' and admin_id = {adminId}";
                dt = database.search_to_check(searchQuery);
                if (dt.Rows.Count == 0)
                {
                    FireTabErrorLabel.Text = "Invalid Credentials!";
                    return;
                }
                else
                {
                    deleteQuery = $"delete from manager where manager_id = '{FireMgIdBox.Text}'";
                    nullQuery1 = $"update baristas set manager_id = NULL where manager_id = '{FireMgIdBox.Text}'";
                    nullQuery2 = $"update cashier set manager_id = NULL where manager_id = '{FireMgIdBox.Text}'";
                    database.query_data(nullQuery1);
                    database.query_data(nullQuery2);
                    database.query_data(deleteQuery);
                    FireTabErrorLabel.Text = "Manager Fired Successfully!";
                }
            }
            else if (FireMgIdBox.Text == "" && FireMgMailBox.Text != "")
            {

                searchQuery = $"select * from manager where manager_email = '{FireMgMailBox.Text}' and admin_id = '{adminId}'";
                dt = database.search_to_check(searchQuery);
                if (dt.Rows.Count == 0)
                {
                    FireTabErrorLabel.Text = "Invalid Credentials!";
                    return;
                }
                else
                {
                    deleteQuery = $"delete from manager where manager_email = '{FireMgMailBox.Text}'";
                    nullQuery1 = $"update baristas set manager_id = NULL where manager_id = (select manager_id from Manager m where m.manager_email = '{FireMgMailBox.Text}')";
                    nullQuery2 = $"update cashier set manager_id = NULL where manager_id = (select manager_id from Manager m where m.manager_email = '{FireMgMailBox.Text}')";
                    database.query_data(nullQuery1);
                    database.query_data(nullQuery2);
                    database.query_data(deleteQuery);
                    FireTabErrorLabel.Text = "Manager Fired Successfully!";
                }
            }
            else
            {
                FireTabErrorLabel.Text = "Kindly fill only field!";
            }

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadManagers()
        {
            string query = $"SELECT manager_name, gender, manager_qualification, contact_info, manager_id FROM Manager WHERE admin_id = '{adminId}'";
            DataTable dt = database.search_to_check(query);

            if (dt.Rows.Count > 0)
            {
                guna2DataGridView1.DataSource = dt;
            }
            else
            {
                // If no managers are found, display a message or handle it accordingly
                MessageBox.Show("No managers found.");
            }
        }

        private void AdminMainScreen_Load(object sender, EventArgs e)
        {
            LoadManagers();
            LoadCompletedOrders();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            LoadManagers();
        }

        private void LoadCompletedOrders()
        {
            try
            {
                // Clear existing columns
                guna2DataGridView2.Columns.Clear();

                string query = @"
            SELECT
                o.order_id AS 'Order ID',
                o.customer_id AS 'Customer ID',
                oi.item_name AS 'Item Name',
                oi.item_size AS 'Item Size',
                oi.item_type AS 'Item Type',
                op.item_quantity AS 'Quantity',
                o.order_time AS 'Order Time',
                o.order_date AS 'Order Date',
                o.order_status AS 'Order Status'
            FROM
                Orders o
            LEFT JOIN
                OrderProducts op ON op.order_id = o.order_id
            LEFT JOIN
                OrderItem oi ON op.item_id = oi.item_id
            WHERE
                o.order_status = 'Completed'";

                DataTable dataTable = database.SelectQuery(query);

                guna2DataGridView2.DataSource = dataTable;

                // Count the completed orders
                int completedOrderCount = dataTable.Rows.Count;

                // Display the count in guna2HtmlLabel20
                guna2HtmlLabel20.Text = $"{completedOrderCount}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading completed orders: " + ex.Message);
            }
        }






        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //// Inside AdminMainScreen form code

        //// Method to populate CurrentManagers UserControl with data
        //private void PopulateCurrentManagers()
        //{
        //    // Fetch data about current managers from the database
        //    DataTable dt = // Perform database query to get current managers data

        //    // Clear existing controls in ManagerFlow panel
        //    ManagerFlow.Controls.Clear();

        //    // Iterate through the DataTable and add CurrentManagers UserControl for each manager
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        CurrentManagers managerDiv = new CurrentManagers()
        //        {
        //            getSetName = row["manager_name"].ToString(),
        //            getSetGender = row["gender"].ToString(),
        //            getSetQuali = row["manager_qualification"].ToString(),
        //            getSetContact = row["contact_info"].ToString(),
        //            getSetID = row["manager_id"].ToString()
        //        };

        //        // Add CurrentManagers UserControl to ManagerFlow panel
        //        ManagerFlow.Controls.Add(managerDiv);
        //    }


    }
}
