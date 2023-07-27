using Backend.Application.Dto;
using Backend.Application.Interfaces;

namespace Backend.Application.UseCases.GetItems;

public class GetUsersHandler
{
    private readonly IUserRepository _userRepository;

    public GetUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IReadOnlyCollection<UserDto>> GetUsersAsync()
    {
        var users = await _userRepository.SelectManyAsync();

        return users.Select(user => new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        }).AsParallel().ToList();
    }
}