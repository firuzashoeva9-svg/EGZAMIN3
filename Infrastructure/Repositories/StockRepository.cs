using Domain.Interfaces;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class StockRepository : IStockRepository
{
    public string RegisterSale(int productId, int qty)
    {
        throw new NotImplementedException();
    }

    public string Restock(int productId, int qty)
    {
        if (qty <= 0)
        {
            return "Quantity must be greater than 0.";
        }

        using var connection = Database.Connection();
        connection.Open();
        var selectQuery = "select quantity from products where id = @id for update";
        using var selectCmd = new NpgsqlCommand(selectQuery, connection);
        selectCmd.Parameters.AddWithValue("@id", productId);
        var result = selectCmd.ExecuteScalar();
        if (result == null)
        {
            return "Product not found.";
        }

        var stock = Convert.ToInt32(result);

        var updateQuery = "update products set quantity = quantity + @qty where id = @id";
        using var updateCmd = new NpgsqlCommand(updateQuery, connection);
        updateCmd.Parameters.AddWithValue("@qty", qty);
        updateCmd.Parameters.AddWithValue("@id", productId);
        updateCmd.ExecuteNonQuery();

        var insertQuery = "insert into stock_movements (product_id, change_qty) values (@id, @change)";
        using var insertCmd = new NpgsqlCommand(insertQuery, connection);
        insertCmd.Parameters.AddWithValue("@id", productId);
        insertCmd.Parameters.AddWithValue("@change", qty);
        insertCmd.ExecuteNonQuery();

        return "Restocked. New quantity: " + (stock + qty);
    }
}