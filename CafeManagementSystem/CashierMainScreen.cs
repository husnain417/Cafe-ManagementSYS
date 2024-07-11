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
    public partial class CashierMainScreen : Form
    {
        DB_Access database = new DB_Access();
        public String cashierEmail { get; set; }
        public String cashierPassword { get; set; }
        private int currOrders;
        private String[] arrayItem = new String[20];
        private int count = 0;
        private String baristasID;
        private bool check = false;
        private bool viewCheck = false;
        public String itemType { get; set; }
        public String itemSize { get; set; }

        public String customerName { get; set; }
        public String customerEmail { get; set; }
        public String customerContact { get; set; }
        public String cashierID { get; set; }
        public CashierMainScreen()
        {
            InitializeComponent();
            CashierTab.SelectedIndex = 1;
        }
        private void CashierMainScreenChanged(object sender, EventArgs e)
        {
            if (CashierTab.SelectedIndex == 0)
            {
                String createViewQuery = $"CREATE VIEW CashierView AS SELECT c.cashier_id, c.manager_id, c.cashier_email, c.cashier_name, c.cashier_qualification, c.contact_info FROM Cashier c WHERE cashier_email = '{cashierEmail}' AND cashier_password = '{cashierPassword}'";
                if (viewCheck == false)
                {
                    database.query_data(createViewQuery);
                    viewCheck = true;
                }

                String searchCashier = $"select c.cashier_id , c.manager_id , c.cashier_email, c.cashier_name, c.cashier_qualification,c.contact_info from Cashier c where cashier_email = '{cashierEmail}' and cashier_password = '{cashierPassword}'";
                DataTable dt = database.search_to_check($"Select * from cashierview");
                IdBox.Text = dt.Rows[0][0].ToString();
                ManagerBox.Text = dt.Rows[0][1].ToString();
                MailBox.Text = dt.Rows[0][2].ToString();
                NameBox.Text = dt.Rows[0][3].ToString();
                QualiBox.Text = dt.Rows[0][4].ToString();
                ContactBox.Text = dt.Rows[0][5].ToString();
            }
            if (CashierTab.SelectedIndex == 1)
            {
                if (check == false)
                {
                    ProductDivFlow.Hide();
                }

            }
            if (CashierTab.SelectedIndex == 2)
            {
                for (int i = 0; i < count; i++)
                {
                    OrderItemsLabel.Text += arrayItem[i];
                    OrderItemsLabel.Text += " ";
                }
                CNLabel.Text = customerName;
            }
        }

        private void EditNameButton_Click(object sender, EventArgs e)
        {
            String editQuery = $"update [cashier] set cashier_name = '{NameBox.Text}' where cashier_id = '{cashierID}'";
            database.query_data(editQuery);
            ProfileErrorLabel.Text = "Name Changed Successfully. Refresh!";
        }

        private void EditQualiButton_Click(object sender, EventArgs e)
        {
            String editQuery = $"update [cashier] set cashier_qualification = '{QualiBox.Text}' where cashier_id = '{cashierID}'";
            database.query_data(editQuery);
            ProfileErrorLabel.Text = "Qualification Changed Successfully. Refresh!";
        }

        private void EditContactButton_Click(object sender, EventArgs e)
        {
            String editQuery = $"update [cashier] set contact_info = '{ContactBox.Text}' where cashier_id = '{cashierID}'";
            database.query_data(editQuery);
            ProfileErrorLabel.Text = "Qualification Changed Successfully. Refresh!";
        }
        private void addItemButtonClicked(object sender, EventArgs e)
        {
            ProductDiv products = (ProductDiv)sender;
            arrayItem[count] = products.getSetName;
            count++;
        }
        private void FilterButton_Click(object sender, EventArgs e)
        {
            if (ItemSizeComboBox.Text == "Choose Size" || ItemTypeComboBox.Text == "Choose Type" || CustomerName.Text == "" ||
                CustomerEmail.Text == "" || CustomerContact.Text == "")
            {
                FilterPanelErrorLabel.Text = "Please Fill all the Fields!";
                return;
            }
            else
            {
                itemType = ItemTypeComboBox.Text;
                itemSize = ItemSizeComboBox.Text;
                customerName = CustomerName.Text;
                customerEmail = CustomerEmail.Text;
                customerContact = CustomerContact.Text;
                FilterPanel.Hide();
                ProductDivFlow.Show();
                if (check == false)
                {
                    String searchProducts = "";
                    searchProducts = $"select * from [OrderItem] oi join [Ingredient] i on oi.ingredient_id = i.ingredient_id where item_type = '{itemType}';";
                    DataTable dt = database.search_to_check(searchProducts);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProductDiv product = new ProductDiv()
                        {
                            getSetId = dt.Rows[i][0].ToString(),
                            getSetName = dt.Rows[i][2].ToString(),
                            getSetIngredient = dt.Rows[i][6].ToString(),
                            getSetSize = itemSize,
                            getSetType = itemType
                        };
                        ProductDivFlow.Controls.Add(product);
                        product.addItemBtn += addItemButtonClicked;
                        check = true;
                    }
                }

            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (PaymentTypeComboBox.Text == "Select Payment" || BaristasIDTextBox.Text == "")
            {
                ConfirmOrderErrorLabel.Text = "Kindly Select a Payment Method and Enter Baristas ID!";
                return;
            }
            else
            {
                DataTable dt;
                String currOrderQuery = $"select COUNT(*) FROM Orders";
                dt = database.search_to_check(currOrderQuery);
                currOrders = int.Parse(dt.Rows[0][0].ToString());
                currOrders++;
                baristasID = BaristasIDTextBox.Text;
                dt = database.search_to_check($"select * from baristas where baristas_id = '{BaristasIDTextBox.Text}'");
                if (dt.Rows.Count == 0)
                {
                    ConfirmOrderErrorLabel.Text = "Enter a valid Baristas ID!";
                    return;
                }
                String insertCustomer = $" insert into Customer(customer_name , customer_email , customer_contact_info) values ('{customerName}','{customerEmail}','{customerContact}')";
                database.query_data(insertCustomer);
                dt = database.search_to_check($"select customer_id from customer where customer_name = '{customerName}' and customer_email = '{CustomerEmail.Text}'");
                currOrders++;
                String insertOrder = $"insert into Orders(order_id,customer_id,baristas_id,order_time,order_date,payment_type,order_status) values ('{currOrders}','{dt.Rows[0][0].ToString()}','{baristasID}',CURRENT_TIMESTAMP,(select GETDATE()),'{PaymentTypeComboBox.Text}','In Progress')";
                database.query_data(insertOrder);
                for (int i = 0; i < count; i++)
                {
                    String insertOrderProducts = $"insert into [OrderProducts] (order_id,item_id,item_quantity) values ('{currOrders}',(select item_id from OrderItem where item_name = '{arrayItem[i]}'),1);";
                    database.query_data(insertOrderProducts);
                    arrayItem[i] = "";
                }
                count = 0;
            }
        }

        private void CashierMainScreen_Load(object sender, EventArgs e)
        {

        }

        private void FilterPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
