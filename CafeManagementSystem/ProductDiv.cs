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
    public partial class ProductDiv : UserControl
    {
        #region Properties
        private String proID;
        private String proName;
        private String proIngredient;
        private String proSize;
        private String proType;
        public event EventHandler addItemBtn;

        public string getSetId
        {
            get
            {
                return proID;
            }
            set
            {
                proID = value;
                ProductIdLabel.Text = value;
            }
        }
        public string getSetName
        {
            get
            {
                return proName;
            }
            set
            {
                proName = value;
                ProductNameLabel.Text = value;
            }
        }
        public string getSetIngredient
        {
            get
            {
                return proIngredient;
            }
            set
            {
                proIngredient = value;
                ProductIngredientLabel.Text = value;
            }
        }
        public string getSetType
        {
            get
            {
                return proType;
            }
            set
            {
                proType = value;
                ProductTypeLabel.Text = value;
            }
        }
        public string getSetSize
        {
            get
            {
                return proSize;
            }
            set
            {
                proSize = value;
                ProductSizeLabel.Text = value;
            }
        }

        #endregion
        public ProductDiv()
        {
            InitializeComponent();
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            addItemBtn?.Invoke(this, e);
        }
    }
}
