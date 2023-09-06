using InvoiceManagementSystem.Core.Entities.Concrete;
using InvoiceManagementSystem.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.Security
{
    public class JwtHelper:ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        private IHttpContextAccessor _context;

        public JwtHelper(IConfiguration configuration, IHttpContextAccessor context)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpriation);
            _context = context;
        }

        public AccessToken CreateToken(User user, List<OperationClaim> claims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, claims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token=jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken
            {
                Token=token,
                Expiration=_accessTokenExpiration,
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: new DateTime(1970, 01, 01),
                claims: SetClaims(user, operationClaims),
                signingCredentials:signingCredentials
                ) ;
            return jwt ;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims=new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddRoles(operationClaims.Select(x => x.Name).ToArray());
            return claims;
        }
        public SessionAddDto CreateNewToken(User user)
        {
            Guid guid = Guid.NewGuid();
            var tokenString = HashString(guid.ToString(), "salt");
            _context.HttpContext.Request.Headers.TryGetValue(HeaderNames.UserAgent, out var userAgent);
            var session = new SessionAddDto
            {
                UserId = user.Id,
                SiteType = "local",
                TokenString = tokenString,
                UserAgent = userAgent.ToString(),
                Ip = "localIp"

            };
            return session;
        }
        private string HashString(string token, string salt)
        {
            using (var sha = new System.Security.Cryptography.HMACSHA256())
            {
                byte[] tokenBytes = Encoding.UTF8.GetBytes(token + salt);
                byte[] hasBytes = sha.ComputeHash(tokenBytes);

                string hash = BitConverter.ToString(hasBytes).Replace("-", string.Empty);
                return hash;
            }
        }
    }
}
