﻿namespace ScreenOps.AuthenticationService.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
