using Core.Entities;
using SweetDictionary.Models.Entities;
using SweetDictionary.Models.Posts;

namespace SweetDictionary.Service.Abstract;

public interface IPostService
{
    ReturnModel<List<PostResponseDto>> GetAll();
    ReturnModel<PostResponseDto> GetById(Guid id);
    ReturnModel<PostResponseDto> Add(CreatePostRequestDto dto);
    ReturnModel<string> Delete(Guid id);
    ReturnModel<PostResponseDto> Update(UpdatePostRequestDto dto);



    ReturnModel<List<PostResponseDto>> GetAllByCategoryId(int id);
    ReturnModel<List<PostResponseDto>> GetAllByAuthorId(long authorId);

    ReturnModel<List<PostResponseDto>> GetAllByTitleContains(string text);




}
