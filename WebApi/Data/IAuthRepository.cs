using System.Threading.Tasks;
using WebApi.Dtos.Token;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> Login(ProvidedTokenDto providedToken);
    }
}