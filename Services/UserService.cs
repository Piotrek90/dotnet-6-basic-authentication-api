namespace WebApi.Services;


using WebApi.Entities;

public interface IUserService
{
    Task<User> Authenticate(string username, string password);
    Task<IEnumerable<User>> GetAll();
    Task<User> Register(User user);
}

public class UserService : IUserService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    
    UsersDbContext _usersDb;
    public UserService()
    {
        _usersDb = new UsersDbContext();
        _usersDb.Database.EnsureCreated();

    }


    public async Task<User> Authenticate(string username, string password)
    {
        // wrapped in "await Task.Run" to mimic fetching user from a db
        var user = await Task.Run(() => _usersDb.Users.SingleOrDefault(x => x.Username == username && x.Password == password));

        // on auth fail: null is returned because user is not found
        // on auth success: user object is returned
        return user;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        // wrapped in "await Task.Run" to mimic fetching users from a db
        return await Task.Run(() => _usersDb.Users);
    }

    public async Task<User> Register(User user)
    {
        _usersDb.Add(user);
        await _usersDb.SaveChangesAsync();

        return await Authenticate(user.Username, user.Password);
    }
}
