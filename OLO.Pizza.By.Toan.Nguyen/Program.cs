using OLO.By.Toan.Nguyen.Helpers;
using OLO.By.Toan.Nguyen.Interfaces;
using OLO.By.Toan.Nguyen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLO.By.Toan.Nguyen
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string jsonURL = "http://files.olo.com/pizzas.json";
                const int top = 20;
                IPizza pizza = new Common();
                string jsonString = pizza.GetJSONDataAsync(jsonURL).GetAwaiter().GetResult();
                List<Pizza> pizzaList = pizza.ConvertToPizzaObject(jsonString);
                List<Pizza> top20List = pizza.ReturnTopNPizzas(pizzaList, top);
                
                foreach(Pizza p in top20List)
                    Console.WriteLine($"Rank: {p.Rank}, Occurence: {p.Occurences}, Toppings: {String.Concat(p.Toppings)}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error detected: {ex.Message} \n Stack Trace: \n {ex.StackTrace}");
            }
        }
    }
}
