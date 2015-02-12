using System;
using Microsoft.Practices.Unity;
using PriceBasket.Common.Interfaces;

namespace PriceBasket.ToConsole
{
    public class BasketToConsole : IGenerateReceipt
    {
        [Dependency] public IBasket Basket { get; set; }

        public void ProduceReceipt(string[] stringProducts)
        {
            Array.ForEach(Basket.GenerateReceipt(stringProducts), Console.WriteLine);
        }
    }
}