using ActorMessaging.Common.Model;
using Akka.Actor;

namespace ActorMessaging.Client.Actor;

public class ClientActor : ReceiveActor
{
    private readonly Guid _id;

    private readonly IActorRef _serverRef;

    public ClientActor(Guid id, IActorRef serverRef)
    {
        _id = id;
        _serverRef = serverRef;
        Receive<ClientMessage>(ClientMessage);
    }

    public void ClientMessage(ClientMessage message)
    {
        WriteLine($"\n {message.SenderId} : {message.Content}");
    }

    protected override void PreStart()
    {
        _serverRef.Tell(new Connect(_id));
        WriteLine($"Connect to server {_id}");
    }

    protected override void PostStop()
    {
        _serverRef.Tell(new Disconnect(_id));
        WriteLine("Disconnect with server :)");
    }
}
