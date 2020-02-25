using Newtonsoft.Json;
using OLO.By.Toan.Nguyen.Interfaces;
using OLO.By.Toan.Nguyen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OLO.By.Toan.Nguyen.Helpers
{
    public class Common : IPizza
    {
        public async Task<string> GetJSONDataAsync(string url)
        {
            string output = null;
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                request.Method = WebRequestMethods.Http.Get;
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    output = reader.ReadToEnd();
                    response.Close();
                }
            }
            catch
            {
                throw;
            }

            return output;
        }

        public List<Pizza> ConvertToPizzaObject(string jsonData)
        {
            List<Pizza> pizzaList = null;
            try
            {
                pizzaList = JsonConvert.DeserializeObject<List<Pizza>>(jsonData);
            }
            catch
            {
                throw;
            }

            return pizzaList;
        }

        public List<Pizza> ReturnTopNPizzas(List<Pizza> request, int top)
        {
            List<Pizza> pizzaList = null;
            try
            {
                IDictionary<string, Pizza> pizzaDictionary = new Dictionary<string, Pizza>();

                foreach(Pizza pizza in request)
                {
                    pizza.Toppings.Sort();
                    string key = String.Concat(pizza.Toppings);

                    if(!pizzaDictionary.ContainsKey(key))
                    {
                        pizza.Occurences = 1;
                        pizzaDictionary.Add(key, pizza);
                    }
                    else
                    {
                        var exist = pizzaDictionary[key];
                        exist.Occurences++;
                    }
                }

                pizzaList = pizzaDictionary.OrderByDescending(i => i.Value.Occurences)
                    .GroupBy(i => i.Value.Occurences)
                    .SelectMany((j, i) => j.Select(k => new Pizza { Toppings = k.Value.Toppings, Occurences = k.Value.Occurences, Rank = i + 1 }))
                    .Take(top).ToList();
            }
            catch
            {
                throw;
            }

            return pizzaList;
        }
    }
}
