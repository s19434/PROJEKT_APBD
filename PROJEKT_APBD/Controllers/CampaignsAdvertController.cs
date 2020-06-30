using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJEKT_APBD.DTOs.Request;
using PROJEKT_APBD.DTOs.Response;
using PROJEKT_APBD.Exceptions;
using PROJEKT_APBD.Services;

namespace PROJEKT_APBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsAdvertController : ControllerBase
    {
        private readonly ICampaignAdvertsService _dbService;
        public CampaignsAdvertController(ICampaignAdvertsService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("api/clients")]
        public IActionResult AddClient(ClientRegistrationRequest req)
        {
            try
            {
                var resp = _dbService.ClientRegistration(req);
                return Created("", resp);
            }
            catch (LoginException e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("refresh/token")]
        public IActionResult RenewBearerToken(RenewTokenRequest req)
        {
            try
            {
                return Ok(_dbService.RenewToken(req));
            }
            catch (TokenException e)
            {
                return NotFound(e);
            }
        }
        [HttpPost("clients/login")]
        public IActionResult LoginClient(LoginRequest req)
        {
            try
            {
                return Ok(_dbService.LoginClient(req));
            }
            catch (LoginException e)
            {
                return BadRequest(e);
            }
            catch (PasswordException e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("clients/campaigns")]
        [Authorize]
        public IActionResult GetCampaings()
        {
            try
            {
                return Ok(_dbService.GetListOfCampaigns());
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("clients/campaigns/create")]
        [Authorize]
        public IActionResult NewCampaign(NewCampaignRequest req)
        {
            try
            {
                return Created("", _dbService.NewCampaign(req));
            }
            catch (BuildingsException exc)
            {
                return NotFound(exc);
            }
            catch (LocationException e)
            {
                return BadRequest(e);
            }
        }
    }
}