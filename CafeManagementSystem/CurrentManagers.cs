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
    public partial class CurrentManagers : UserControl
    {
        #region Properties

        private String mgName;
        private String mgQualification;
        private String mgGender;
        private String mgContact;
        private String mgID;


        public String getSetName
        {
            get
            {
                return mgName;
            }
            set
            {
                mgName = value;
                ManagerName.Text = value;
            }
        }
        public String getSetID
        {
            get
            {
                return mgID;
            }
            set
            {
                mgID = value;
                ManagerID.Text = value;
            }
        }
        public String getSetQuali
        {
            get
            {
                return mgQualification;
            }
            set
            {
                mgQualification = value;
                ManagerQualification.Text = value;
            }
        }

        public String getSetGender
        {
            get
            {
                return mgGender;
            }
            set
            {
                mgGender = value;
                ManagerGender.Text = value;
            }
        }
        public String getSetContact
        {
            get
            {
                return mgContact;
            }
            set
            {
                mgContact = value;
                ManagerContact.Text = value;
            }
        }
        #endregion

        public CurrentManagers()
        {
            InitializeComponent();
        }
    }
}
