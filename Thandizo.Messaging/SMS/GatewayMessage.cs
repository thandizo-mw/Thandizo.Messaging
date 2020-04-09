using System.Collections.Generic;

namespace Thandizo.Messaging.SMS
{

    public class GatewayMessage
    {
        public string Message { get; set; }
        public IEnumerable<string> Recipients { get; set; }
        public string Source { get; set; }
        public string Sender { get; set; }
        public string TransactionReference { get; set; }
    }

}
