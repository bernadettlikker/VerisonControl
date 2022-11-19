using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace BpLakaspiac
{
    public partial class Form1 : Form
    {
        List<Flat> Flats;
        RealEstateEntities context = new RealEstateEntities();

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            Flats = context.Flat.ToList();
        }


    }
}
