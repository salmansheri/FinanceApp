using Backend.Models; 

namespace Backend.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetCommentsAsync(); 
    Task<Comment?> GetByIdAsync(int id); 
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> UpdateAsync(int id, Comment comment); 
    Task<Comment?> DeleteAsync(int id); 


}