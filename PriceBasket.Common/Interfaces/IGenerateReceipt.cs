namespace PriceBasket.Common.Interfaces
{
    public interface IGenerateReceipt
    {
        void ProduceReceipt(string[] basketDetails);
    }
}
