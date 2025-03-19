using Backend.DTO;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController: ControllerBase
{
    private readonly ICommentRepository _commentRepo;
    private readonly IStockInterface _stockRepo;

    public CommentController(ICommentRepository commentRepo, IStockInterface stockRepo)
    {
        _commentRepo = commentRepo; 
        _stockRepo = stockRepo; 
    }

    [HttpGet]
    public async Task<IActionResult> GetComments()
    {
        var comments = await _commentRepo.GetCommentsAsync();
        var commentDto = comments.Select(s => s.ToCommentDto()); 
        return Ok(commentDto);


    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetById([FromRoute] int id)
    {
        var comment = await _commentRepo.GetByIdAsync(id); 

        if (comment == null)
        {
            return NotFound(); 
        }

        return Ok(comment.ToCommentDto()); 
    }

    [HttpPost("{stockId}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto comment)
    {
        if (!await _stockRepo.StockExists(stockId))
        {
            return BadRequest("Stock does not exist"); 
        }

        var commentModel = comment.ToCommentFromCreate(stockId); 
        await _commentRepo.CreateAsync(commentModel); 

        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id}, commentModel.ToCommentDto()); 

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateComment)

    {
        var comment= await _commentRepo.UpdateAsync(id,updateComment.ToCommentFromUpdate()); 

        if (comment == null)
        {
            return NotFound(); 
        }

        return Ok(comment.ToCommentDto()); 

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var comment  = await _commentRepo.DeleteAsync(id); 

        if(comment == null)
        {
            return NotFound(); 
        }

        return Ok(comment); 
        
    }
    
    
    
}