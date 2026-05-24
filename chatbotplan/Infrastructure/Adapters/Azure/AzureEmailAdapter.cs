using Azure;
using Azure.Communication.Email;
using ChatBotPlan.Application;
using Microsoft.Extensions.Options;

public class AzureEmailAdapter : IEmailService
{

    private readonly AzureEmailSettings _settings;

    public AzureEmailAdapter(IOptions<AzureEmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default)
    {
        var client = new EmailClient(_settings.ConnectionString);

        var message = new EmailMessage(
            senderAddress: _settings.From,
            content: new EmailContent(subject)
            {
                Html = body
            },
            recipients: new EmailRecipients(new List<EmailAddress>
            {
                new EmailAddress(to)
            })
        );

        await client.SendAsync(WaitUntil.Completed, message, ct);
    }
}