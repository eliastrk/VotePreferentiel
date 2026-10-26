using VotePreferentiel;

namespace VotePreferentiel.Worker;

public class Worker : BackgroundService{
    private readonly VoteProvider _voteProvider;

    public Worker(VoteProvider voteProvider){
        _voteProvider = voteProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken){
        var voteModel = await _voteProvider.GetVotes();

        Console.WriteLine($"Nombre de vote: {voteModel.Votes.Count}");

        foreach (var vote in voteModel.Votes)
        {
            Console.WriteLine(string.Join(" > ", vote.Choices));
        }
    }
}
