using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minenetred.Web.Services;

namespace Minenetred.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserSettingsController : ControllerBase
    {
        private readonly IUsersManagementService _usersManagementService;
        private readonly ILogger<UserSettingsController> _logger;
        public UserSettingsController(
            IUsersManagementService usersManagementService,
            ILogger<UserSettingsController> logger)
        {
            _usersManagementService = usersManagementService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> updateBaseAddressAsync(string address)
        {
            try
            {
                if (string.IsNullOrEmpty(address))
                {
                    _logger.LogError(new ArgumentNullException(nameof(address)), "Empty address");
                    return BadRequest();
                }
                _usersManagementService.updateBaseAddress(address, UserPrincipal.Current.EmailAddress);
                if (!await _usersManagementService.IsValidBaseAddressAsync())
                {
                    _usersManagementService.updateBaseAddress("", UserPrincipal.Current.EmailAddress);
                    _logger.LogError(new InvalidCastException("Invalid base address"), "Invalid Base address");
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception)
            {
                _logger.LogError(new FormatException("Invalid address format"), "Invalid address format");
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> updateRedmineKeyAsync(string Redminekey)
        {
            try
            {
                if (string.IsNullOrEmpty(Redminekey))
                {
                    _logger.LogError(new ArgumentNullException(nameof(Redminekey)), "Missing key");
                    return BadRequest();
                }
                _usersManagementService.UpdateKey(Redminekey, UserPrincipal.Current.EmailAddress);
                await _usersManagementService.AddRedmineIdAsync(Redminekey, UserPrincipal.Current.EmailAddress);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Invalid key");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Bad request");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unhandled exception");
            }
            return BadRequest();
        }
        /* [HttpGet]
         public ActionResult ValidateUserSettings()
         {
             try
             {

                 return Ok();
             }
             catch (Exception)
             {

                 throw;
             }
         }*/

    }
}