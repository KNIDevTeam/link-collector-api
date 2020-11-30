using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Dtos.Link;
using WebApi.Models;

namespace WebApi.services.LinkService
{
    public interface ILinkService
    {
        Task<ServiceResponse<bool>> AddLink(AddLinkDto newLink);
        Task<ServiceResponse<List<GetLinkDto>>> GetAllLinks();
    }
}