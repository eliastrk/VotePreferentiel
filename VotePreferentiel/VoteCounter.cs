namespace VotePreferentiel;

public class VoteCounter : IVoteCounter{
    public HashSet<string> GetCandidates(VoteModel voteModel){
        return voteModel.Votes.SelectMany(v => v.Choices).ToHashSet();
    }

    public List<string> GetAllFirstChoices(VoteModel voteModel){
        return voteModel.Votes.Select(v => v.Choices.First()).ToList();
    }

    public IDictionary<string, int> CountFirstChoices(List<string> firstChoices){
        return firstChoices.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
    }

    public string[] GetCandidatesWithLeastVotes(IDictionary<string, int> firstChoiceCounts){
        int minimum = firstChoiceCounts.Values.Min();

        return firstChoiceCounts.Where(c => c.Value == minimum).Select(c => c.Key).ToArray();
    }

    public string GetWinner(VoteModel voteModel){
        if (voteModel.Votes.Count == 0 || voteModel.Votes.Any(v => v.Choices.Count == 0)){
            throw new ArgumentException("Un bulletin est vide");
        }

        var candidates = GetCandidates(voteModel);

        while (candidates.Count > 1){
            var firstChoices = voteModel.Votes.Select(v => v.Choices.FirstOrDefault(c => candidates.Contains(c))).Where(c => c != null).Select(c => c!).ToList();

            var counts = CountFirstChoices(firstChoices);

            // Ajout candidat avec zéro voix
            foreach (var candidate in candidates){
                if (!counts.ContainsKey(candidate)){
                    counts[candidate] = 0;
                }
            }

            var eliminated = GetCandidatesWithLeastVotes(counts);

            if (eliminated.Length == candidates.Count){
                throw new InvalidOperationException("Egalite avec les candidats");
            }

            foreach (var candidate in eliminated){
                candidates.Remove(candidate);
            }
        }

        return candidates.Single();
    }
}
