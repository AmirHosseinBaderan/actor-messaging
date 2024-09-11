using Akka.Actor;

namespace ActorMessaging.Client.Handler;

public class MessageHandler(IActorRef serverRef)
{
    public bool GetUserInput()
    {
        Write("Please write your message : ");
        string? message = ReadLine();
        if (message == "exit")
        {
            WriteLine("Finish Thank your :)");
            return false;
        }

        serverRef.Tell(message);
        return true;
    }

    public void OnServerReply(string message)
    {
        GetUserInput();
    }
}
