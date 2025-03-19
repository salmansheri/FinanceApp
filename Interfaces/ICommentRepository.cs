using Backend.Models; 

namespace Backend.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetCommentsAsync(); 
    Task<Comment?> GetByIdAsync(int id); 
    Task<Comment> CreateAsync(Comment comment); 


}