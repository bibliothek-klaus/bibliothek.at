using MailChimp;
using MailChimp.Helper;
using System.Configuration;

namespace bibliothek.at.Contracts
{
    public class MailChimpEmailMarketing : IEmailMarketing
    {
        public bool RegisterRecipient(string emailAddress)
        {
            var mailChimpApiKey = ConfigurationManager.AppSettings["MailChimpApiKey"];
            var mailChimpListId = ConfigurationManager.AppSettings["MailChimpListId"];

            var mc = new MailChimpManager(mailChimpApiKey);

            var emailParameter = new EmailParameter()
            {
                Email = emailAddress
            };

            var results = mc.Subscribe(mailChimpListId, emailParameter);
            return true;
        }
    }
}