﻿namespace BudgettingWebApps.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }=string.Empty;
        public string Password { get; set; }=string.Empty;
        public string Role { get; set; } = string.Empty;    
    }
}