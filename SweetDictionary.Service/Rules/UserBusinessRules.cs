
using Core.Exceptions;
using SweetDictionary.Repository.Repositories.Abstracts;

namespace SweetDictionary.Service.Rules;

public class UserBusinessRules
{
    private readonly IUserRepository _userRepository;

    public UserBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void UserIsPresent(long id)
    {
        var user = _userRepository.GetById(id);
        if (user is null)
        {
            throw new NotFoundException($"İlgili id'ye göre kullanıcı bulunamadı: {id}");
        }
    }
}
