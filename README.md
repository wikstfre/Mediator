Mediator
=======

Simple in-process messaging with mediator pattern in .NET

Send commands, queries and events, asynchronous dispatching via C# generic variance.

Create a pipeline around your messages with behaviors, easy and verbose registration.

## How

Setup your commands, queries, events and pipelines using dependency injection. An example:

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
