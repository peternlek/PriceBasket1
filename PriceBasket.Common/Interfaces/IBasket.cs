namespace PriceBasket.Common.Interfaces
{
    public interface IBasket
    {
        string[] GenerateReceipt(string[] stringProducts);
    }
}
