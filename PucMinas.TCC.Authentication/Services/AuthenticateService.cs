using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PucMinas.TCC.Domain.Models;
using PucMinas.TCC.Domain.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.TCC.Authentication.Services
{
    public class AuthenticateService
    {
        readonly IConfiguration Configuration;
        readonly IUserRepository UserRepository;

        public AuthenticateService(
            IConfiguration configuration,
            IUserRepository userRepository
        )
        {
            Configuration = configuration;
            UserRepository = userRepository;
        }

        public async Task<JwtTokenModel> Authenticate(Models.UserModel user)
        {
            UserModel userModel = await UserRepository.FindByUserId(user.UserId);
            ValidateUser(user, userModel);
            return CreateToken(userModel);
        }

        private void ValidateUser(Models.UserModel user, UserModel userModel)
        {
            if (userModel == null || userModel.Password != user.Password)
                throw new Exception("O usuário ou a senha estão incorretos.");

            if (userModel.IsSuspended)
                throw new Exception("O usuário está temporariamente suspenso. Entre em contato com o administrador.");
        }

        private JwtTokenModel CreateToken(Domain.Models.UserModel user)
        {
            DateTime createDate = DateTime.UtcNow;
            DateTime expirationDate = createDate.AddMinutes(Convert.ToInt32(Configuration["LifecycleMinutes"]));

            byte[] key = Encoding.ASCII.GetBytes(Configuration["APISecret"]);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = CreateClaimsIdentity(user),
                NotBefore = createDate,
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return new JwtTokenModel
            {
                Authenticated = true,
                Created = createDate,
                Expiration = expirationDate,
                AccessToken = token
            };
        }

        private ClaimsIdentity CreateClaimsIdentity(UserModel user)
        {
            return new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserId),
                new Claim(ClaimTypes.Role, user.Role)
            });
        }
    }
}
