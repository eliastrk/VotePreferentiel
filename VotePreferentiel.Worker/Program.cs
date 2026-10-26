using VotePreferentiel;
using VotePreferentiel.Worker;

var builder = Host.CreateApplicationBuilder(args);

//config des services/dépendances
builder.Services.AddHttpClient<VoteProvider>();
builder.Services.AddSingleton<IVoteCounter, VoteCounter>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
