using Backend.Data;
using Backend.DTO;
using Backend.Interfaces;
using Backend.Models;

using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class StockRepository : IStockInterface
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context) 
        {
            _context = context; 

        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await  _context.SaveChangesAsync();
             return stock; 
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id); 

            if (stockModel is null)
            {
                return null; 
            } 
            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync(); 
            return stockModel;

        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stock.Include(s => s.Comment).FirstOrDefaultAsync(s => s.Id== id);
        

        }

        public async Task<List<Stock>> GetStocksAsync()
        {
            return await _context.Stock.Include(s => s.Comment).ToListAsync();
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stock.AnyAsync(s => s.Id == id); 
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDTO  updatedStock)
        {
            var existingStock = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id); 

            if (existingStock is null)
            {
                return null; 
            }


             existingStock.Symbol = updatedStock.Symbol; 
            existingStock.CompanyName = updatedStock.CompanyName;
            existingStock.Purchase = updatedStock.Purchase;
            existingStock.LastDiv = updatedStock.LastDiv;
            existingStock.Industry = updatedStock.Industry;
            existingStock.MarketCap = updatedStock.MarketCap; 

            await _context.SaveChangesAsync();
            return existingStock;
            
        }
    }
}