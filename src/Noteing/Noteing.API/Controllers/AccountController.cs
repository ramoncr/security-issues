using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Noteing.API.Models;
using Noteing.API.Services;

namespace Noteing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MailService _mailService;

        public AccountController(UserManager<ApplicationUser> userManager, MailService mailService)
        {
            this._userManager = userManager;
            this._mailService = mailService;
        }

        [HttpPost("/api/account/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterAccountModel model)
        {
            var result = await _userManager.CreateAsync(new()
            {
                Email = model.Email,
                UserName = model.Email,
                EmailConfirmed = true,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
            }, model.Password);

            if (result.Succeeded && model.Roles.Any())
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                await _userManager.AddToRolesAsync(user, model.Roles);
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }


        [HttpPut("/api/account/{accountId}")]
        public async Task<IActionResult> Update([FromBody] UpdateAccountModel model, string accountId)
        {
            if (!Guid.TryParse(accountId, out var parsedAccountId))
            {
                return BadRequest("Invalid account id");
            }

            return await Update(parsedAccountId, model);
        }

        [HttpPut("/api/account/me")]
        public Task<IActionResult> UpdateMe([FromBody] UpdateAccountModel model)
        {
            var userId = User.Identity.GetSubjectId();
            var parsedAccountId = Guid.Parse(userId);
            return Update(parsedAccountId, model);
        }

        [HttpGet]
        public List<ApplicationUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        [HttpPost("/api/account/passwordreset")]
        public async Task<ActionResult> ResetPassword([FromBody] PasswordResetModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest();

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _mailService.SendPasswordResetEmail(model.Email, user.FirstName, resetToken);
            return Ok(resetToken);
        }

        private async Task<IActionResult> Update(Guid accountId, UpdateAccountModel model)
        {
            var result = await _userManager.FindByIdAsync(accountId.ToString());
            if (result == null)
                return BadRequest();

            result.Email = model.Email;
            result.FirstName = model.FirstName;
            result.LastName = model.LastName;
            result.MiddleName = model.MiddleName;

            await _userManager.UpdateAsync(result);


            foreach (var role in model.Roles)
            {
                if (!await _userManager.IsInRoleAsync(result, role))
                {
                    await _userManager.AddToRoleAsync(result, role);
                }
            }

            return Ok();
        }
    }
}
