using System;
using System.ComponentModel.DataAnnotations;
using com.esendex.sdk.messaging;


namespace Drim.Interfaces.MVC.Helpers
{
   
    public class SmsManager  
    {
        [Required]
        public string AccountSid { get; set; }

        [Required]
        public string AuthToken { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }


        private string SendTheMessageToTheRemoteHost(string message)
        {
            string msg = (message.Length > 160) ? message.Substring(0, 160) : message;
            string result = string.Empty;

            try
            {
                MessagingService messagingService = null;
                SmsMessage smsmessage = null;

                if (!string.IsNullOrEmpty(this.To))
                {
                    foreach (var to in this.To.Split(';'))
                    {
                        messagingService = new MessagingService(this.From, this.AuthToken);
                        smsmessage = new SmsMessage(to, msg, this.AccountSid);
                        smsmessage.Originator = "DRIM";
                        smsmessage.Recipients = to;
                        messagingService.SendMessage(smsmessage);
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}