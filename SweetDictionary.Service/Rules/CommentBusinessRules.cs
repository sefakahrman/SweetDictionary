
using Core.Exceptions;
using SweetDictionary.Repository.Repositories.Abstracts;

namespace SweetDictionary.Service.Rules;

public class CommentBusinessRules
{
    private readonly ICommentRepository _commentRepository;

    public CommentBusinessRules(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public void CommentIsPresent(Guid id)
    {
        var comment = _commentRepository.GetById(id);
        if (comment is null)
        {
            throw new NotFoundException($"İlgili id'ye göre yorum bulunamadı: {id}");
        }
    }
}
