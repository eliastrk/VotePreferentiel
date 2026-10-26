namespace VotePreferentiel;

// méthode pour comptage votes 
public interface IVoteCounter{
    // récupère les candidat
    HashSet<string> GetCandidates(VoteModel voteModel);

    // qui a gagné 
    string GetWinner(VoteModel voteModel);

    // récupère les premier choix des votant
    List<string> GetAllFirstChoices(VoteModel voteModel);

    // compte les voix de chaque candidat
    IDictionary<string, int> CountFirstChoices(List<string> firstChoices);

    // trouve les candidats avec le moins de voix
    string[] GetCandidatesWithLeastVotes(IDictionary<string, int> firstChoiceCounts);
}
