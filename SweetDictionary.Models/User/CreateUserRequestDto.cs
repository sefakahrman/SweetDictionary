
namespace SweetDictionary.Models.User;

public sealed record CreateUserRequestDto(string firstname,string lastname,string email,string username,string password);
