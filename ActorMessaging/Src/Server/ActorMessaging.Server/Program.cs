using ActorMessaging.Server.Actor;
using Akka.Actor;
using Akka.Configuration;



Config config = ConfigurationFactory.ParseString(@"
            akka {
                actor.provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
                remote.dot-netty.tcp {
                    hostname = ""localhost""
                    port = 8081
                }
            }");

using ActorSystem system = ActorSystem.Create(ServerName, config);
var actor = system.ActorOf(Props.Create(() => new MessageActor()), MessageActorName);

WriteLine($"Server started at : {Host}:{Port} \n Press any key to stop server :)");
ReadLine();
