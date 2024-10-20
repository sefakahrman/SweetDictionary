
using AutoMapper;
using Core.Entities;
using SweetDictionary.Models.Comment;
using SweetDictionary.Models.Entities;
using SweetDictionary.Repository.Repositories.Abstracts;
using SweetDictionary.Service.Abstract;
using SweetDictionary.Service.Rules;

namespace SweetDictionary.Service.Concretes;

public sealed class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly CommentBusinessRules _businessRules;

    public CommentService(ICommentRepository commentRepository, IMapper mapper, CommentBusinessRules businessRules)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public ReturnModel<CommentResponseDto> Add(CreateCommentRequestDto dto)
    {
        Comment createdComment = _mapper.Map<Comment>(dto);
        createdComment.Id = Guid.NewGuid();

        Comment comment = _commentRepository.Add(createdComment);

        CommentResponseDto response = _mapper.Map<CommentResponseDto>(comment);

        return new ReturnModel<CommentResponseDto>
        {
            Data = response,
            Message = "Yorum eklendi.",
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<string> Delete(Guid id)
    {
        _businessRules.CommentIsPresent(id);

        Comment? comment = _commentRepository.GetById(id);
        Comment deletedComment = _commentRepository.Delete(comment);

        return new ReturnModel<string>
        {
            Data = $"Yorum İçeriği: {deletedComment.Content}",
            Message = "Yorum silindi.",
            Status = 204,
            Success = true
        };
    }

    public ReturnModel<List<CommentResponseDto>> GetAll()
    {
        var comments = _commentRepository.GetAll();
        List<CommentResponseDto> responses = _mapper.Map<List<CommentResponseDto>>(comments);
        return new ReturnModel<List<CommentResponseDto>>
        {
            Data = responses,
            Message = string.Empty,
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<CommentResponseDto> GetById(Guid id)
    {
        try
        {
            _businessRules.CommentIsPresent(id);

            var comment = _commentRepository.GetById(id);
            var response = _mapper.Map<CommentResponseDto>(comment);
            return new ReturnModel<CommentResponseDto>
            {
                Data = response,
                Message = "İlgili yorum gösterildi.",
                Status = 200,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return ExceptionHandler<CommentResponseDto>.HandleException(ex);
        }
    }

    public ReturnModel<CommentResponseDto> Update(UpdateCommentRequestDto dto)
    {
        try
        {
            _businessRules.CommentIsPresent(dto.Id);

            Comment comment = _mapper.Map<Comment>(dto);
            Comment updated = _commentRepository.Update(comment);

            CommentResponseDto response = _mapper.Map<CommentResponseDto>(updated);

            return new ReturnModel<CommentResponseDto>
            {
                Data = response,
                Message = "Yorum güncellendi.",
                Status = 200,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return ExceptionHandler<CommentResponseDto>.HandleException(ex);
        }
    }
}

