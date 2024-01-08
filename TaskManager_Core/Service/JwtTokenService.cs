using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Identity;
using TaskManager_Core.DTO;
using TaskManager_Core.ServiceContracts;

namespace TaskManager_Core.Service
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a JWT token using the given user's information and the configuration settings.
        /// </summary>
        /// <param name="user">ApplicationUser object</param>
        /// <returns>AuthenticationResponse that includes token</returns>
        public async Task<AuthenticationResponse> CreateJwtToken(ApplicationUser user)
        {
            //Create a DateTime object representing the token expiration time by adding the number of minutes specified in the configuration to the current UTC time
            DateTime expirationTime = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

            
            
            //Create an array of claims objects representing the user's claims such as their ID, name, email, etc.
            Claim[] claims = new Claim[] //Creating claims for Payload
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()), //ClaimName -> Subject (user id)
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), //ClaimName -> JWT unique ID, add newly generated id only
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()), //Issued at (date and time of token generation)
                new Claim("nameidentifier",user.Email), //Unique name identifier of the user(Email)
                new Claim("name",user.Id.ToString()),//Name of the user
                new Claim("email", user.Email),
                new Claim("role",user.Role), 

            };

            
            



            //Create  a SymmetricSecurityKey object using the key specified in the configuration.
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key"]));//secret key supplied here

            //Create a SigningCredentials objects with the security key and the HMACSHA256 algorithm
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); //hashing algorithm

            //Creates a JwtSecurityToken object with the given issuer, audience, claims, expiraion, and signing credentials.
            System.IdentityModel.Tokens.Jwt.JwtSecurityToken tokenGenerator = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: expirationTime, signingCredentials: signingCredentials);

            //Create a JwtSecurityTokenHandler object aand use it to write the token as a string
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            //Create and return an AuthenticationResponse object containing the token, user email, user name, and token expiration time.
            return new AuthenticationResponse()
            {
                Token = token, 
                Email = user.Email,
                Expiration = expirationTime,
                //RefreshToken = GenerateRefreshToken(),
                //RefreshTokenExpirationDateTime = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["RefreshToken:EXPIRATION_MINUTES"]))
            };
        }

        public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token)
        {
            var tokenValidationParameter = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key"])),
                ValidateLifetime = false //should be false because received token already expired so not needed.
            };

            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler jwtSecurityTokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            ClaimsPrincipal claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameter, out SecurityToken securityToken);

            if (securityToken is not System.IdentityModel.Tokens.Jwt.JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
            {
                throw new SecurityTokenException("invalid token");
            }

            return claimsPrincipal;
        }

        //Create refresh token(base 64 string of random numbers)
        private string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];

            var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }
    }
}

