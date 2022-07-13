using Microsoft.AspNetCore.Mvc;
using PasswordManagementApi.Contracts;
using PasswordManagementApi.Infrastructure;
using System.Collections.Generic;

namespace PasswordManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordManagementController : ControllerBase
    {
        private readonly PasswordManagementService service;

        public PasswordManagementController(PasswordManagementService service)
        {
            this.service = service;
        }

        [HttpPost("generate")]
        public GeneratePasswordResponse Generate(GeneratePasswordRequest request)
        {
            return service.GenerateUserPassword(request);
        }

        [HttpGet("list")]
        public List<UserDto> List()
        {
            return service.ListUsers();
        }
    }
}
