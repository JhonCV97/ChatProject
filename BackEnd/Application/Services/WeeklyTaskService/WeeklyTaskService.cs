using Application.Interfaces.SendEmail;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.WeeklyTaskService
{
    public class WeeklyTaskService : BackgroundService
    {
        private readonly ILogger<WeeklyTaskService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public WeeklyTaskService(ILogger<WeeklyTaskService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("WeeklyTaskService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    // Obtener el servicio de usuarios
                    var userService = scope.ServiceProvider.GetRequiredService<ISendEmailService>();
                    
                    // Ejecutar el método de usuarios
                    //await userService.GenerateEmail();
                }

                _logger.LogInformation("WeeklyTaskService is running. " + DateTime.Now);

                // Esperar 1 minuto antes de ejecutar nuevamente
                //await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                await Task.Delay(TimeSpan.FromDays(8), stoppingToken);
            }

            _logger.LogInformation("WeeklyTaskService is stopping.");
        }
    }
}
