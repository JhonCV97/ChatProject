using Application.Common.Response;
using Application.Cqrs.ConnectionChatGPT.Queries;
using System.Threading.Tasks;

namespace Application.Interfaces.ConnectionChatGPT
{
    public interface IConnectionChatGPT
    {
        Task<ApiResponse<string>> GetMessageChat(GetMessageChatGPTQuery request);
    }
}