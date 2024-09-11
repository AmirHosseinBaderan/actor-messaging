namespace ActorMessaging.Common.Model;

public record ClientMessage(Guid SenderId, string Content);
