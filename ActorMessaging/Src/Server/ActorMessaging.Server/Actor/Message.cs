using Akka.Actor;

namespace ActorMessaging.Server.Actor;

public class MessageActor : ReceiveActor
{
    public MessageActor()
    {
        Receive<string>(HandleMessage);
    }

    public static void HandleMessage(string message)
    {
        Console.WriteLine($"Client send : {message}");
    }
}
