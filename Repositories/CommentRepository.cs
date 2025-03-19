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

    public Task<Comment> CreateAsync(Comment comment)
    {
        
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comment.FindAsync(id);
    }

    public async Task<List<Comment>> GetCommentsAsync()
    {
        return await _context.Comment.ToListAsync();

    }
}