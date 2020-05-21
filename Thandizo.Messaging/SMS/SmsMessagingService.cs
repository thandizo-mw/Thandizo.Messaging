using AngleDimension.Standard.Http.HttpServices;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Thandizo.DataModels.General;
using Thandizo.DataModels.Messaging;
using Thandizo.DataModels.SMS;
using Thandizo.Messaging.Core;

namespace Thandizo.Messaging.SMS
{
    public class SmsMessagingService : IMessagingService
    {
        private readonly SmsConfiguration _smsConfiguration;

        public SmsMessagingService(SmsConfiguration smsConfiguration)
        {
            _smsConfiguration = smsConfiguration;
        }

        public async Task<OutputResponse> SendMessage(MessageModel message)
        {
            var result = new OutputResponse
            {
                IsErrorOccured = false
            };

            if (message != null)
            {
                var url = string.Format("{0}?username={1}&password={2}&to={3}&smsc={4}&from={5}&text={6}",
                    _smsConfiguration.BaseUrl, _smsConfiguration.RapidProUserName,
                    _smsConfiguration.RapidProPassword, message.DestinationRecipients.FirstOrDefault(),
                   _smsConfiguration.RapidProSmsCode, _smsConfiguration.SmsSender,
                   message.MessageBody);

                var response = await HttpRequestFactory.Get(url);

                if (response.StatusCode == HttpStatusCode.Accepted)
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
