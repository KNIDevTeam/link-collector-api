using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Link;
using WebApi.services.LinkService;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LinkController : ControllerBase
    {
        private readonly ILinkService _linkService;

        public LinkController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        [HttpPost]
        public async Task<IActionResult> AddLink(AddLinkDto newLink)
        {
            var response = await _linkService.AddLink(newLink);
            
            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllLinks()
        {
            var response = await _linkService.GetAllLinks();
            
            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}