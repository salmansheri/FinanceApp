using Backend.DTO;
using Backend.Helpers;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IStockInterface
    {
        Task<List<Stock>> GetStocksAsync(QueryObject query);
        Task<Stock?> GetStockByIdAsync(int id); 
        Task<Stock> CreateStockAsync(Stock stock); 
        Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDTO updatedStock); 
        Task<Stock?> DeleteStockAsync(int id);
        Task<bool> StockExists(int id); 

        
    }
}