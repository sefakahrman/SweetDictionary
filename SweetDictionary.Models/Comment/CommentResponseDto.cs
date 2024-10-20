
namespace SweetDictionary.Models.Comment;

public sealed record CommentResponseDto(Guid id,string text, Guid postid,Guid userid);