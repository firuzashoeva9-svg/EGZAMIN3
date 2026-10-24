using Domain.Models;

namespace Domain.Interfaces;

public interface IProductRepository
{
    List<Product> GetAll();
    Product? GetById(int id);
    int Add(Product product);
    bool Update(Product product);
    bool Delete(int id);
    List<Product> SearchByName(string term);
    List<Product> Filter(int? categoryId, decimal? minPrice, decimal? maxPrice);
}