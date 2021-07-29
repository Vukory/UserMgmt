using System;
using Microsoft.AspNetCore.Identity;

/// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0#change-the-primary-key-type
public class AppRole : IdentityRole<int>
{
    public AppRole() : base()
    {}
    public AppRole(string roleName) : base(roleName)
    {}
    public string Description { get; set; }
}