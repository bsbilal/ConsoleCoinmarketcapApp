using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web;

class CSharpExample
{
    private static string API_KEY = "abfae75d-ac8c-4516-9834-851de52f3ab5";

    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Write your coin name ex(BTC or XRP) :");
            string shortName = Console.ReadLine();

            dynamic stuff = JObject.Parse(makeAPICall());
           foreach (var item in stuff.data)
            {
               
                if(shortName== Convert.ToString(item.symbol))
                { 
                Console.WriteLine("Name:{0} Symbol: {1} USD Price: {2} Last Update: {3} \n", item.name, item.symbol,
                    item.quote.USD.price, item.quote.USD.last_updated);
                }
            }
  

        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    static string makeAPICall()
    {
        var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["start"] = "1";
        queryString["limit"] = "4";
        queryString["convert"] = "USD";
     

        URL.Query = queryString.ToString();

        var client = new WebClient();
        client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
        client.Headers.Add("Accepts", "application/json");
        return client.DownloadString(URL.ToString());

    }

}

