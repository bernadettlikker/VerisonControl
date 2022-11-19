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
        RealEstateEntities re = new RealEstateEntities();

        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül

        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
                   
        }

        private void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add();
                xlSheet = xlWB.ActiveSheet;

                CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg);
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }


        }
        private void CreateTable()
        {
            string[] headers = new string[]
            {
                "Kód",
                "Eladó",
                "Oldal",
                "Kerület",
                "Lift",
                "Szobák száma",
                "Alapterület (m2)",
                "Ár (mFt)",
                "Négyzetméter ár (Ft/m2)"
            };

            for (int i = 1; i < headers.Length; i++)
                xlSheet.Cells[1, i] = headers[i - 1];

            object[,] values = new object[Flats.Count, headers.Length];
            int counter = 0;
            foreach (Flat item in Flats)
            {
                values[counter, 0] = item.Code;
                values[counter, 1] = item.Vendor;
                values[counter, 2] = item.Side;
                values[counter, 3] = item.District;
                values[counter, 4] = item.Elevator;
                values[counter, 5] = item.NumberOfRooms;
                values[counter, 6] = item.FloorArea;
                values[counter, 7] = item.Price;
                values[counter, 8] = "=" + GetCell(counter + 2, 8) + "*1000000/" + GetCell(counter + 2, 7);
                counter++;

            }



            void LoadData()
            {
                Flats = re.Flat.ToList();
            }

        
    }
}
