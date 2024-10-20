
namespace SweetDictionary.Models.Comment;

public sealed record UpdateCommentRequestDto(Guid id, string text, Guid postid, Guid userid);
