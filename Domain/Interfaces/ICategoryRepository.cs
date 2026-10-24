using Domain.Models;
using MiniShop.Models;

namespace Domain.Interfaces;

public interface ICategoryRepository
{
    List<Category> GetAll();
    Category? GetById(int id);
    int Add(Category category);
    bool Update(Category category);
    bool Delete(int id);
}
