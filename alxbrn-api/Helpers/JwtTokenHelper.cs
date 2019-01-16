using alxbrn_api.Data;
using alxbrn_api.DTOs;
using alxbrn_api.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace alxbrn_api.Helpers
{
    public class JwtTokenHelper
    {
        public TokenDto CreateToken(AuthorizedApp authApp)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = DateTime.UtcNow.AddDays(30);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new GenericIdentity(authApp.Name), new[]
            {
                new Claim("appToken", authApp.AppToken, ClaimValueTypes.String),
            });

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(GlobalConfig.Secret));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //create the token
            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(
                GlobalConfig.Issuer,
                GlobalConfig.Audience,
                claimsIdentity,
                issuedAt,
                expires,
                signingCredentials: signingCredentials);

            return new TokenDto
            {
                Token = tokenHandler.WriteToken(token),
                Expires = expires,
            };
        }
    }
}