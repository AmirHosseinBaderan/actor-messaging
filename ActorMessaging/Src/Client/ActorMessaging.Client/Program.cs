
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
ActorSelection actorSelection = system.ActorSelection($"akka.tcp://{ServerName}@{Host}:{Port}/user/{MessageActorName}");

while (true)
{
    Write("Please write your message : ");
    string? message = ReadLine();
    if (message == "exit")
    {
        WriteLine("Finish Thank your :)");
        break;
    }

    actorSelection.Tell(message);
}