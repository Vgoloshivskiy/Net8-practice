using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Repository;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController] 
        public class StockController : ControllerBase
        {
            private readonly IStockRepository _stockRepository;
            private readonly ApplicationDBContext _context;
            public StockController(ApplicationDBContext context, IStockRepository stockRepository)
            {
                _stockRepository = stockRepository;
                _context = context;
            }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllStocksAsync();
            var stockDtos = stocks.Select(stock => stock.ToStockDto()).ToList();
            return Ok(stockDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStock([FromRoute] int id)
        {
            var stock = await _stockRepository.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
        {
            var stockmodel = stockDto.ToStockFromRequest();
            await _stockRepository.CreateStockAsync(stockmodel);
            return CreatedAtAction(nameof(GetStock), new { id = stockmodel.Id }, stockmodel.ToStockDto());
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            var stock = await _stockRepository.UpdateStockAsync(id, stockDto);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await _stockRepository.DeleteStockAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return NoContent();
        }   
    }
}