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

namespace MNB_arfolyamok
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody() //új példányt hoztunk létre
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            GetExchangeRatesRequestBody response = mnbService.GetExchangeRates(request);
            string result = response.GetExchangesRateResult;
            //MessageBox.Show(result);
        }
    }
}
