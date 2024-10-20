
using Core.Entities;
using SweetDictionary.Models.User;

namespace SweetDictionary.Service.Abstract;


public interface IUserService
{
    ReturnModel<List<UserResponseDto>> GetAll();
    ReturnModel<UserResponseDto> GetById(Guid id);
    ReturnModel<UserResponseDto> Add(CreateUserRequestDto dto);
    ReturnModel<string> Delete(Guid id);
    ReturnModel<UserResponseDto> Update(UpdateUserRequestDto dto);
}
