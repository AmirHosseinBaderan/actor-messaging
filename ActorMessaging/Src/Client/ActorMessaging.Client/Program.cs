using ActorMessaging.Client.Actor;
using ActorMessaging.Common.Model;
using Akka.Actor;
using Akka.Configuration;


var config = ConfigurationFactory.ParseString(@"
            akka {
                actor.provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
                remote.dot-netty.tcp {
                    hostname = ""localhost""
                    port = 0
                }
            }");

ActorSystem system = ActorSystem.Create(ClientName, config);
IActorRef actorSelection = await system
                            .ActorSelection($"akka.tcp://{ServerName}@{Host}:{Port}/user/{MessageActorName}")
                            .ResolveOne(TimeSpan.FromSeconds(3));

Guid id = Guid.NewGuid();
Props props = Props.Create(() => new ClientActor(id, actorSelection));
IActorRef clientActor = system.ActorOf(props, ClientMessageActorName);

while (true)
{
    Write("Write a message : ");
    var message = ReadLine();
    if (message == "exit") break;

    actorSelection.Tell(new ClientMessage(id, message ?? "..."));
}