using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string GivenName { get; set; } = "";
    public string Surname { get; set; } = "";
    public int HP { get; set; } = 0;
    public int MP { get; set; } = 0;
}