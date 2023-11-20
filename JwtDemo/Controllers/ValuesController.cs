using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace JwtDemo.Controllers
{
    using JwtDemo.Models;
    using System.Net.Http.Headers;
    using System.Security.Claims;

    
    public class ValuesController : ApiController
    {
        private readonly LoginContext _dbContext;
        

        public ValuesController()
        {
           
            _dbContext = new LoginContext();
        }
        [HttpPost]
        [Route("api/values/getToken")]
        public IHttpActionResult TokenGenerate([FromBody] LoginModel value)
        {
            var user = _dbContext.user.FirstOrDefault(u => u.Username == value.Username && u.Password == value.Password);

            if (user != null)
            {
                var token = GenerateToken(value.Username, value.Password, user.Role);
                return Ok(new { Token = token });
            }

           
            return BadRequest("Data not found");
        }



        private string GenerateToken(string username , string password , role role)
        {
            string key = "vuilcebljwevuevb23lq123uir98bqvoqf-vqwev8fy3uq-093ru1o3if1p98gh1340934-g3g923478ggb4up2";
            var issuer = "http://mysite.com";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            
            new Claim("username", username),
             new Claim(ClaimTypes.Role, role.ToString()),
        };

            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        [HttpPost]
        [Route("api/values/Login")]
        public string Login([FromBody] LoginModel credentials)
        {
            var username = credentials.Username;
            var password = credentials.Password;

            if (!IsValidUserInDatabase(username, password))
            {
                return "Invalid Username (or) Password";
            }

            string token_generate = GetToken(credentials.Username, credentials.Password );

           

            var tokenHandler = new JwtSecurityTokenHandler();

            var hardcodedToken = token_generate; 
            var authHeader = new AuthenticationHeaderValue("Bearer", hardcodedToken);
            if (authHeader == null || !authHeader.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                return "Invalid token";
            }

            var token = authHeader.Parameter;

            
           var key = Encoding.UTF8.GetBytes("vuilcebljwevuevb23lq123uir98bqvoqf-vqwev8fy3uq-093ru1o3if1p98gh1340934-g3g923478ggb4up2");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "http://mysite.com",
                ValidateAudience = true,
                ValidAudience = "http://mysite.com",
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            ClaimsPrincipal principal;
            try
            {
                principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
            return "Login Successfully...!";

           
        }

        private string GetToken(string username, string password)
        {
            var user = _dbContext.user.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                var token = GenerateToken(username, password, user.Role);
                return token;
            }

            return "data not found";
        }

        private bool IsValidUserInDatabase(string username, string password)
        {
            using (var _dbContext = new LoginContext()) 
            {
                var user = _dbContext.user.FirstOrDefault(u => u.Username == username && u.Password == password);

                return user != null;
            }
        }


   
    }
   
}
