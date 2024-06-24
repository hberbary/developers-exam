using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Domain.Services
{
  public class UserService : Service<UserEntity>
  {
    private readonly IRepository<UserEntity> _userRepository;

    public UserService(IRepository<UserEntity> userRepository) : base(userRepository)
    {
      _userRepository = userRepository;
    }

    public async void Save(int id, string email, string password, string name, string surname, int age)
    {
      var user = await _userRepository.GetByIdAsync(id);
      if (user == null)
      {
        user = new UserEntity(email, password, name, surname, age);
        await _userRepository.InsertAsync(user);
      }
      else
        await _userRepository.UpdateAsync(user);
    }

    public async void Delete(int id)
    {
      var user = await _userRepository.GetByIdAsync(id);

      await _userRepository.DeleteAsync(user.Id);
    }
  }
}