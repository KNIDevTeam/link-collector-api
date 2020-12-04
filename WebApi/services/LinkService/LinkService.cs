using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos.Link;
using WebApi.Models;

namespace WebApi.services.LinkService
{
    public class LinkService : ILinkService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LinkService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ServiceResponse<bool>> AddLink(AddLinkDto newLink)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                if (newLink.Url == null || newLink.Description == null)
                {
                    response.Success = false;
                    response.Message = "Url and description cannot be null";
                    return response;
                }

                if (await _context.Links.FirstOrDefaultAsync(l => l.Url.Equals(newLink.Url)) != null)
                {
                    response.Success = false;
                    response.Message = "Specified url exists in links database";
                    return response;
                }

                Link link = _mapper.Map<Link>(newLink);
                link.CreatedAt = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                await _context.Links.AddAsync(link);
                await _context.SaveChangesAsync();
                response.Data = true;
            }
            catch (Exception e)
            {
                response.SetException(e);
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetLinkDto>>> GetAllLinks()
        {
            var response = new ServiceResponse<List<GetLinkDto>>();

            try
            {
                List<Link> links = await _context.Links.ToListAsync();
                response.Data = (links.Select(l => _mapper.Map<GetLinkDto>(l)).ToList());
            }
            catch (Exception e)
            {
                response.SetException(e);
            }

            return response;
        }
    }
}