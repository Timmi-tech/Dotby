using Dotby.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Dotby.Application.Services.Contracts
{
    
    
    public interface IAuthenticationService
    { 
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration); 
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
        void SetTokenInsideCookie(TokenDto tokenDto, HttpContext context);
    } 
}