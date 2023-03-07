using Domain.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using SendMail.MailTemplate;
using System;
using System.Threading.Tasks;

namespace SendMail.Functions
{
    public static class SendMail
    {
        [FunctionName(nameof(SendMail))]
        public static async Task Run(
        [ServiceBusTrigger("onboardinguserqueue", Connection = "ServiceBusConnection")] string message,
        [SendGrid(ApiKey = "SendgridAPIKey")] IAsyncCollector<SendGridMessage> messageCollector,
        ILogger log)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<User>(message);

                var mailMessage = new SendGridMessage();
                mailMessage.AddTo(user.Mail);
                mailMessage.AddContent("text/html", string.Format(OnboardingTemplate.Mail, user.FirstName));
                mailMessage.SetFrom(new EmailAddress("fernando.vendramin@ciandt.com", "Anima Tech Week"));
                mailMessage.SetSubject($"Seja bem vindo ao Anima Tech Week 2023 {user.FirstName}!");

                await messageCollector.AddAsync(mailMessage);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Erro no envio de email.");
                throw;
            }
        }
    }
}
