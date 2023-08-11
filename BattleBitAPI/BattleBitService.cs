using BattleBitAPI;
using BattleBitAPI.Common;
using BattleBitAPI.Server;

using Microsoft.Extensions.Hosting;

namespace BattleBitAPI;

public class MyPlayer : Player<MyPlayer>
{
}

public class MyGameServer : GameServer<MyPlayer>
{
}

public class BattleBitService : BackgroundService
{
    private readonly int _portNumber;
    private readonly ServerListener<MyPlayer, MyGameServer> _listener;

    public BattleBitService(int portNumber)
    {
        _portNumber = portNumber;
        _listener = new ServerListener<MyPlayer, MyGameServer>();
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        _listener.Start(port: _portNumber);
        // Thread.Sleep(-1);

        while (_listener.IsListening)
        {
            // Do something while waiting?
        }
    }

    public bool PostMessage(
        string message,
        string targetServer)
    {
        var server = _listener.ConnectedGameServers
            .FirstOrDefault(x => x.ServerName.Equals(targetServer));

        if (server is null)
            return false;

        server.AnnounceLong(message);

        return true;
    }

    public bool PostToPlayer(
        string message,
        string targetServer,
        ulong targetPlayer)
    {
        var server = _listener.ConnectedGameServers
            .FirstOrDefault(x => x.ServerName.Equals(targetServer));

        if (server is null)
            return false;

        server.AllPlayers
            .FirstOrDefault(x => x.SteamID == targetPlayer)
            .Message(message);

        return true;
    }
}