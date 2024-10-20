using Core.Entities;
using SweetDictionary.Models.Comment;


namespace SweetDictionary.Service.Abstract;

public interface ICommentService
{
    ReturnModel<List<CommentResponseDto>> GetAll();
    ReturnModel<CommentResponseDto> GetById(Guid id);
    ReturnModel<CommentResponseDto> Add(CreateCommentRequestDto dto);
    ReturnModel<string> Delete(Guid id);
    ReturnModel<CommentResponseDto> Update(UpdateCommentRequestDto dto);
}
