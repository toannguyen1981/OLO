using OLO.By.Toan.Nguyen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLO.By.Toan.Nguyen.Interfaces
{
    public interface IPizza
    {
        Task<string> GetJSONDataAsync(string url);
        List<Pizza> ConvertToPizzaObject(string jsonData);
        List<Pizza> ReturnTopNPizzas(List<Pizza> pizzaList, int top);
    }
}
