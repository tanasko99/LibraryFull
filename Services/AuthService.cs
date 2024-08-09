using FluentResults;
using FullLibrary.DTOs;
using FullLibrary.Responses;
using FullLibrary.Security;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FullLibrary.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;


        public AuthService(
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<Result<TokenResponse>> Login(LoginDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = JwtHandler.CreateToken(_configuration, authClaims);
                    var refreshToken = JwtHandler.GenerateRefreshToken();

                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays);

                    await _userManager.UpdateAsync(user);

                    return Result.Ok(new TokenResponse
                    {
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = refreshToken,
                        Expiration = token.ValidTo
                    });
                }
                return Result.Fail("Unable to login");
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> RegisterLibrarian(RegisterDto model)
        {
            try
            {
                var librarianExists = await _userManager.FindByEmailAsync(model.Email);
                if (librarianExists != null)
                    Result.Fail("Librarian already exists!");

                ApplicationUser user = new()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    Result.Fail($"Failed to register {model.Email}");
                await _userManager.AddToRoleAsync(user, UserRoles.Librarian);
                _userService.CreateUser(new UserDto
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                });
                return Result.Ok();
            }
            catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public Task<Result> RegisterUser(RegisterDto model)
        {
            throw new NotImplementedException();
        }
    }
}
