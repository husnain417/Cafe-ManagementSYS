using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeManagementSystem
{
    public partial class Barista : Form
    {
        private readonly DB_Access dbAccess = new DB_Access();

        String baristaid;
        public Barista(string baristaId)
        {
            baristaid = baristaId;
            InitializeComponent();
            LoadOrders(baristaId);
            LoadShifts(baristaId);
            LoadBaristaManagerDetails(baristaId);
        }

        string originalEmail, originalName, originalQualification, originalContactInfo;

        private void Barista_Load(object sender, EventArgs e)
        {
            LoadInventoryReport();
            LoadCustomerOrderHistory();
            DataTable dt = new DataTable();
            string query = $"SELECT * FROM Baristas WHERE baristas_id = '{baristaid}'";
            dt = dbAccess.search_to_check(query);

            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                IdBox.Text = row["baristas_id"].ToString();
                MailBox.Text = originalEmail = row["baristas_email"].ToString();
                NameBox.Text = originalName = row["baristas_name"].ToString();
                QualiBox.Text = originalQualification = row["baristas_qualification"].ToString();
                ContactBox.Text = originalContactInfo = row["contact_info"].ToString();
            }
            else
            {
                MessageBox.Show("Error: baristas information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadOrders(string baristaId)
        {
            try
            {
                string query = $@"
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
            JOIN
                Baristas b ON o.baristas_id = b.baristas_id
            LEFT JOIN
                OrderProducts op ON op.order_id = o.order_id
            LEFT JOIN
                OrderItem oi ON op.item_id = oi.item_id
            WHERE
                b.baristas_id = '{baristaId}'";

                DataTable dataTable = dbAccess.SelectQuery(query);

                guna2DataGridView2.DataSource = dataTable;

                // Adjust column widths, set selection mode, and other formatting...
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }



        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            LoadOrdersWithStatusFilter(baristaid, includeInProgressAndReady: false);
        }

        private void LoadOrdersWithStatusFilter(string baristaId, bool includeInProgressAndReady = false)
        {
            try
            {
                // SQL query to fetch all orders and their details for the specific barista
                string query = $@"
            SELECT
                o.order_id AS 'Order ID',
                o.customer_id AS 'Customer ID',
                oi.item_name AS 'Item Name',
                oi.item_size AS 'Item Size',
                oi.item_type AS 'Item Type',
                op.item_quantity AS 'Quantity',
                o.order_time AS 'Order Time',
                o.order_status AS 'Order Status'
            FROM
                Orders o
            JOIN
                Baristas b ON o.baristas_id = b.baristas_id
            JOIN
                OrderProducts op ON op.order_id = o.order_id
            JOIN
                OrderItem oi ON op.item_id = oi.item_id
            WHERE
                b.baristas_id = '{baristaId}'";

                // If includeInProgressAndReady is false, add filter for 'In Progress' and 'Ready' statuses
                if (!includeInProgressAndReady)
                {
                    query += " AND o.order_status IN ('In Progress', 'Ready')";
                }

                DataTable dataTable = dbAccess.SelectQuery(query);

                // Set the DataTable as the DataSource of the DataGridView
                guna2DataGridView2.DataSource = dataTable;

                // Adjust column widths to fit the content
                guna2DataGridView2.AutoResizeColumns();

                guna2DataGridView2.ColumnHeadersVisible = true;
                guna2DataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                guna2DataGridView2.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
                foreach (DataGridViewColumn column in guna2DataGridView2.Columns)
                {
                    column.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the order ID entered by the barista
                int orderId = Convert.ToInt32(guna2TextBox1.Text);

                // Update the order status to "Complete" in the database
                string query = $@"
            UPDATE Orders
            SET order_status = 'Complete'
            WHERE order_id = {orderId}";

                // Execute the update query
                dbAccess.query_data(query);

                MessageBox.Show("Order status updated to Complete successfully.");

                // Reload the orders in the DataGridView
                LoadOrders(baristaid);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating order status: " + ex.Message);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            LoadShiftsafter(baristaid);
        }

        private void LoadShiftsafter(string baristaId)
        {
            try
            {
                // Get today's date
                DateTime today = DateTime.Today;

                string query = $@"
SELECT
    s.staff_name AS 'Name',
    s.staff_type AS 'Role',
    shift_date AS 'Shift Date',
    start_time AS 'Start Time',
    end_time AS 'End Time',
    attendance AS 'Attendance'
FROM
    Shift sh
JOIN
    Staff s ON sh.staff_id = s.staff_id
WHERE
    s.staff_id = {baristaId} AND
    shift_date >= '{today.ToShortDateString()}'";

                DataTable dataTable = dbAccess.SelectQuery(query);

                guna2DataGridView1.DataSource = dataTable;

                // Show column headers
                guna2DataGridView1.ColumnHeadersVisible = true;

                // Set column header text
                guna2DataGridView1.Columns["Name"].HeaderText = "Name";
                guna2DataGridView1.Columns["Role"].HeaderText = "Role";
                guna2DataGridView1.Columns["Shift Date"].HeaderText = "Shift Date";
                guna2DataGridView1.Columns["Start Time"].HeaderText = "Start Time";
                guna2DataGridView1.Columns["End Time"].HeaderText = "End Time";
                guna2DataGridView1.Columns["Attendance"].HeaderText = "Attendance";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading shift schedule: " + ex.Message);
            }
        }




        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadShifts(string baristaId)
        {
            try
            {
                string query = $@"
    SELECT
        s.staff_name AS 'Name',
        s.staff_type AS 'Role',
        shift_date AS 'Shift Date',
        start_time AS 'Start Time',
        end_time AS 'End Time',
        attendance AS 'Attendance'
    FROM
        Shift sh
    JOIN
        Staff s ON sh.staff_id = s.staff_id
    WHERE
        s.staff_id = {baristaId}";

                DataTable dataTable = dbAccess.SelectQuery(query);

                guna2DataGridView1.DataSource = dataTable;

                guna2DataGridView1.ColumnHeadersVisible = true;

                guna2DataGridView1.Columns["Name"].HeaderText = "Name";
                guna2DataGridView1.Columns["Role"].HeaderText = "Role";
                guna2DataGridView1.Columns["Shift Date"].HeaderText = "Shift Date";
                guna2DataGridView1.Columns["Start Time"].HeaderText = "Start Time";
                guna2DataGridView1.Columns["End Time"].HeaderText = "End Time";
                guna2DataGridView1.Columns["Attendance"].HeaderText = "Attendance";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading shift schedule: " + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void LoadBaristaManagerDetails(string baristaId)
        {
            try
            {
                // Query to retrieve the manager and barista details for the given barista ID
                string query = $@"
            SELECT 
                M.manager_id AS 'Manager ID',
                B.baristas_id AS 'Barista ID',
                B.baristas_name AS 'Barista Name', 
                B.baristas_email AS 'Barista Email', 
                B.baristas_qualification AS 'Barista Qualification', 
                B.contact_info AS 'Barista Contact'
            FROM 
                Baristas B
            INNER JOIN 
                Manager M ON B.manager_id = M.manager_id
            WHERE 
                B.baristas_id = {baristaId}";

                // Execute the query to fetch manager and barista details
                DataTable dataTable = dbAccess.SelectQuery(query);

                // Check if any data was retrieved
                if (dataTable.Rows.Count > 0)
                {
                    // Populate the text boxes with the manager and barista details
                    IdBox.Text = dataTable.Rows[0]["Manager ID"].ToString();
                    NameBox.Text = dataTable.Rows[0]["Barista Name"].ToString();
                    MailBox.Text = dataTable.Rows[0]["Barista Email"].ToString();
                    QualiBox.Text = dataTable.Rows[0]["Barista Qualification"].ToString();
                    ContactBox.Text = dataTable.Rows[0]["Barista Contact"].ToString();
                }
                else
                {
                    // If no data is found, clear the text boxes or show a message
                    IdBox.Text = "";
                    NameBox.Text = "";
                    MailBox.Text = "";
                    QualiBox.Text = "";
                    ContactBox.Text = "";
                    MessageBox.Show("Details not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading details: " + ex.Message);
            }
        }
    

        private void EditNameButton_Click_1(object sender, EventArgs e)
        {
            string newName = NameBox.Text;
            if (newName != originalName)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to change your name from '{originalName}' to '{newName}'?", "Confirm Name Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = $"UPDATE Baristas SET baristas_name = '{newName}' WHERE barista_id = '{baristaid}'";
                    dbAccess.query_data(query);
                    originalName = newName;
                    MessageBox.Show("Name updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

        }

        private void EditQualiButton_Click_1(object sender, EventArgs e)
        {
            string newQualification = QualiBox.Text;
            if (newQualification != originalQualification)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to change your qualification from '{originalQualification}' to '{newQualification}'?", "Confirm Qualification Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = $"UPDATE Baristas SET baristas_qualification = '{newQualification}' WHERE baristas_id = '{baristaid}'";
                    dbAccess.query_data(query);
                    originalQualification = newQualification;
                    MessageBox.Show("Qualification updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void guna2DataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void EditContactButton_Click_1(object sender, EventArgs e)
        {
            string newContactInfo = ContactBox.Text;
            if (newContactInfo != originalContactInfo)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to change your contact info from '{originalContactInfo}' to '{newContactInfo}'?", "Confirm Contact Info Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = $"UPDATE Baristas SET contact_info = '{newContactInfo}' WHERE baristas_id = '{baristaid}'";
                    dbAccess.query_data(query);
                    originalContactInfo = newContactInfo;
                    MessageBox.Show("Contact info updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void EditMailButton_Click_1(object sender, EventArgs e)
        {
            string newEmail = MailBox.Text;
            if (newEmail != originalEmail)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to change your email from '{originalEmail}' to '{newEmail}'?", "Confirm Email Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = $"UPDATE Baristas SET baristas_email = '{newEmail}' WHERE baristas_id = '{baristaid}'";
                    dbAccess.query_data(query);
                    originalEmail = newEmail;
                    MessageBox.Show("Email updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LoadInventoryReport()
        {
            // Create a new DataTable to store the inventory report
            DataTable dt = new DataTable();

            // Construct the query to retrieve inventory information using subqueries
            string query = @"
    SELECT 
        (SELECT ingredient_name FROM Ingredient WHERE ingredient_id = I.ingredient_id) AS IngredientName,
        I.inventory_location AS InventoryLocation,
        I.ingredient_quantity AS Quantity
    FROM 
        Inventory I
    ORDER BY
        I.ingredient_quantity ASC";

            // Execute the query and populate the DataTable
            dt = dbAccess.SelectQuery(query);

            // Set the DataTable as the data source for the DataGridView
            guna2DataGridView3.DataSource = dt;

            // Optionally, you can customize column headers here
            guna2DataGridView3.Columns["IngredientName"].HeaderText = "Ingredient Name";
            guna2DataGridView3.Columns["InventoryLocation"].HeaderText = "Inventory Location";
            guna2DataGridView3.Columns["Quantity"].HeaderText = "Quantity";
        }

        private void LoadCustomerOrderHistory()
        {
            try
            {
                // Construct the query to retrieve customer details and their order history using nested subqueries
                string query = @"
            SELECT
                c.customer_id AS 'Customer ID',
                c.customer_name AS 'Customer Name',
                o.order_id AS 'Order ID',
                (SELECT oi.item_name FROM OrderItem oi WHERE oi.item_id = op.item_id) AS 'Item Name',
                (SELECT oi.item_size FROM OrderItem oi WHERE oi.item_id = op.item_id) AS 'Item Size',
                (SELECT oi.item_type FROM OrderItem oi WHERE oi.item_id = op.item_id) AS 'Item Type'
            FROM
                Customer c
            JOIN
                Orders o ON c.customer_id = o.customer_id
            LEFT JOIN
                OrderProducts op ON o.order_id = op.order_id";

                DataTable dataTable = dbAccess.SelectQuery(query);

                // Set the DataTable as the DataSource of the DataGridView
                guna2DataGridView4.DataSource = dataTable;

                // Optionally, you can customize column headers here
                guna2DataGridView4.Columns["Customer ID"].HeaderText = "Customer ID";
                guna2DataGridView4.Columns["Customer Name"].HeaderText = "Customer Name";
                guna2DataGridView4.Columns["Order ID"].HeaderText = "Order ID";
                guna2DataGridView4.Columns["Item Name"].HeaderText = "Item Name";
                guna2DataGridView4.Columns["Item Size"].HeaderText = "Item Size";
                guna2DataGridView4.Columns["Item Type"].HeaderText = "Item Type";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer order history: " + ex.Message);
            }
        }


    }
}
