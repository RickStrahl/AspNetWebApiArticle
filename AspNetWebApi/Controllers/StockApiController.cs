using System.Web.Http;
using Westwind.StockServer;
using System.Json;
using System;
using System.Collections.Generic;

namespace AspNetWebApi.Controllers
{
    public class StockApiController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public StockQuote GetStockQuote(string symbol)
        {
            var stocks = new StockServer();                    
            return stocks.GetStockQuote(symbol);
        }
        public StockQuote[] GetStockQuotes()
        {
            var stocks = new StockServer();

            // return a list of fixed stock symbols
            return stocks.GetStockQuotes(new string[] { "MSFT", "INTC", "IBM", "GLD", "SLW" });
        }


        //public object GetAnonymousType()
        //{
            //    return new { name = "Rick", company = "West Wind", entered= DateTime.Now };
            //}

            //public Dictionary<string,object> GetDictionary()
            //{
            //    return null;
            //}

            //public JsonValue GetJsonValue()
            //{
            //    dynamic json = new JsonObject();
            //    json.name = "Rick";
            //    json.company = "West Wind";
            //    json.entered = DateTime.Now;

            //    dynamic address = new JsonObject();
            //    address.street = "32 Kaiea";
            //    address.zip = "96779";
            //    json.address = address;

            //    dynamic phones = new JsonArray();
            //    json.phoneNumbers = phones;

            //    dynamic phone = new JsonObject();
            //    phone.type = "Home";
            //    phone.number = "808 123-1233";
            //    phones.Add(phone);

            //    phone = new JsonObject();
            //    phone.type = "Home";
            //    phone.number = "808 123-1233";
            //    phones.Add(phone);


            //    //var jsonString = json.ToString();

            //    return json;
            //}
               
    }
}