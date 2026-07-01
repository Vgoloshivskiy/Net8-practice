using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly ApplicationDBContext _context;
        public CommentController(ApplicationDBContext context, ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepository.GetAllCommentsAsync();
            return Ok(comments.Select(comment => comment.ToCommentDto()).ToList());
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockid:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockid, [FromBody] CommentCreateDto commentDto)
        {
            if(!await _stockRepository.StockExistsAsync(stockid))
            {
                return NotFound($"Stock with ID {stockid} not found.");
            }
            var comment = await _commentRepository.CreateCommentAsync(commentDto.ToCommentFromRequest(stockid));
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment.ToCommentDto());
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CommentUpdateDto commentDto)
        {
            var comment = await _commentRepository.UpdateCommentAsync(id, commentDto.ToCommentFromUpdateRequest());
            if (comment == null)
            {
                return NotFound("Comment not found.");
            }
            return Ok(comment.ToCommentDto());
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteCommentAsync(id);
            if (comment == null)
            {
                return NotFound("Comment not found.");
            }
            return Ok("Comment deleted successfully.");
        }
    }
}