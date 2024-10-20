
namespace SweetDictionary.Models.User;

public sealed record UpdateUserRequestDto(Guid id,string firstname,string lastname,string email,string username,string password);
