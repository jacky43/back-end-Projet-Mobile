﻿namespace ProjetMobileAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BornDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
    }
}
