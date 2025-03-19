﻿using Backend.DTO;
using Backend.Models;

namespace Backend.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedOn = commentModel.CreatedOn,
            StockId = commentModel.StockId,
            
        }; 

    }

    public static Comment ToCommentFromCreate(this CreateCommentDto commentModel, int stockId)
    {
        return new Comment
        {
          
            Title = commentModel.Title,
            Content = commentModel.Content,
            
            StockId = stockId
             
        }; 

    }

    public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentModel)
    {
        return new Comment
        {
            Title = commentModel.Title,
            Content = commentModel.Content
        }; 
        
    }
}