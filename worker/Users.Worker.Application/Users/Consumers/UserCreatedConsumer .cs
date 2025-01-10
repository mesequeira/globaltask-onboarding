using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Worker.Domain.Users.Events;
using MassTransit;


namespace Users.Worker.Application.Users.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        public Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            // Lógica para enviar el mensaje de bienvenida
            return Task.CompletedTask;
        }
    }

}
