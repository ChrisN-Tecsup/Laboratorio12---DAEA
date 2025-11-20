using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Lab12_ChristianMamani.Services;

namespace Lab12_ChristianMamani.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        [HttpPost("fire-and-forget")]
        public IActionResult EnqueueJob()
        {
            BackgroundJob.Enqueue(() => new NotificationService().SendNotification("usuario_fallo"));
            return Ok("Job Fire-and-forget encolado correctamente.");
        }
        [HttpPost("delayed")]
        public IActionResult ScheduleJob()
        {
            BackgroundJob.Schedule(() => new NotificationService().SendNotification("usuario2"), TimeSpan.FromMinutes(10));
            return Ok("Job delayed encolado. Se ejecutará en 10 minutos.");
        }
        [HttpPost("recurrent")]
        public IActionResult CreateRecurrentJob()
        {
            RecurringJob.AddOrUpdate(
                "job-notificacion-diaria",
                () => new NotificationService().SendNotification("usuario_diario"),
                Cron.Daily);

            return Ok("Job recurrente creado. Se ejecutará diariamente.");
        }
    }
}