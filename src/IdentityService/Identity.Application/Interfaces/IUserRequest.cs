namespace Ecommerce.Identity.Application.Interfaces
{
    public interface IUserRequest
    {
        string email { get; } 
        string password { get; }
    }
}