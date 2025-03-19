using Backend.Data;
using Backend.DTO;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; 

namespace Backend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StockController> _logger;
        private readonly IStockInterface _stockInterface; 

        public StockController(ApplicationDbContext context, ILogger<StockController> logger, IStockInterface stockInterface)
        {
            _context = context;
            _logger = logger;
            _stockInterface = stockInterface;
        }
        

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var stocks = await _stockInterface.GetStocksAsync();
            var stockDTO = stocks.Select(s => s.ToStockDTO());
            return Ok(stockDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock([FromRoute] int id)
        {
            if (id <= 0 ) {
                return BadRequest(); 

            }
            _logger.LogInformation("Id = {id}", id);
            var stock = await _context.Stock.FindAsync(id);
            if (stock is null)
            {
                return NotFound();

            }

            return Ok(stock.ToStockDTO());

        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDTO stockDTO )
        {
            if (stockDTO is null)
            {
                return BadRequest();
            }
            var stockModel = stockDTO.ToStockFromCreateDTO(); 
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStock), new { id = stockModel.Id}, stockModel); 
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDTO upatedStock)
        {
            var stockModel = await _stockInterface.UpdateStockAsync(id, upatedStock);
            if(stockModel is null)
            {
                return NotFound();
            }
            
            return Ok(stockModel.ToStockDTO()); 

        }
        [HttpDelete("{id}")]
        public async  Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stockModel = await _stockInterface.DeleteStockAsync(id); 

            if(stockModel is null)
            {
                return NotFound();
            }
            

            return NoContent();

        }
    }
}