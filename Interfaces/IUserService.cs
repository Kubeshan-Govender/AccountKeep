namespace AccountKeep.Interfaces
{
    public interface IUserService
    {
        // Returns true if credentials are valid
        bool Authenticate(string username, string password);
    }
}
