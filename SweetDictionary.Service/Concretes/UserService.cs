
using AutoMapper;
using Core.Entities;
using SweetDictionary.Models.Entities;
using SweetDictionary.Models.User;
using SweetDictionary.Repository.Repositories.Abstracts;
using SweetDictionary.Service.Abstract;
using SweetDictionary.Service.Rules;

namespace SweetDictionary.Service.Concretes;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly UserBusinessRules _businessRules;

    public UserService(IUserRepository userRepository, IMapper mapper, UserBusinessRules businessRules)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public ReturnModel<UserResponseDto> Add(CreateUserRequestDto dto)
    {
        User createdUser = _mapper.Map<User>(dto);
        createdUser.Id = Guid.NewGuid();

        User user = _userRepository.Add(createdUser);

        UserResponseDto response = _mapper.Map<UserResponseDto>(user);

        return new ReturnModel<UserResponseDto>
        {
            Data = response,
            Message = "Kullanıcı eklendi.",
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<string> Delete(long id)
    {
        _businessRules.UserIsPresent(id);

        User? user = _userRepository.GetById(id);
        User deletedUser = _userRepository.Delete(user);

        return new ReturnModel<string>
        {
            Data = $"Kullanıcı Adı: {deletedUser.Username}",
            Message = "Kullanıcı silindi.",
            Status = 204,
            Success = true
        };
    }

    public ReturnModel<string> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public ReturnModel<List<UserResponseDto>> GetAll()
    {
        var users = _userRepository.GetAll();
        List<UserResponseDto> responses = _mapper.Map<List<UserResponseDto>>(users);
        return new ReturnModel<List<UserResponseDto>>
        {
            Data = responses,
            Message = string.Empty,
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<UserResponseDto> GetById(Guid id)
    {
        try
        {
            _businessRules.UserIsPresent(id);

            var user = _userRepository.GetById(id);
            var response = _mapper.Map<UserResponseDto>(user);
            return new ReturnModel<UserResponseDto>
            {
                Data = response,
                Message = "İlgili kullanıcı gösterildi.",
                Status = 200,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return ExceptionHandler<UserResponseDto>.HandleException(ex);
        }
    }

    public ReturnModel<UserResponseDto> Update(UpdateUserRequestDto dto)
    {
        try
        {
            _businessRules.UserIsPresent(dto.Id);

            User user = _mapper.Map<User>(dto);
            User updated = _userRepository.Update(user);

            UserResponseDto response = _mapper.Map<UserResponseDto>(updated);

            return new ReturnModel<UserResponseDto>
            {
                Data = response,
                Message = "Kullanıcı güncellendi.",
                Status = 200,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return ExceptionHandler<UserResponseDto>.HandleException(ex);
        }
    }
}
