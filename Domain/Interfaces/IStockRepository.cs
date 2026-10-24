namespace Domain.Interfaces;

public interface IStockRepository
{
    string RegisterSale(int productId, int qty);
    string Restock(int productId, int qty);
}