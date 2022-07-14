using System.Threading.Tasks;
using CIS.Service.Client.Models;

namespace CIS.Service.Client.Services
{
    /// <summary>
    /// The token provider.
    /// </summary>
    public interface ICisServiceTokenProvider
    {
        JsonTokenModel GetToken(WebApiOption settings);

        Task<JsonTokenModel> GetTokenAsync(WebApiOption settings);

        JsonTokenModel RefreshToken(WebApiOption settings, JsonTokenModel originalToken);

        Task<JsonTokenModel> RefreshTokenAsync(WebApiOption settings, JsonTokenModel originalToken);
    }
}