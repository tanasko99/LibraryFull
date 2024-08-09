using FluentResults;
using FullLibrary.DTOs;
using FullLibrary.Responses;

namespace FullLibrary.Services
{
    public interface IAuthService
    {
        public Task<Result<TokenResponse>> Login(LoginDto model);
        public Task<Result> RegisterLibrarian(RegisterDto model);
        public Task<Result> RegisterUser(RegisterDto model);
    }
}
