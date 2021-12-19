using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PucMinas.TCC.Authentication.Services;
using PucMinas.TCC.Domain.Models;
using PucMinas.TCC.Domain.Repositories;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PucMinas.TCC.Authentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        readonly AuthenticateService AuthenticateService;

        public AuthenticateController(
            IConfiguration configuration,
            IUserRepository userRepository
        )
        {
            AuthenticateService = new AuthenticateService(
                configuration,
                userRepository
            );
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(Models.UserModel user)
        {
            ReturnModel<JwtTokenModel> returnModel = new();

            try
            {
                returnModel.Object = await AuthenticateService.Authenticate(user);
                return StatusCode((int)HttpStatusCode.OK, returnModel);
            }
            catch (Exception ex)
            {
                returnModel.SetError(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnModel);
            }
        }
    }
}
