using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using SweetDictionary.Models.Entities;
using SweetDictionary.Models.Posts;
using SweetDictionary.Repository.Repositories.Abstracts;
using SweetDictionary.Service.Abstract;
using SweetDictionary.Service.Rules;

namespace SweetDictionary.Service.Concretes;

public sealed class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private readonly PostBusinessRules _businessRules;

    public PostService(IPostRepository postRepository, IMapper mapper, PostBusinessRules businessRules)
    {
        _postRepository = postRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public ReturnModel<PostResponseDto> Add(CreatePostRequestDto dto)
    {
        Post createdPost = _mapper.Map<Post>(dto);
        createdPost.Id = Guid.NewGuid();

        Post post = _postRepository.Add(createdPost);

        PostResponseDto response = _mapper.Map<PostResponseDto>(post);

        return new ReturnModel<PostResponseDto>
        {
            Data = response,
            Message = "Post eklendi.",
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<string> Delete(Guid id)
    {
        _businessRules.PostIsPresent(id);



        Post? post = _postRepository.GetById(id);
        Post deletedPost = _postRepository.Delete(post);

        return new ReturnModel<string>
        {
            Data = $"Post Başlığı : {deletedPost.Title}",
            Message = "Post Silindi",
            Status = 204,
            Success = true
        };
    }

    public ReturnModel<List<PostResponseDto>> GetAll()
    {
        var posts = _postRepository.GetAll();
        List<PostResponseDto> responses = _mapper.Map<List<PostResponseDto>>(posts);
        return new ReturnModel<List<PostResponseDto>>
        {
            Data = responses,
            Message = string.Empty,
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<PostResponseDto> GetById(Guid id)
    {
        try
        {
            _businessRules.PostIsPresent(id);

            var post = _postRepository.GetById(id);
            var response = _mapper.Map<PostResponseDto>(post);
            return new ReturnModel<PostResponseDto>
            {
                Data = response,
                Message = "İlgili post gösterildi",
                Status = 200,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return ExceptionHandler<PostResponseDto>.HandleException(ex);
        }

    }

    public ReturnModel<PostResponseDto> Update(UpdatePostRequestDto dto)
    {
        try
        {
            _businessRules.PostIsPresent(dto.Id);

            Post post = _mapper.Map<Post>(dto);
            Post updated = _postRepository.Update(post);

            PostResponseDto response = _mapper.Map<PostResponseDto>(updated);

            return new ReturnModel<PostResponseDto>
            {
                Data = response,
                Message = "Post Güncellendi.",
                Status = 200,
                Success = true
            };

        }
        catch (Exception ex)
        {
            return ExceptionHandler<PostResponseDto>.HandleException(ex);
        }



    }



    public ReturnModel<List<PostResponseDto>> GetAllByAuthorId(long authorId)
    {
        List<Post> posts = _postRepository.GetAllByAuthorId(authorId);
        List<PostResponseDto> responses = _mapper.Map<List<PostResponseDto>>(posts);

        return new ReturnModel<List<PostResponseDto>>
        {
            Data = responses,
            Message = $"Yazar Id sine göre Postlar listelendi : Yazar Id: {authorId}",
            Status = 200,
            Success = true
        };
        
    }

    public ReturnModel<List<PostResponseDto>> GetAllByCategoryId(int id)
    {
        List<Post> posts = _postRepository.GetAllByCategoryId(id);
        List<PostResponseDto> responses = _mapper.Map<List<PostResponseDto>>(posts);
        return new ReturnModel<List<PostResponseDto>>
        {
            Data = responses,
            Message = $"Kategori Id sine göre Postlar listelendi : Kategori Id: {id}",
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<List<PostResponseDto>> GetAllByTitleContains(string text)
    {
        var posts = _postRepository.GetAllByTitleContains(text);
        var responses = _mapper.Map<List<PostResponseDto>>(posts);
        return new ReturnModel<List<PostResponseDto>>
        {
            Data = responses,
            Message = string.Empty,
            Status = 200,
            Success = true 
        };
    }



}
