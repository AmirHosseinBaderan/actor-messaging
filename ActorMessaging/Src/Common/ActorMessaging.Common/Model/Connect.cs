namespace ActorMessaging.Common.Model;

public record Connect(Guid ClientId);

public record Disconnect(Guid ClientId);
