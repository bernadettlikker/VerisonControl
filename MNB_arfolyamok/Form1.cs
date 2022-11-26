using MNB_arfolyamok.Entities;
using MNB_arfolyamok.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MNB_arfolyamok
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = Rates;

            //GetRates();

            ReadXml();
        }

        private void ReadXml()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(GetRates());
            foreach (XmlElement item in xml.DocumentElement) //végigmegy az xml minden elemén
            {
                RateData rd = new RateData();
                Rates.Add(rd);
                rd.Currency = item.ChildNodes[0].Attributes["curr"].Value; //childenodes-zal lejebb ugrik egyet
                rd.Date = Convert.ToDateTime(item.Attributes["date"].Value); //item date attribútúmát kiolvassa és konvertálja dátummá
                decimal unit = Convert.ToDecimal(item.ChildNodes[0].Attributes["unit"].Value);
                decimal value = Convert.ToDecimal(item.ChildNodes[0].InnerText);
                if (unit != 0)
                {
                    rd.Value = value / unit;
                }
                else { rd.Value = value; }
            }
        }

        private static string GetRates() //a string eredetileg void volt, eljárásból függvény lesz (+return lent, + xml.Loadxml...)
        {
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody() //új példányt hoztunk létre
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            GetExchangeRatesResponseBody response = mnbService.GetExchangeRates(request);
            string result = response.GetExchangeRatesResult;
            MessageBox.Show(result);
            return result;
        }
    }
}
