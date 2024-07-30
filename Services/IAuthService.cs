using FluentResults;
using FullLibrary.DTOs;
using FullLibrary.Responses;

namespace FullLibrary.Services
{
    public interface IAuthService
    {
        public Task<Result<TokenResponse>> Login(LoginDto model);
    }
}
