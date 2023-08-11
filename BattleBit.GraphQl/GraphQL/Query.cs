using BattleBitAPI;

namespace BattleBit.GraphQL;

public class Query
{
    public bool SendMessage(BattleBitService service, string message, string serverName)
        => service.PostMessage(message, serverName);
}