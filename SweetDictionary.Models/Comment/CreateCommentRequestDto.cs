
namespace SweetDictionary.Models.Comment;

public sealed record CreateCommentRequestDto(string text, Guid postid, Guid userid);
