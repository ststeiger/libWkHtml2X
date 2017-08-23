using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestWkHtmlToX
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var bm = new System.Drawing.Bitmap(1, 1);
            System.Console.WriteLine(bm.HorizontalResolution);
            System.Console.WriteLine(bm.VerticalResolution);

        }
    }
}
