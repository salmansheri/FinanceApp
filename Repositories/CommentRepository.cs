using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context; 

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context; 

    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _context.Comment.AddAsync(comment); 
        await _context.SaveChangesAsync(); 
        return comment; 
        
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await _context.Comment.FirstOrDefaultAsync(c => c.Id == id);  

        if (comment == null)
        {
            return null; 
        }

        _context.Comment.Remove(comment); 
        await _context.SaveChangesAsync(); 
        return comment; 
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comment.FindAsync(id);
    }

    public async Task<List<Comment>> GetCommentsAsync()
    {
        return await _context.Comment.ToListAsync();

    }

    public async Task<Comment?> UpdateAsync(int id, Comment comment)
    {
        var existingComment = await _context.Comment.FindAsync(id); 

        if (existingComment == null)
        {
            return null;
        }

        existingComment.Title = comment.Title; 
        existingComment.Content = comment.Content; 

        await _context.SaveChangesAsync();
        return existingComment; 


    }
}