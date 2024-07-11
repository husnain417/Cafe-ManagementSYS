using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeManagementSystem
{
    public partial class ManagerControls : Form
    {
        DB_Access database = new DB_Access();
        public String managerEmail { get; set; }
        public String managerPassword { get; set; }
        public String managerId { get; set; }

        private bool check = false;

        public ManagerControls()
        {
            InitializeComponent();
            LoadEmployeeShifts();
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegiEMP_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2ContainerControl1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2HtmlLabel19_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel18_Click(object sender, EventArgs e)
        {

        }


        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (guna2TextBox1.Text == "" || ContactField.Text == "" || QualificationField.Text == "" || EmailField.Text == ""
                || PassowordField.Text == "" || MaleRadioButton.Checked != true && FemaleRadioButton.Checked != true || Cashierradio.Checked != true && baristaradio.Checked != true)
            {
                ErrorLabel.Text = "Please Fill all Fields!";
                return;

            }
            String insertionQuery = "";
            if (Cashierradio.Checked == true)
            {
                if (MaleRadioButton.Checked == true)
                {
                    insertionQuery = $"INSERT INTO Cashier (manager_id, cashier_name, cashier_qualification, cashier_email, contact_info, cashier_password, gender) VALUES ('{managerId}', '{guna2TextBox1.Text}', '{QualificationField.Text}', '{EmailField.Text}', '{ContactField.Text}', '{PassowordField.Text}', 'Male')";
                }
                if (FemaleRadioButton.Checked == true)
                    insertionQuery = $"INSERT INTO Cashier (manager_id,  cashier_name,  cashier_qualification, cashier_email, contact_info,  cashier_password, gender) VALUES ('{managerId}', '{guna2TextBox1.Text}', '{QualificationField.Text}', '{EmailField.Text}', '{ContactField.Text}', '{PassowordField.Text}', 'Female')";
            }
            else if (baristaradio.Checked == true)
            {
                if (MaleRadioButton.Checked == true)
                {
                    insertionQuery = $"INSERT INTO Baristas (manager_id, baristas_name, baristas_qualification, baristas_email, contact_info, baristas_password, gender) VALUES ('{managerId}', '{guna2TextBox1.Text}', '{QualificationField.Text}', '{EmailField.Text}', '{ContactField.Text}', '{PassowordField.Text}', 'Male')";
                }
                if (FemaleRadioButton.Checked == true)
                    insertionQuery = $"INSERT INTO Baristas (manager_id,  baristas_name,  baristas_qualification, baristas_email, contact_info,  baristas_password, gender) VALUES ('{managerId}', '{guna2TextBox1.Text}', '{QualificationField.Text}', '{EmailField.Text}', '{ContactField.Text}', '{PassowordField.Text}', 'Female')";
            }
            database.query_data(insertionQuery);
            ErrorLabel.Text = "Employee Added Successfully!";
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string query = $"SELECT 'Cashier' AS Role, cashier_id AS ID, cashier_name AS Name, cashier_email AS Email, contact_info AS ContactInfo FROM Cashier WHERE manager_id = '{managerId}' " +
                           $"UNION " +
                           $"SELECT 'Barista' AS Role, baristas_id AS ID, baristas_name AS Name, baristas_email AS Email, contact_info AS ContactInfo FROM Baristas WHERE manager_id = '{managerId}'";
            dt = database.search_to_check(query);

            guna2DataGridView1.DataSource = dt;

            guna2DataGridView1.Columns["Role"].HeaderText = "Role";
            guna2DataGridView1.Columns["ID"].HeaderText = "ID";
            guna2DataGridView1.Columns["Name"].HeaderText = "Name";
            guna2DataGridView1.Columns["Email"].HeaderText = "Email";
            guna2DataGridView1.Columns["ContactInfo"].HeaderText = "Contact Info";
        }


        private void namefield_TextChanged(object sender, EventArgs e)
        {

        }

        string originalEmail, originalName, originalQualification, originalContactInfo;

        private void EditNameButton_Click(object sender, EventArgs e)
        {
            string newName = NameBox.Text;
            if (newName != originalName)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to change your name from '{originalName}' to '{newName}'?", "Confirm Name Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = $"UPDATE Manager SET manager_name = '{newName}' WHERE manager_id = '{managerId}'";
                    database.query_data(query);
                    originalName = newName;
                    MessageBox.Show("Name updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void EditQualiButton_Click(object sender, EventArgs e)
        {
            string newQualification = QualiBox.Text;
            if (newQualification != originalQualification)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to change your qualification from '{originalQualification}' to '{newQualification}'?", "Confirm Qualification Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = $"UPDATE Manager SET manager_qualification = '{newQualification}' WHERE manager_id = '{managerId}'";
                    database.query_data(query);
                    originalQualification = newQualification;
                    MessageBox.Show("Qualification updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void EditContactButton_Click(object sender, EventArgs e)
        {
            string newContactInfo = ContactBox.Text;
            if (newContactInfo != originalContactInfo)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to change your contact info from '{originalContactInfo}' to '{newContactInfo}'?", "Confirm Contact Info Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = $"UPDATE Manager SET contact_info = '{newContactInfo}' WHERE manager_id = '{managerId}'";
                    database.query_data(query);
                    originalContactInfo = newContactInfo;
                    MessageBox.Show("Contact info updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            LoadEmployeeShifts();
            LoadInventoryReport();
            DataTable dt = new DataTable();
            string query = $"SELECT * FROM Manager WHERE manager_id = '{managerId}'";
            dt = database.search_to_check(query);

            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                IdBox.Text = row["manager_id"].ToString();
                MailBox.Text = originalEmail = row["manager_email"].ToString();
                NameBox.Text = originalName = row["manager_name"].ToString();
                QualiBox.Text = originalQualification = row["manager_qualification"].ToString();
                ContactBox.Text = originalContactInfo = row["contact_info"].ToString();
            }
            else
            {
                MessageBox.Show("Error: Manager information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FireMgButton_Click(object sender, EventArgs e)
        {
            if (!cashierr.Checked && !baristar.Checked)
            {
                MessageBox.Show("Please select whether the employee is a Cashier or a Barista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string role = cashierr.Checked ? "Cashier" : "Baristas";
            string employeeEmailOrId = FireMgIdBox.Text.Trim();
            string idColumnName = role == "Cashier" ? "cashier_id" : "baristas_id";
            string emailColumnName = role == "Cashier" ? "cashier_email" : "baristas_email";

            string query = $"SELECT * FROM {role} WHERE manager_id = '{managerId}' AND ({emailColumnName} = '{employeeEmailOrId}' OR {idColumnName} = '{employeeEmailOrId}')";

            DataTable dt = database.search_to_check(query);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show($"No {role} found with the provided email or ID under your management.", "Employee Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow employeeRow = dt.Rows[0];
            string employeeId = employeeRow[idColumnName].ToString();
            string employeeEmail = employeeRow[emailColumnName].ToString();

            DialogResult result = MessageBox.Show($"Are you sure you want to fire {role.ToLower()} with email/ID '{employeeEmail}' (ID: {employeeId})?", "Confirm Firing", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string deleteQuery = $"DELETE FROM {role} WHERE {idColumnName} = '{employeeId}'";

                database.query_data(deleteQuery);

                MessageBox.Show($"{role} with email/ID '{employeeEmail}' (ID: {employeeId}) has been successfully fired.", "Employee Fired", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2HtmlLabel33_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            string searchText = namefield.Text;
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Please enter a name or email to search.");
                return;
            }

            DataTable dt = new DataTable();
            string query = $"SELECT * FROM (SELECT 'Cashier' AS Role, cashier_name AS Name, cashier_email AS Email, contact_info AS ContactInfo FROM Cashier WHERE manager_id = '{managerId}' " +
                           $"UNION " +
                           $"SELECT 'Barista' AS Role, baristas_name AS Name, baristas_email AS Email, contact_info AS ContactInfo FROM Baristas WHERE manager_id = '{managerId}') AS Employees " +
                           $"WHERE Name = '{searchText}' OR Email = '{searchText}'";

            dt = database.search_to_check(query);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string role = row["Role"].ToString();
                string name = row["Name"].ToString();
                string email = row["Email"].ToString();
                string contactInfo = row["ContactInfo"].ToString();

                MessageBox.Show($"Role: {role}\nName: {name}\nEmail: {email}\nContact Info: {contactInfo}", "Employee Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Employee not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            AddShift();
        }

        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void EditMailButton_Click(object sender, EventArgs e)
        {
            string newEmail = MailBox.Text;
            if (newEmail != originalEmail)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to change your email from '{originalEmail}' to '{newEmail}'?", "Confirm Email Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = $"UPDATE Manager SET manager_email = '{newEmail}' WHERE manager_id = '{managerId}'";
                    database.query_data(query);
                    originalEmail = newEmail;
                    MessageBox.Show("Email updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LoadEmployeeShifts()
        {
            string query = $"SELECT b.baristas_id AS EmployeeID, b.baristas_name AS EmployeeName, 'Barista' AS EmployeeType, s.shift_date AS ShiftDate, s.start_time AS StartTime, s.end_time AS EndTime, s.attendance AS Attendance " +
                           $"FROM Baristas b " +
                           $"INNER JOIN Shift s ON b.baristas_id = s.staff_id " +
                           $"WHERE b.manager_id = '{managerId}' " +
                           $"UNION " +
                           $"SELECT c.cashier_id AS EmployeeID, c.cashier_name AS EmployeeName, 'Cashier' AS EmployeeType, s.shift_date AS ShiftDate, s.start_time AS StartTime, s.end_time AS EndTime, s.attendance AS Attendance " +
                           $"FROM Cashier c " +
                           $"INNER JOIN Shift s ON c.cashier_id = s.staff_id " +
                           $"WHERE c.manager_id = '{managerId}'";

            DataTable dt = database.SelectQuery(query);

            guna2DataGridView2.DataSource = dt;

            // Optionally, you can customize column headers here
            guna2DataGridView2.Columns["EmployeeID"].HeaderText = "Employee ID";
            guna2DataGridView2.Columns["EmployeeName"].HeaderText = "Employee Name";
            guna2DataGridView2.Columns["EmployeeType"].HeaderText = "Employee Type";
            guna2DataGridView2.Columns["ShiftDate"].HeaderText = "Shift Date";
            guna2DataGridView2.Columns["StartTime"].HeaderText = "Start Time";
            guna2DataGridView2.Columns["EndTime"].HeaderText = "End Time";
            guna2DataGridView2.Columns["Attendance"].HeaderText = "Attendance";
        }

        private void AddShift()
        {
            string employeeId = ID.Text.Trim();
            string role = cashier.Checked ? "Cashier" : "Barista";
            string shiftDate = date.Text.Trim();
            string startTime = Stime.Text.Trim();
            string endTime = Etime.Text.Trim();

            // Validate input fields
            if (string.IsNullOrEmpty(employeeId) || string.IsNullOrEmpty(shiftDate) || string.IsNullOrEmpty(startTime) || string.IsNullOrEmpty(endTime))
            {
                MessageBox.Show("Please fill all fields to add a shift.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the employee ID exists
            string employeeTable = role == "Cashier" ? "Cashier" : "Baristas";
            string idColumnName = role == "Cashier" ? "cashier_id" : "baristas_id";
            DataTable employeeCheckDt = database.SelectQuery($"SELECT * FROM {employeeTable} WHERE {idColumnName} = '{employeeId}' AND manager_id = '{managerId}'");
            if (employeeCheckDt.Rows.Count == 0)
            {
                MessageBox.Show($"No {role.ToLower()} found with the provided ID under your management.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the last shift ID
            DataTable lastShiftDt = database.SelectQuery($"SELECT TOP 1 shift_id FROM Shift ORDER BY shift_id DESC");
            int lastShiftId = 0;
            if (lastShiftDt.Rows.Count > 0)
            {
                lastShiftId = Convert.ToInt32(lastShiftDt.Rows[0]["shift_id"]);
            }

            int newShiftId = lastShiftId + 1;

            // Format strings for date and time
            string dateFormat = "yyyy-MM-dd";
            string timeFormat = "HH:mm:ss";

            // Parse shift date, start time, and end time
            DateTime parsedShiftDate, parsedStartTime, parsedEndTime;
            if (!DateTime.TryParseExact(shiftDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedShiftDate) ||
                !DateTime.TryParseExact(startTime, timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedStartTime) ||
                !DateTime.TryParseExact(endTime, timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedEndTime))
            {
                MessageBox.Show("Invalid date or time format. Please use the correct format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Convert parsed dates and times back to string format for SQL query
            string formattedShiftDate = parsedShiftDate.ToString(dateFormat);
            string formattedStartTime = parsedStartTime.ToString(timeFormat);
            string formattedEndTime = parsedEndTime.ToString(timeFormat);

            // Insert the shift record with the new shift ID
            string insertQuery = $"INSERT INTO Shift (shift_id, staff_id, shift_date, start_time, end_time, attendance) " +
                       $"VALUES ('{newShiftId}', '{employeeId}', '{formattedShiftDate}', '{formattedStartTime}', '{formattedEndTime}', '-')";

            database.query_data(insertQuery);

            MessageBox.Show($"Shift added successfully for {role.ToLower()} with ID '{employeeId}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadEmployeeShifts();
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
            Inventory I";

            // Execute the query and populate the DataTable
            dt = database.SelectQuery(query);

            // Set the DataTable as the data source for the DataGridView
            guna2DataGridView3.DataSource = dt;

            // Optionally, you can customize column headers here
            guna2DataGridView3.Columns["IngredientName"].HeaderText = "Ingredient Name";
            guna2DataGridView3.Columns["InventoryLocation"].HeaderText = "Inventory Location";
            guna2DataGridView3.Columns["Quantity"].HeaderText = "Quantity";
        }


    }
}
