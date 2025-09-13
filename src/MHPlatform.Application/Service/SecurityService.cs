using MHPlatform.Application.DTO;
using MHPlatform.Application.Interface;
using MHPlatform.Domain.Entities.Auth;
using MHPlatform.Domain.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Application.Service
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public SecurityService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<UserAuthBaseDto> LoginAsync(string username, string password)
        {
            //var user = await _unitOfWork.Users.GetByCredentialsAsync(username, password);
            var user = (await _unitOfWork.User.FindAsync(u => u.UserName == username && u.Password == password)).FirstOrDefault();

            if (user == null)
                return new UserAuthBaseDto { IsAuthenticated = false };

            var claims = await _unitOfWork.UserClaim.FindAsync(u => u.UserId == user.UserId);
            var (accessToken, newRefreshToken) = BuildJwtTokens(claims, user.UserName);

            var auth = new UserAuthBaseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                IsAuthenticated = true,
                BearerToken = accessToken,
                RefreshToken = newRefreshToken
            };

            //var refreshRepo = _unitOfWork.RefreshTokens;
            //var existingToken = await refreshRepo.SingleOrDefaultAsync(t => t.UserId == user.Id);

            //if (existingToken == null || !ValidateExpiry(existingToken.Token))
            //{
            //    if (existingToken == null)
            //    {
            //        existingToken = new RefreshTokenDto
            //        {
            //            //Id = Guid.NewGuid(),
            //            UserId = user.Id,
            //            Token = newRefreshToken
            //            //Created = DateTime.UtcNow
            //        };
            //        await refreshRepo.AddAsync(existingToken);
            //    }
            //    else
            //    {
            //        existingToken.Token = newRefreshToken;
            //        existingToken.Created = DateTime.UtcNow;
            //        await refreshRepo.UpdateAsync(existingToken);
            //    }

            //    await _unitOfWork.CompleteAsync();
            //}
            //else
            //{
            //    auth.RefreshToken = existingToken.Token;
            //}

            return auth;
        }

        public bool ValidateExpiry(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken)) return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(refreshToken) as JwtSecurityToken;
            var exp = securityToken?.Payload.Exp;

            if (!exp.HasValue) return false;

            var expiryDate = DateTimeOffset.FromUnixTimeSeconds(exp.Value).UtcDateTime;
            return expiryDate > DateTime.UtcNow;
        }

        private (string, string) BuildJwtTokens(IEnumerable<UserClaim> claims, string username)
        {
            var jwt = GenerateToken(claims, username, _configuration.GetSection("JwtToken"));
            var refresh = GenerateToken(new List<UserClaim>(), username, _configuration.GetSection("RefreshJwtToken"));
            return (jwt, refresh);
        }

        private string GenerateToken(IEnumerable<UserClaim> claims, string username, IConfigurationSection config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            foreach (var claim in claims)
            {
                jwtClaims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
            }

            var token = new JwtSecurityToken(
                issuer: config["issuer"],
                audience: config["audience"],
                claims: jwtClaims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(config["minutestoexpiration"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
