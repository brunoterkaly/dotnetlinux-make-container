using AspNetCoreWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace AspNetCoreWebApp.Controllers
{
    public class DataDownloader : IDataDownloader
    {
        public DataDownloader()
        {
        }

        public List<StockViewModel> GetData()
        {
            string url = "https://www.quandl.com/api/v3/datasets/EOD/{0}?start_date=2018-11-20&end_date=2018-11-20&api_key={1}";

            // Choose any tickers you want
            List<string> tickers = new List<string> { "MSFT", "IBM", "AAPL" };

            // This holds all the data
            List<StockViewModel> objList = new List<StockViewModel>();
            using (WebClient wc = new WebClient())
            {
                // Loop through all ticker symbols
                foreach (var item in tickers)
                {
                    string currUrl = String.Format(url, item, apiKey);
                    var json = wc.DownloadString(currUrl);
                    RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
                    Regex regx = new Regex("<code data-language=\"ruby\">(?<theBody>.*)</code>", options);
                    Match match = regx.Match(json);

                    if (match.Success)
                    {
                        string theBody = match.Groups["theBody"].Value;
                        dynamic parsedData =  JsonConvert.DeserializeObject(theBody);
                        dynamic d1 = parsedData.dataset.data;
                        DateTime date = DateTime.Parse(d1[0][0].ToString());
                        double closeprice = Convert.ToDouble(d1[0][4].ToString());
                        double volume = Convert.ToDouble(d1[0][5].ToString());
                        // We have our data. Add it to the list
                        objList.Add(new StockViewModel { dateTime = date.ToShortDateString(),
                            ticker = item,
                            close = closeprice.ToString(),
                            volume = volume.ToString() });
                    }
                }
            }
            return objList;
        }
        private void OnDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            // Useful later if we get large downloads
        }

        private string apiKey = "fSEQmsyff_z54oRSaKWQ";
    }
}
