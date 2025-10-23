
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class IdentityService : IIdentityService
{
    private UserManager<User> _userManager;
    private IOptions<JwtSettings> _config;
    private RoleManager<UserRole> _roleManager;
    private IHttpContextAccessor _contextAccessor;

    public IdentityService(UserManager<User> userManager, IOptions<JwtSettings> config, RoleManager<UserRole> roleManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _config = config;
        _roleManager = roleManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<Result<string>> CreateUser(User request, string password)
    {
        await _userManager.CreateAsync(request, password);

        //To-Do remove this section.
        UserRole? userRole = await _roleManager.FindByNameAsync("admin");
        if (userRole == null)
        {
            UserRole adminRole = new UserRole()
            {
                Name = "admin",
            };
            await _roleManager.CreateAsync(adminRole);
        }
        User? user = await _userManager.FindByNameAsync(request.UserName);
        await _userManager.AddToRoleAsync(user, "admin");


        return Result<string>.Success("");
    }

    public Task<Result<Access>> GetAccess(IDomainAccess item, CancellationToken none)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<string>> GetUserId()
    {
        string? userName = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userName == null)
        {
            return Result<string>.Failure(["UserId Not Found"]);
        }

        User user = await _userManager.FindByNameAsync(userName);

        return Result<string>.Success(user.Id);
    }

    public async Task<Result<string>> SignIn(string email, string username, string password)
    {
        User? user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return Result<string>.Failure(["User could not be found"]);
        }

        if (await _userManager.CheckPasswordAsync(user, password))
        {
            string token = await GenerateJwtToken(user);
            return Result<string>.Success(token);
        }

        return Result<string>.Failure(["Incorrect Password"]);
    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        // Add Identity roles if needed
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config.Value.Issuer,
            audience: _config.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}