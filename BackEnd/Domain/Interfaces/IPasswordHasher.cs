namespace Infra.Data.Repository
{
    public interface IPasswordHasher
    {
        bool Check(string hash, string password);
        string Hash(string password);
    }
}