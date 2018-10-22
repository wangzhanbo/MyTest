using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRDemo.Events
{
    public class PingEvent
    {
    }

    public class AppUser : INotification
    {
        public int Id { get; set; }
    }

    public class UserHandle : INotificationHandler<AppUser>
    {
        public Task Handle(AppUser notification, CancellationToken cancellationToken)
        {

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString()+"Handle");

            return Task.CompletedTask;
        }
    }
}
