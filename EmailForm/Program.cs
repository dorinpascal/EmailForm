using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;

using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailForm
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sender = new SmtpSender(() => new System.Net.Mail.SmtpClient("localhost")
            {
                EnableSsl = false,
                
                DeliveryMethod=SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation=@"C:\Email"
            }); ;


            StringBuilder template = new();
            template.AppendLine("Dear @Model.FirstName,");
            template.AppendLine("<p>Thanks for purchasing @Model.ProductName.We hope you enjoy it.</p>");
            template.AppendLine("- Dorin Pascal");


            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = await Email
                .From("pascald@mail.ru").To("304192@via.dk", "Dorin").Subject("Thanks!").
                UsingTemplate(template.ToString(),new { FirstName = "Dorin", ProductName = "Cars" })
               // Body("Thanks for buying our product")
                .SendAsync();
           


        }
    }
}
