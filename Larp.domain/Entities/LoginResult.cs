using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.domain.Entities
{
    public enum LoginOutcome
    {
        Success,
        UserNotFound,
        TenantInactive,    
        TenantUnreachable,   
        InvalidCredentials,  
        Error
    }

    public class LoginResult
    {
        public LoginOutcome Outcome { get; set; }
        public string Message { get; set; } = string.Empty;

        public int TenantId { get; set; }
        public string TenantCode { get; set; } = string.Empty;
        public string TenantName { get; set; } = string.Empty;
        public string TenantDatabaseName { get; set; } = string.Empty;
        public string TenantServerName { get; set; } = string.Empty;

        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;   // "Cashier", "Manager", "Admin"

        public bool IsSuccess => Outcome == LoginOutcome.Success;

        public static LoginResult Fail(LoginOutcome outcome, string message) =>
            new LoginResult { Outcome = outcome, Message = message };
    }
}