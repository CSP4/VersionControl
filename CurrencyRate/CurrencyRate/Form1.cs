using CurrencyRate.Entities;
using CurrencyRate.MNBServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace CurrencyRate
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<String> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();

            comboBox1.DataSource = Currencies;
            
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetCurrenciesRequestBody();
            var response = mnbService.GetCurrencies(request);
            var result = response.GetCurrenciesResult;

            var xml = new XmlDocument();
            xml.LoadXml(result);
            string curr = "";

            foreach (XmlElement element in xml.DocumentElement)
            {
                foreach (XmlElement childElement in element.ChildNodes)
                {
                    curr = childElement.InnerText;
                    Currencies.Add(curr);
                }  
            }

                RefreshData();
        }

        private void RefreshData()
        {
            Rates.Clear();

            GetExchangeRates();
            dataGridView1.DataSource = Rates;
            CreateChart();
        }

        private void CreateChart()
        {
            chartRateData.DataSource = Rates;
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;

            series.XValueMember = "Date";
            series.YValueMembers = "Value";

            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];

            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];

            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void GetExchangeRates()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = dateTimePicker1.Value.Year.ToString()+ "-"+dateTimePicker1.Value.Month.ToString() +"-"+ dateTimePicker1.Value.Day.ToString(),
                endDate = dateTimePicker2.Value.Year.ToString() + "-" + dateTimePicker2.Value.Month.ToString() + "-" + dateTimePicker2.Value.Day.ToString()
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;

            XmlDocument xml = new XmlDocument();

            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);

                rate.Date= DateTime.Parse(element.GetAttribute("date"));



                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null)
                    continue;
                rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0) rate.Value = value/(unit*100);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }


    }
}
