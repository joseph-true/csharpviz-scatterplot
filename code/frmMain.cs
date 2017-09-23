// -------------------------------------------------
// C-Sharp DataViz Scatter Plot
//
// License:
// Copyright (c) 2008-2017 Joseph True
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// -------------------------------------------------
// Author:            Joseph True
// Email:             jtrueprojects@gmail.com
// Original Date:     10/15/2008
// Updated:           Aug, 2017
// History:           Originally created as a Scatter Plot programming project for CS 525D Fall, 2008
//
// Overall Design:    Reads CSV file with car data.
//                    Plots price and horsepower on x,y.
//                    Plots # of cylinders as a # at the x,y data point.
//
// Additional Files:  mydata.csv data file with 3 columns of car data
//
// -------------------------------------------------
//
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace WindowsApplication2
{
	// .NET genertared code for form
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
    {

		//---------------------------------------
		// application variables
		//---------------------------------------
		// String data values read from file
		ArrayList m_arrayX = new ArrayList();
		ArrayList m_arrayY = new ArrayList();
		ArrayList m_arrayZ = new ArrayList();

		// Axis points
		static int offsetX = 40;
        static int offsetY = 440;
		
		int m_xAxisStart = offsetX;
        int m_xAxisEnd = 800;
		int m_yAxisStart = offsetY;
        int m_yAxisEnd = offsetY - 400;

		//imported data
		float[,] m_XYdata;
		float m_xMin;
		float m_yMin;
		float m_xMax;
		float m_yMax;
		float m_xDataAxisStart;
		float m_xDataAxisEnd;
		float m_yDataAxisStart;
		float m_yDataAxisEnd;

        // Set default color for data points
        Color m_DataPtColor = Color.Black;


		//---------------------------------------

		private System.Windows.Forms.Button btnDrawViz;
        private System.Windows.Forms.Label lblMinMax;
        private Panel pnlViz;
        private CheckBox chkDataLabel;
        private ComboBox cboPointType;
        private Button btnDataPtColor;
        private GroupBox grpDataPoint;
        private ComboBox cboPointSize;
        private LinkLabel lnkAbout;
        private Label lblPtSize;
        private Label lblPtType;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

            // Populate data point type picklist
            cboPointType.Items.Add("None");
            cboPointType.Items.Add("Circle");
            cboPointType.Items.Add("Circle - filled");
            cboPointType.Items.Add("Square");
            cboPointType.Items.Add("Square - filled");
            cboPointType.SelectedIndex = 1;     // set to circle

            cboPointSize.Items.Add(2);
            cboPointSize.Items.Add(4);
            cboPointSize.Items.Add(6);
            cboPointSize.Items.Add(8);
            cboPointSize.SelectedIndex = 1;     // default size 4 

            // Display data labels by default
            chkDataLabel.Checked = true;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnDrawViz = new System.Windows.Forms.Button();
            this.lblMinMax = new System.Windows.Forms.Label();
            this.pnlViz = new System.Windows.Forms.Panel();
            this.chkDataLabel = new System.Windows.Forms.CheckBox();
            this.cboPointType = new System.Windows.Forms.ComboBox();
            this.btnDataPtColor = new System.Windows.Forms.Button();
            this.grpDataPoint = new System.Windows.Forms.GroupBox();
            this.lblPtSize = new System.Windows.Forms.Label();
            this.lblPtType = new System.Windows.Forms.Label();
            this.cboPointSize = new System.Windows.Forms.ComboBox();
            this.lnkAbout = new System.Windows.Forms.LinkLabel();
            this.grpDataPoint.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDrawViz
            // 
            this.btnDrawViz.Location = new System.Drawing.Point(906, 46);
            this.btnDrawViz.Name = "btnDrawViz";
            this.btnDrawViz.Size = new System.Drawing.Size(156, 32);
            this.btnDrawViz.TabIndex = 2;
            this.btnDrawViz.Text = "Draw Scatterplot";
            this.btnDrawViz.Click += new System.EventHandler(this.btnDrawViz_Click);
            // 
            // lblMinMax
            // 
            this.lblMinMax.Location = new System.Drawing.Point(24, 8);
            this.lblMinMax.Name = "lblMinMax";
            this.lblMinMax.Size = new System.Drawing.Size(312, 35);
            this.lblMinMax.TabIndex = 4;
            this.lblMinMax.Text = "X & Y min max values display here";
            // 
            // pnlViz
            // 
            this.pnlViz.BackColor = System.Drawing.SystemColors.Control;
            this.pnlViz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlViz.Location = new System.Drawing.Point(15, 46);
            this.pnlViz.Name = "pnlViz";
            this.pnlViz.Size = new System.Drawing.Size(873, 472);
            this.pnlViz.TabIndex = 6;
            this.pnlViz.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlViz_Paint);
            // 
            // chkDataLabel
            // 
            this.chkDataLabel.AutoSize = true;
            this.chkDataLabel.Location = new System.Drawing.Point(906, 107);
            this.chkDataLabel.Name = "chkDataLabel";
            this.chkDataLabel.Size = new System.Drawing.Size(108, 17);
            this.chkDataLabel.TabIndex = 7;
            this.chkDataLabel.Text = "Show Data Label";
            this.chkDataLabel.UseVisualStyleBackColor = true;
            this.chkDataLabel.CheckedChanged += new System.EventHandler(this.chkDataLabel_CheckedChanged);
            // 
            // cboPointType
            // 
            this.cboPointType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPointType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPointType.FormattingEnabled = true;
            this.cboPointType.Location = new System.Drawing.Point(15, 49);
            this.cboPointType.Name = "cboPointType";
            this.cboPointType.Size = new System.Drawing.Size(131, 21);
            this.cboPointType.TabIndex = 8;
            // 
            // btnDataPtColor
            // 
            this.btnDataPtColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataPtColor.Location = new System.Drawing.Point(15, 136);
            this.btnDataPtColor.Name = "btnDataPtColor";
            this.btnDataPtColor.Size = new System.Drawing.Size(131, 35);
            this.btnDataPtColor.TabIndex = 9;
            this.btnDataPtColor.Text = "Select color";
            this.btnDataPtColor.UseVisualStyleBackColor = true;
            this.btnDataPtColor.Click += new System.EventHandler(this.btnDataPtColor_Click);
            // 
            // grpDataPoint
            // 
            this.grpDataPoint.Controls.Add(this.lblPtSize);
            this.grpDataPoint.Controls.Add(this.lblPtType);
            this.grpDataPoint.Controls.Add(this.cboPointSize);
            this.grpDataPoint.Controls.Add(this.cboPointType);
            this.grpDataPoint.Controls.Add(this.btnDataPtColor);
            this.grpDataPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDataPoint.Location = new System.Drawing.Point(901, 148);
            this.grpDataPoint.Name = "grpDataPoint";
            this.grpDataPoint.Size = new System.Drawing.Size(161, 184);
            this.grpDataPoint.TabIndex = 10;
            this.grpDataPoint.TabStop = false;
            this.grpDataPoint.Text = "Data Point";
            // 
            // lblPtSize
            // 
            this.lblPtSize.AutoSize = true;
            this.lblPtSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPtSize.Location = new System.Drawing.Point(12, 78);
            this.lblPtSize.Name = "lblPtSize";
            this.lblPtSize.Size = new System.Drawing.Size(54, 13);
            this.lblPtSize.TabIndex = 12;
            this.lblPtSize.Text = "Point Size";
            // 
            // lblPtType
            // 
            this.lblPtType.AutoSize = true;
            this.lblPtType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPtType.Location = new System.Drawing.Point(12, 31);
            this.lblPtType.Name = "lblPtType";
            this.lblPtType.Size = new System.Drawing.Size(58, 13);
            this.lblPtType.TabIndex = 11;
            this.lblPtType.Text = "Point Type";
            // 
            // cboPointSize
            // 
            this.cboPointSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPointSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPointSize.FormattingEnabled = true;
            this.cboPointSize.Location = new System.Drawing.Point(15, 95);
            this.cboPointSize.Name = "cboPointSize";
            this.cboPointSize.Size = new System.Drawing.Size(131, 21);
            this.cboPointSize.TabIndex = 10;
            // 
            // lnkAbout
            // 
            this.lnkAbout.AutoSize = true;
            this.lnkAbout.Location = new System.Drawing.Point(958, 9);
            this.lnkAbout.Name = "lnkAbout";
            this.lnkAbout.Size = new System.Drawing.Size(104, 13);
            this.lnkAbout.TabIndex = 11;
            this.lnkAbout.TabStop = true;
            this.lnkAbout.Text = "About this program...";
            this.lnkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAbout_LinkClicked);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1079, 536);
            this.Controls.Add(this.lnkAbout);
            this.Controls.Add(this.grpDataPoint);
            this.Controls.Add(this.chkDataLabel);
            this.Controls.Add(this.pnlViz);
            this.Controls.Add(this.lblMinMax);
            this.Controls.Add(this.btnDrawViz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scatter Plot with Data Label ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpDataPoint.ResumeLayout(false);
            this.grpDataPoint.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		// --- JTrue --------------------------------------------
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			importData();

		}


		// supporting functions
		private void drawXYaxis()
		{
			using (Graphics g = pnlViz.CreateGraphics())
			{
                // ==============================
                // Draw the canvas
                Color myColor;
                myColor = Color.White;
                int wd = m_xAxisEnd - m_xAxisStart;
                int ht = m_yAxisStart - m_yAxisEnd;
                g.FillRectangle(new SolidBrush(myColor), m_xAxisStart, m_yAxisEnd, wd, ht);

				Pen myPen = new Pen(Color.Black,1);
				//x axis
				g.DrawLine(myPen, new Point(m_xAxisStart,m_yAxisStart), new Point(m_xAxisEnd,m_yAxisStart));
				//y axis
				g.DrawLine(myPen, new Point(m_xAxisStart,m_yAxisStart), new Point(m_xAxisStart,m_yAxisEnd));

				// Draw text labels
				Font fnt = new Font("Verdana", 10);
				g.DrawString("Horsepower", fnt, new SolidBrush(Color.Black),m_xAxisStart,m_yAxisEnd-20);
				g.DrawString("Price", fnt, new SolidBrush(Color.Black),m_xAxisEnd+10,m_yAxisStart-10);
				g.DrawString("Data Label = # of cylnders", fnt, new SolidBrush(Color.Black),(m_xAxisStart+150),m_yAxisEnd);
			}
		}

		private void importData()
		{
			FileStream aFile = new FileStream("mydata.csv",FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(aFile);

			string strLine;
			strLine = sr.ReadLine();
			string[] strData;
			char[] chrDelimeter = new char[] {','};

			// Read line from file and split into x, y, z dimensions
			// price, HP, # cylinders
			while (strLine !=null)
			{
				strData = strLine.Split(chrDelimeter,10);

				m_arrayX.Add (strData[0]);
				m_arrayY.Add (strData[1]);
				m_arrayZ.Add (strData[2]);
				
				strLine = sr.ReadLine();
			}

			sr.Close();
			
			m_XYdata = new float[3,m_arrayX.Count];

			for(int i=0;i<m_arrayX.Count;i++)
			{
				m_XYdata[0,i]=System.Convert.ToSingle( m_arrayX[i]);
				m_XYdata[1,i]=(System.Convert.ToSingle (m_arrayY[i]));
				m_XYdata[2,i]=(System.Convert.ToSingle (m_arrayZ[i]));
				//m_XYdata[2,i]=(System.Convert.ChangeType(m_arrayY[i]));
			}

			getMinMax();
		}

		//private void drawData(int x, int y)
		private void drawData()
		{
			if (m_XYdata != null)
			{
				//adjust to coordinate system
				int x=0, y=0;
				float xDataAxisRange = m_xDataAxisEnd - m_xDataAxisStart;
				float yDataAxisRange = m_yDataAxisEnd - m_yDataAxisStart;

				for(int i=0; i<=m_XYdata.GetUpperBound(1);i++)
				{
					//now scale data points when plotting
					//get x point
					//xscaled = axisrange*(x/datarangex)
					x = System.Convert.ToInt16((m_xAxisEnd-m_xAxisStart)*(m_XYdata[0,i]/(xDataAxisRange)));
					
					//add offset
					x = x + offsetX;
					
					//get y point
					y = System.Convert.ToInt16(
						(m_yAxisStart-m_yAxisEnd)*(m_XYdata[1,i]/(yDataAxisRange)));

					//add offset
					y = offsetY - y;

					// Get # of cyl as string
					string strZ = System.Convert.ToString(m_XYdata[2,i]);

                    using (Graphics g = pnlViz.CreateGraphics())
					{
                        int ptSize = Int16.Parse((cboPointSize.GetItemText(cboPointSize.SelectedItem)));

                        Pen myPen = new Pen(m_DataPtColor, 1);
                        //System.Drawing.
                        SolidBrush myBrush = new SolidBrush(m_DataPtColor);
                        
                        String myString = cboPointType.GetItemText(cboPointType.SelectedItem).ToLower();

                        if (myString == "circle")
                        {
                            // Open circle
                            g.DrawEllipse(myPen, x, y, ptSize, ptSize);
                        }

                        if (myString == "circle - filled")
                        {
                            // Filled circle
                            g.FillEllipse(myBrush, new Rectangle(x, y, ptSize, ptSize));
                        }

                        if (myString == "square")
                        {
                            // Square
                            g.DrawRectangle(myPen, x, y, ptSize, ptSize);
                        }

                        if (myString == "square - filled")
                        {
                            g.FillRectangle(myBrush, new Rectangle(x, y, ptSize, ptSize));
                        }

						// Draw Z dimension
						float fntSizef;
						Color myColor;

                        if (chkDataLabel.Checked == true)
                        {
                            // Set font size and color based on # of cylinders
                            switch (System.Convert.ToInt16((m_XYdata[2, i])))
                            {
                                case 4:
                                    fntSizef = 10;
                                    myColor = Color.LightBlue;
                                    break;
                                case 6:
                                    fntSizef = 12;
                                    myColor = Color.Blue;
                                    break;
                                case 8:
                                    fntSizef = 14;
                                    myColor = Color.MediumBlue;
                                    break;
                                case 10:
                                    fntSizef = 16;
                                    myColor = Color.DarkSlateBlue;
                                    break;
                                case 12:
                                    fntSizef = 18;
                                    myColor = Color.DarkBlue;
                                    break;
                                default:
                                    fntSizef = 10;
                                    myColor = Color.Blue;
                                    break;
                            }
                            // Draw string for # of cylinders
                            Font fnt = new Font("Verdana", fntSizef);
                            g.DrawString(strZ, fnt, new SolidBrush(myColor), x - 10, y - 10);
                        }
					}
				}
			}
		}

		private void getMinMax()
		{
			if (m_XYdata != null)
			{
				//adjust to coordinate system
				//int xMin, yMin, xMax, yMax;

				m_xMin = m_XYdata[0,0];
				for(int i=1; i<=m_XYdata.GetUpperBound(1);i++)
				{
					if (m_XYdata[0,i] < m_xMin)
					{
						m_xMin = m_XYdata[0,i];
					}
				}
				m_yMin = m_XYdata[1,0];
				for(int i=1; i<=m_XYdata.GetUpperBound(1);i++)
				{
					if (m_XYdata[1,i] < m_yMin)
					{
						m_yMin = m_XYdata[1,i];
					}
				}
				m_xMax = m_XYdata[0,0];
				for(int i=1; i<=m_XYdata.GetUpperBound(1);i++)
				{
					if (m_XYdata[0,i] > m_xMax)
					{
						m_xMax = m_XYdata[0,i];
					}
				}
				m_yMax = m_XYdata[1,0];
				for(int i=1; i<=m_XYdata.GetUpperBound(1);i++)
				{
					if (m_XYdata[1,i] > m_yMax)
					{
						m_yMax = m_XYdata[1,i];
					}
				}
				
				// Display results
				lblMinMax.Text = "Y Range:  " + m_yMin  + " to " + m_yMax + "\nX Range:  " + m_xMin + " to " + m_xMax;

				// Set overall axis range of imported data based on data min max
				float xDataRange = m_xMax - m_xMin;
				float yDataRange = m_yMax - m_yMin;
				m_xDataAxisStart = m_xMin - System.Convert.ToSingle(.5*m_xMin);
				m_xDataAxisEnd = m_xMax + System.Convert.ToSingle((2*m_xMin));
				m_yDataAxisStart= m_yMin - System.Convert.ToSingle((.5*m_yMin));
				m_yDataAxisEnd= m_yMax + System.Convert.ToSingle((2*m_yMin));
			}
		}



        private void pnlViz_Paint(object sender, PaintEventArgs e)
        {
            // Redraw screen when form re-paints
            drawXYaxis();
            drawData();
        }

        private void chkDataLabel_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnDataPtColor_Click(object sender, EventArgs e)
        {
            ColorDialog cdl = new ColorDialog();
            cdl.ShowDialog();
            m_DataPtColor = cdl.Color;
        }


        private void btnDrawViz_Click(object sender, EventArgs e)
        {
            // First clear the canvas
            Graphics g = pnlViz.CreateGraphics();
            g.Clear(pnlViz.BackColor);
            g.Dispose();

            // Draw viz
            drawXYaxis();
            drawData();
        }

        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAbout fAbout = new frmAbout();
            fAbout.ShowDialog();
        }

	}
}
