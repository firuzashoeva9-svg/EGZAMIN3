using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public int Add(Product product)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "insert into products (name, price, quantity, category_id) values (@name, @price, @quantity, @categoryId) returning id";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@name", product.Name);
        cmd.Parameters.AddWithValue("@price", product.Price);
        cmd.Parameters.AddWithValue("@quantity", product.Quantity);
        cmd.Parameters.AddWithValue("@categoryId", product.CategoryId);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public bool Delete(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "delete from products where id = @id";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);
        return cmd.ExecuteNonQuery() > 0;
    }

    public List<Product> Filter(int? categoryId, decimal? minPrice, decimal? maxPrice)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetAll()
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "select id, name, price, quantity, category_id from products order by id";
        using var cmd = new NpgsqlCommand(query, connection);
        using var data = cmd.ExecuteReader();
        var products = new List<Product>();
        while (data.Read())
        {
            var product = new Product();
            product.Id = data.GetInt32(0);
            product.Name = data.GetString(1);
            product.Price = data.GetDecimal(2);
            product.Quantity = data.GetInt32(3);
            product.CategoryId = data.GetInt32(4);
            products.Add(product);
        }
        return products;
    }

    public Product? GetById(int id)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "select id, name, price, quantity, category_id from products where id = @id";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);
        using var data = cmd.ExecuteReader();
        Product? product = null;
        while (data.Read())
        {
            product = new Product();
            product.Id = data.GetInt32(0);
            product.Name = data.GetString(1);
            product.Price = data.GetDecimal(2);
            product.Quantity = data.GetInt32(3);
            product.CategoryId = data.GetInt32(4);
        }
        return product;
    }

    public List<Product> SearchByName(string term)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "select id, name, price, quantity, category_id from products where name ilike @term order by id";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@term", "%" + term + "%");
        using var data = cmd.ExecuteReader();
        var products = new List<Product>();
        while (data.Read())
        {
            var product = new Product();
            product.Id = data.GetInt32(0);
            product.Name = data.GetString(1);
            product.Price = data.GetDecimal(2);
            product.Quantity = data.GetInt32(3);
            product.CategoryId = data.GetInt32(4);
            products.Add(product);
        }
        return products;
    }

    public bool Update(Product product)
    {
        using var connection = Database.Connection();
        connection.Open();
        var query = "update products set name = @name, price = @price, quantity = @quantity, category_id = @categoryId where id = @id";
        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@name", product.Name);
        cmd.Parameters.AddWithValue("@price", product.Price);
        cmd.Parameters.AddWithValue("@quantity", product.Quantity);
        cmd.Parameters.AddWithValue("@categoryId", product.CategoryId);
        cmd.Parameters.AddWithValue("@id", product.Id);
        return cmd.ExecuteNonQuery() > 0;
    }
}