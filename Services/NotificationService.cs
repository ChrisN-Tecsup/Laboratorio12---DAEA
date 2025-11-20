using System;

namespace Lab12_ChristianMamani.Services
{
    public class NotificationService
    {
        public void SendNotification(string user)
        {
            if (user == "usuario_fallo")
            {
                throw new Exception("Error simulado para probar reintentos de Hangfire");
            }
            Console.WriteLine($"Notificación enviada a {user} en {DateTime.Now}");
        }
    }
}