namespace Larp.domain.DTO
{
    public class CreateCompanyUserRequest
    {
        // Login (master DB)
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;   // Admin / Manager / Staff

        // Employee (tenant DB)
        public string EmployeeCode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string Position { get; set; } = string.Empty;
        public string? ContactNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
        public DateTime DateHired { get; set; } = DateTime.UtcNow;
    }
}