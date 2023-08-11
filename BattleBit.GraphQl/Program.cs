using BattleBit.GraphQL;
using BattleBitAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton(provider => new BattleBitService(12345));
builder.Services
    .AddHostedService(sp => sp.GetRequiredService<BattleBitService>());

builder.Services
    .AddGraphQLServer()
        .InitializeOnStartup()
        .RegisterService<BattleBitService>()
    .AddQueryType<Query>();

var app = builder.Build();

app.UseWebSockets();
app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

app.Run();
