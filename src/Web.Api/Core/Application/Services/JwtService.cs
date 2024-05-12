using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Web.Api.Core.Application.Abstractions.JwtToken;
using Web.Api.Core.Application.Settings;
using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Application.Services;

public class JwtService : IJwtService
{
    private readonly JWTSettings _jwtSettings;

    public JwtService(IOptions<JWTSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(User user)
    {
        var userClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id),
            new Claim("uid", user.Id),
            new Claim("cpf", user.Cpf),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
        };

        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

        //JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
        //    issuer: _jwtSettings.Issuer,
        //    audience: _jwtSettings.Audience,
        //    claims: userClaims,
        //    //expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
        //    //expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpiresInHours),
        //    expires: DateTime.UtcNow.AddDays(_jwtSettings.ExpiresInDays),
        //    signingCredentials: signingCredentials);
        // return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(userClaims),
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.ExpiresInDays),
            SigningCredentials = signingCredentials,
            Issuer = _jwtSettings.Issuer
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwt = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(jwt);
    }
}
/*
 var roleClaims = new List<Claim>();
            roles.ToList().ForEach(r => roleClaims.Add(new Claim("roles", r)));

            var userClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.UserName),
                new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", identityUser.Id)
            }
            .Union(claims)
            .Union(roleClaims);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
 */