using System;
using System.Threading.Tasks;
using Koa.Persistence.EntityRepository;
using Microsoft.Extensions.Logging;

namespace MicroSample.Business.Event.User
{
  [Koa.Integration.PubSub.EventType("user-created")]
  public class UserCreatedEvent : Koa.Integration.PubSub.Event
  {
    public int UserId { get; set; }

    public UserCreatedEvent(int userId)
    {
      this.UserId = userId;
    }
  }

  public class UserCreatedEventHandler : Koa.Integration.PubSub.IEventHandler<UserCreatedEvent>
  {
    private readonly IEntityRepository<int> repository;
    private readonly ILogger<UserCreatedEventHandler> logger;

    public UserCreatedEventHandler(IEntityRepository<int> repository, ILogger<UserCreatedEventHandler> logger)
    {
      this.repository = repository;
      this.logger = logger;
    }

    public Task HandleAsync(UserCreatedEvent @event)
    {
      var user = this.repository.FindOne<Domain.Entity.User>(@event.UserId);
      if (user == null)
      {
        this.logger.LogInformation($"User with id {@event.UserId} not found");
      }
      else
      {
        this.logger.LogInformation($"User with id {@event.UserId} found. Name: {user.FirstName}");
      }

      return Task.CompletedTask;
    }
  }
}