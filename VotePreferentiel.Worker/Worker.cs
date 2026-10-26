using VotePreferentiel;

namespace VotePreferentiel.Worker;

public class Worker : BackgroundService{
    private readonly VoteProvider _voteProvider;
    private readonly IVoteCounter _voteCounter;

    public Worker(VoteProvider voteProvider, IVoteCounter voteCounter){
        _voteProvider = voteProvider;
        _voteCounter = voteCounter;
    }

    //récupère les votes, ensuite il calcule le gagnant puis l'affiche
    protected override async Task ExecuteAsync(CancellationToken stoppingToken){
        var voteModel = await _voteProvider.GetVotes();

        Console.WriteLine($"Nombre votes : {voteModel.Votes.Count}");

        string winner = _voteCounter.GetWinner(voteModel);

        Console.WriteLine($"Le gagnant : {winner}");
    }
}
