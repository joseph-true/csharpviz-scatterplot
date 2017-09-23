using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication2
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();

            string msgText;

            msgText = "Author:   Joseph True\r\n";
            msgText = msgText + "Email:   jtrueprojects@gmail.com\r\n";
            msgText = msgText + "Copyright (c) 2008-2017 Joseph True\r\n\r\n";
            msgText = msgText + "This program imports 3 dimensions from the cars database: \r\n\r\n";
            msgText = msgText + "price \r\nHorsepower \r\n# of cylinders \r\n\r\n";
            msgText = msgText + "Cylinder values are shown as a # at the x,y data point\r\n";
            msgText = msgText + "The cylinder data was cleaned up so only values ";
            msgText = msgText + "for 4,6,8, 10 and 12 cyl remain";
            txtAbout.Text = msgText;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
