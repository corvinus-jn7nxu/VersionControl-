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

namespace week04
{
    public partial class Form1 : Form
    {

        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;
        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül
        
        string[] headers = new string[] {
                 "Kód",
                 "Eladó",
                 "Oldal",
                 "Kerület",
                 "Lift",
                 "Szobák száma",
                 "Alapterület (m2)",
                 "Ár (mFt)",
                 "Négyzetméter ár (Ft/m2)"};

        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
        }
        private void LoadData()
        {
            Flats = context.Flat.ToList();
        }

        private void CreateExcel()
        {

            try
            {
                xlApp = new Excel.Application(); // Excel elindítása és az applikáció objektum betöltése

                xlWB = xlApp.Workbooks.Add(Missing.Value); //új munkafüzet

                xlSheet = xlWB.ActiveSheet; // Új munkalap
                xlApp.Visible = true;
                xlApp.UserControl = true;
                CreateTable();

                
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        
        }

        public void CreateTable()
        {
            

            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[ 1, 1 + i] = headers[i];
            }

            object[,] values = new object[Flats.Count, headers.Length];
            int counter = 0;
            foreach (Flat item in Flats)
            {
                values[counter, 0] = item.Code;
                values[counter, 1] = item.Vendor;
                values[counter, 2] = item.Side;
                values[counter, 3] = item.District;
                if (item.Elevator)
                {
                    values[counter, 4] = "Van";
                }
                else
                {
                    values[counter, 4] = "Nincs";
                }               
                values[counter, 5] = item.NumberOfRooms;
                values[counter, 6] = item.FloorArea;
                values[counter, 7] = item.Price;
                values[counter, 8] = "";
                
                string Fnct = "=" + GetCell(counter + 2, 8) + "/" + GetCell(counter + 2, 7);
                xlSheet.Cells[counter + 2, 9] = Fnct;
                //xlSheet.get_Range(GetCell)
                counter++;
            }

            xlSheet.get_Range(
            GetCell(2, 1),
            GetCell(1 + values.GetLength(0), values.GetLength(1)-1)).Value2 = values;
            
            FormatTable();
        }
        
        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

        private void FormatTable()
        {
            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
            int lastRowID = xlSheet.UsedRange.Rows.Count;
            Excel.Range TableRange = xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID, headers.Length));
            TableRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
            Excel.Range FirstColRange = xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID, 1));
            FirstColRange.Font.Bold = true;
            FirstColRange.Interior.Color = Color.LightGoldenrodYellow;
            Excel.Range LastColRange = xlSheet.get_Range(GetCell(2, 9), GetCell(lastRowID, 9));
            LastColRange.Interior.Color = Color.LightGreen;
            LastColRange.NumberFormat = "0.##";
        }
    }
}
