using ActorMessaging.Common.Model;
using Akka.Actor;

namespace ActorMessaging.Server.Actor;

public class Clients
{
    public static Dictionary<Guid, IActorRef> ClientsList = [];
}

public class ServerActor : ReceiveActor
{
    public ServerActor()
    {
        Receive<Connect>(Connect);
        Receive<Disconnect>(Disconnect);
        Receive<ClientMessage>(ClientMessage);
    }

    public void Connect(Connect connect)
    {
        Clients.ClientsList.Add(connect.ClientId, Sender);
        WriteLine($"Client connected with id : {connect.ClientId}");
    }

    public void Disconnect(Disconnect disconnect)
    {
        Clients.ClientsList.Remove(disconnect.ClientId);
        WriteLine($"Client disconnected : {disconnect.ClientId}");
    }

    public void ClientMessage(ClientMessage message)
    {
        foreach (var client in Clients.ClientsList.Values)
            client.Tell(message);
    }
}
