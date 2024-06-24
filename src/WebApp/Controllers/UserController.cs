using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Services;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
  public class UsersController : Controller
  {
    private readonly UserService _userService;
    private readonly IRepository<UserEntity> _userRepository;

    public UsersController(UserService userService,
        IRepository<UserEntity> userRepository)
    {
      _userService = userService;
      _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<UserEntity>> GetUsers()
    {
      var users = await _userRepository.GetAllAsync();

      /* IEnumerable<UserViewModel> viewModels = new List<UserViewModel>();

      foreach (var user in users)
      {
        viewModels.Append(new UserViewModel { Age = user.Age, Name = user.Name, Email = user.Email, Surname = user.Surname, Id = user.Id });
      } */

      // return View(viewModels);
      return users;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserEntity>> GetUser(int id)
    {
      var user = await _userRepository.GetByIdAsync(id);
      if (user == null)
      {
        return NotFound(new { message = $"User de id={id} não encontrado" });
      }
      return user;
    }
  }
}