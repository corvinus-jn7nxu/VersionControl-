﻿using System;
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
using week06.Entities;
using week06.MnbServiceReference;

namespace week06
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();
            _getCurr();
            comboBox1.DataSource = Currencies;
            //comboBox1.DisplayMember = "Currency";
            RefreshData();
        }
        string _getCurr()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetCurrenciesRequestBody()
            {
            };
            var response = mnbService.GetCurrencies(request);
            var Currresult = response.GetCurrenciesResult;
            Console.WriteLine(Currresult);
            //XMLProcessing(result);
            return Currresult;
        }

        void CurrXMLProcessing(string result)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            var itemC = xml.DocumentElement;
            var childElement = (XmlElement)itemC.ChildNodes[0];
            foreach (XmlElement item in childElement)
            {
                string curr;
                
                //rd.Date = DateTime.Parse(item.GetAttribute("date"));
                //var childElement = (XmlElement)item.ChildNodes[0];
                if (childElement == null)
                    continue;
                //curr = childElement.GetAttribute("curr");
                curr = item.InnerText;
                Currencies.Add(curr);

            }
        }

        string CallService()
        {
            
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.Text,
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
            Console.WriteLine(result);
            //XMLProcessing(result);
            return result;
        }


        void XMLProcessing(string result)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            foreach (XmlElement item in xml.DocumentElement)
            {
                RateData rd = new RateData();
                Rates.Add(rd);
                rd.Date = DateTime.Parse(item.GetAttribute("date"));
                var childElement = (XmlElement)item.ChildNodes[0];
                if (childElement == null)
                    continue;
                rd.Currency = childElement.GetAttribute("curr");

                // Érték
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rd.Value = value / unit;
            }
        }

        void Graph()
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

        void RefreshData()
        {
            Rates.Clear();
            dataGridView1.DataSource = Rates;
            var asd = CallService();
            var Curr = _getCurr();
            XMLProcessing(asd);
            CurrXMLProcessing(Curr);
            Graph();
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
