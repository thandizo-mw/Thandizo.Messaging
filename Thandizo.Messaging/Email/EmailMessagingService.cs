using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Thandizo.DataModels.General;
using Thandizo.DataModels.Messaging;
using Thandizo.Messaging.Core;

namespace Thandizo.Messaging.Email
{
    public class EmailMessagingService : IMessagingService
    {
        private readonly ISocketLabsClient _client;

        public EmailMessagingService(ISocketLabsClient client)
        {
            _client = client;
        }
        public async Task<OutputResponse> SendMessage(MessageModel message)
        {
            var result = new OutputResponse
            {
                IsErrorOccured = false
            };
            var hasRecipient = false;
            var emailMessage = new BulkMessage();
            emailMessage.Subject = message.Subject;
            emailMessage.HtmlBody = message.MessageBody;
            emailMessage.From.Email = message.SourceAddress;

            foreach (var recipient in message.DestinationRecipients)
            {
                if (!string.IsNullOrEmpty(recipient))
                {
                    emailMessage.To.Add(recipient);
                    hasRecipient = true;
                }
            }


            if (hasRecipient)
            {
                await _client.SendAsync(emailMessage);
               
            }
            return result;
            
        }
    }
}
