﻿using System;
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

namespace ExcelGeneralas
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;
        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;

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
        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
        }

        private void LoadData()
        {
            Flats = context.Flats.ToList();
        }

        private void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;

                CreateTabel();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errorMSG = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errorMSG, "Error");

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlSheet = null;
            }
        }

        private void CreateTabel()
        {


            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i+1] = headers[i];
            }

            object[,] values = new object[Flats.Count, headers.Length];

            int counter = 0;
            foreach (Flat f in Flats)
            {
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Vendor;
                values[counter, 2] = f.Side;
                values[counter, 3] = f.District;

                if (f.Elevator) values[counter, 4] = "Van";
                else values[counter, 4] = "Nincs";
                    
                values[counter, 5] = f.NumberOfRooms;
                values[counter, 6] = f.FloorArea;
                values[counter, 7] = f.Price;
                values[counter, 8] = "="+GetCell(counter+2, 8)+"*1000000/"+ GetCell(counter+2, 7);

                counter++;
            }

            xlSheet.get_Range(GetCell(2, 1), GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;
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
            int lastRowID = xlSheet.UsedRange.Rows.Count;
            int lastColumnID = xlSheet.UsedRange.Columns.Count;

            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range wholeTableRange = xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID, lastColumnID));
            wholeTableRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range firstColumnRange = xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID, 1));
            firstColumnRange.Font.Bold = true;
            firstColumnRange.Interior.Color = Color.LightYellow;


            Excel.Range lastColumnRange = xlSheet.get_Range(GetCell(2, lastColumnID), GetCell(lastRowID, lastColumnID));
            lastColumnRange.Interior.Color = Color.LightGreen;
            lastColumnRange.NumberFormat = "0.00";

        }


    }
}
