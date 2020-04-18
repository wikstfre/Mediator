Mediator
=======

Simple in-process messaging with mediator pattern in .NET

Send commands, queries and events, asynchronous dispatching via C# generic variance.

Create a pipeline around your messages with behaviors, easy and verbose registration.

## How

Setup your commands, queries, events and pipelines using dependency injection. An example:

### Implement handler and behavior:

```c#
public class AddFilterHandler : IMessageHandler<AddFilter>
{
  private readonly ISomeService _someService;

  public PingMessageHandler(ISomeService someService)
  {
    _someService = someService ?? throw new ArgumentNullException(nameof(someService));
  }

  public async Task Handle(PingMessage message, IMessageContext context)
  {
    // handle the message
  }
}

public class PingMessageBehavior : IMessageBehavior<PingMessage>
{
  private readonly ISomeOtherService _someOtherService;

  public PingMessageBehavior(ISomeOtherService someOtherService)
  {
    _someOtherService = someOtherService ?? throw new ArgumentNullException(nameof(someOtherService));
  }
  
  public async Task Handle(IMessage message, IMessageContext context, PipelineDelegate next)
  {
    // do something before the handler is executed
	
    await next().ConfigureAwait(false);
	
	// do something after the handler is executed
  }
}
```

### Register message, handler and behavior

```c#
services
  .AddMessage<PingMessage>()
  .AddMessageHandler<PingMessageHandler>()
  .AddMessageBehavior<PingMessageBehavior>();
```

Then you are able to use the injected `IMediator` interface.

### Sending messages

```c#
var ping = new PingMessage();

mediator.Send(ping);
```

## Why

When designing *CQRS* or *Event-Driven* applications we need to publish events from the infrastructure layer into the *domain event handlers*. We do not want framework dependencies leaking out to the Model. 
