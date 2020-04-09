using System.Threading.Tasks;
using Thandizo.DataModels.General;
using Thandizo.DataModels.Messaging;

namespace Thandizo.Messaging.Core
{
    public interface IMessagingService
    {
        Task<OutputResponse> SendMessage(MessageModel message);
    }
}
