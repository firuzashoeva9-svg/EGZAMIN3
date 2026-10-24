using Domain.Interfaces;
using Infrastructure.Data;
using MiniShop.Models;
using Npgsql;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public int Add(Category category)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "insert into categories (name) values (@name) returning id";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@name", category.Name);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public bool Delete(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "delete from categories where id = @id";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);
        return cmd.ExecuteNonQuery() > 0;
    }

    public List<Category> GetAll()
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "select id, name from categories order by id";
        using var cmd = new NpgsqlCommand(query, connection);
        using var data = cmd.ExecuteReader();
        var categories = new List<Category>();
        while (data.Read())
        {
            var category = new Category();
            category.Id = data.GetInt32(0);
            category.Name = data.GetString(1);
            categories.Add(category);
        }
        return categories;
    }

    public Category? GetById(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "select id, name from categories where id = @id";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);
        using var data = cmd.ExecuteReader();
        Category? category = null;
        while (data.Read())
        {
            category = new Category();
            category.Id = data.GetInt32(0);
            category.Name = data.GetString(1);
        }
        return category;
    }

    public bool Update(Category category)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "update categories set name = @name where id = @id";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@name", category.Name);
        cmd.Parameters.AddWithValue("@id", category.Id);
        return cmd.ExecuteNonQuery() > 0;
    }
}