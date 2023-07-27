using Backend.Application.Dto;
using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.GetItems;

public class GetUserByIdHandler
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> GetUserById(Guid id)
    {
        var user = await _userRepository.SelectAsync(id);

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };
    }
}