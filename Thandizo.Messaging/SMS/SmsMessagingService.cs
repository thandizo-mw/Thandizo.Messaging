using AngleDimension.Standard.Http.HttpServices;
using System;
using System.Threading.Tasks;
using Thandizo.DataModels.General;
using Thandizo.DataModels.Messaging;
using Thandizo.Messaging.Core;

namespace Thandizo.Messaging.SMS
{
    public class SmsMessagingService : IMessagingService
    {
        private readonly string _baseUrl;
        private readonly string _sender;

        public SmsMessagingService(string baseUrl, string sender)
        {
            _baseUrl = baseUrl;
            _sender = sender;
        }

        public async Task<OutputResponse> SendMessage(MessageModel message)
        {
            var result = new OutputResponse
            {
                IsErrorOccured = false
            };

            if (message != null)
            {
                var gatewayMessage = new GatewayMessage
                {
                    Message = message.MessageBody,
                    Recipients = message.DestinationRecipients,
                    Sender = _sender,
                    Source = message.SourceAddress,
                    TransactionReference = Guid.NewGuid().ToString()
                };

                var response = await HttpRequestFactory.Post($"{_baseUrl}/api/Messages/SendMessageRapid", gatewayMessage);

                if (response.IsSuccessStatusCode)
                {
                    result.Message = "Message Queued for delivery";
                }
                else
                {
                    result.Message = "Sending of message failed";
                    result.IsErrorOccured = true;
                }
            }
            else
            {
                result.IsErrorOccured = true;
                result.Message = "Message model cannot be null or empty";
            }
            return result;
        }
    }
}
