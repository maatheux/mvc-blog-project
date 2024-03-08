using System.Net;
using System.Net.Mail;
using System.Text.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Blog.Services;

public class EmailService
{
    public bool Send(
        string toName,
        string toEmail,
        string subject,
        string body,
        string fromName = "Study test",
        string fromEmail = "chinchilaextreme@hotmail.com")
    {
        /////// SMTP ///////
        var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port);
        
         smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password);
         smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
         smtpClient.EnableSsl = true;
         smtpClient.UseDefaultCredentials = false;
        
        var mail = new MailMessage();

        mail.From = new MailAddress(fromEmail, fromName);
        mail.To.Add(new MailAddress(toEmail, toName));
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;


        /////// PELO PACKAGE ///////
        // var client = new SendGridClient(Configuration.Smtp.Password);
        // var msg = new SendGridMessage()
        // {
        //     From = new EmailAddress(fromEmail, fromName),
        //     Subject = subject,
        //     HtmlContent = body
        // };
        // msg.AddTo(new EmailAddress(toEmail, toName));
  
        try
        {
            smtpClient.Send(mail);
            
            /////// PELO PACKAGE ///////
            // var response = client.SendEmailAsync(msg).ConfigureAwait(false);
            
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}

// Podemos usar o sendgrid de algumas forma
// pela package dotnet add package SendGrid -> esta funcionando
// pela api (http) do sendgrid
// pelo smtp -> dessa forma fica generico para utilizarmos outros clients (zoho, onesignal...)

// smtp -> client nativo .NET esta funcionando