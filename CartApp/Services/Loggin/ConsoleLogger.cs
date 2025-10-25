namespace CartApp.Services.Loggin
{
    public class ConsoleLogger:ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine($"[INFO ] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }

        public void Error(string message)
        {
            Console.WriteLine($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }
    }
}
