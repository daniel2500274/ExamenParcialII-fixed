using CartApp.Services.Notify.interfaces;

namespace CartApp.Services.Notify
{
    public class EmailSender : IEmailSender
    { 
        public void Send(string recipientEmail, string subjectEmail, string bodyEmail)
        {
            Console.WriteLine("\n=== Enviando Email ===");
            Console.WriteLine($"Para: {recipientEmail}");
            Console.WriteLine($"Asunto: {subjectEmail}");
            Console.WriteLine("Contenido:");
            Console.WriteLine(bodyEmail);
            Console.WriteLine("======================\n"); 
        }
    }
}