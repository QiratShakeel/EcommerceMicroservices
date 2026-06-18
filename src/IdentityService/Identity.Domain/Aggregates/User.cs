using BuildingBlocks.Shared.Exceptions;
using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Identity.Domain.Events;
using Ecommerce.Identity.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;
using System.Text.RegularExpressions;

namespace Ecommerce.Identity.Domain.Aggregates
{
    public class User : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }           // Unique
        public string PasswordHash { get; private set; }    // Hash, not plain text
        public DateTime CreatedDate { get; private set; }
        private readonly HashSet<Role> _roles = new();
        public bool isActive { get; private set; } = true;
        public IReadOnlyCollection<Role> Roles => _roles;
        private readonly string EmaiPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public User() { }
        //name empty nh hskta , email format ma hne chaiye password strong hna chaiye 
        public User(string name, string email, string passwordHash)
        {
            SetName(name);
            SetEmail(email);
            SetPasswordHash(passwordHash);
            CreatedDate = DateTime.UtcNow;
            AddDomainEvent(new UserRegistered(name,email));
        }
        public void AssignRole(Role role)
        {
            if (_roles.Contains(role))
                throw new DomainException($"Role {role} already assigned");

            _roles.Add(role);
        }
        public void RemoveRole(Role role)
        {
            if (!_roles.Contains(role))
                throw new DomainException($"Role {role} not found");

            _roles.Remove(role);
        }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new UserNameRequiredException();
            if (name.Length > 100) // max length limit
                throw new UserNameTooLongException();
            Name = name.Trim();
        }
        public void SetEmail(string email) {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email)) throw new UserEmailRequiredException();
            if(email.Length > 254) // typical max email length
                throw new UserEmailTooLongException();
            if ( !Regex.IsMatch(email,EmaiPattern)) throw new UserEmailPatternException();
            Email = email.Trim().ToLower();
        }
        public void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("Password hash cannot be empty");

            PasswordHash = passwordHash;
        }
        public static void ValidatePasswordRules(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new DomainException("Password cannot be empty");
            if (password.Length < 8) throw new DomainException("Password is Too short");
            if (!password.Any(char.IsUpper)) throw new DomainException("Password Missing uppercase");
            if (!password.Any(char.IsLower)) throw new DomainException("Password Missing lowercase");
            if (!password.Any(char.IsDigit)) throw new DomainException("Password Missing number");
            if (!password.Any(c => "!@#$%^&*()".Contains(c))) throw new DomainException("Password Missing special char");
        }
        public void LogLogin()
        {
            AddDomainEvent(new UserLoggedIn(Id, DateTime.UtcNow));
        }
        public void Deactivate()
        {
            isActive = false;
        }
    }
}