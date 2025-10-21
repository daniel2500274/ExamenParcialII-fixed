namespace CartApp
{
    public class EmailSender
    {
        public bool Send(string to, string subject, string body)
        {
            Console.WriteLine("\n=== Enviando Email ===");
            Console.WriteLine($"Para: {to}");
            Console.WriteLine($"Asunto: {subject}");
            Console.WriteLine("Contenido:");
            Console.WriteLine(body);
            Console.WriteLine("======================\n");
            return true;
        }
    }
}
