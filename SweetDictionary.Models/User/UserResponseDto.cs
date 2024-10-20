
namespace SweetDictionary.Models.User;

public sealed record UserResponseDto(Guid id, string firstname,string lastname,string email,string username,string password);
