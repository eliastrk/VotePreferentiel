namespace VotePreferentiel;

public class VoteCounter : IVoteCounter{
    // prends tous les candidats sans les doublons
    public HashSet<string> GetCandidates(VoteModel voteModel){
        return voteModel.Votes.SelectMany(v => v.Choices).ToHashSet();
    }

    // prend le premier choix pour chaque votant
    public List<string> GetAllFirstChoices(VoteModel voteModel){
        return voteModel.Votes.Select(v => v.Choices.First()).ToList();
    }

    //compte le nombre de voix par candidat
    public IDictionary<string, int> CountFirstChoices(List<string> firstChoices){
        return firstChoices.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
    }

    //prend les candidats avec le moins de voix
    public string[] GetCandidatesWithLeastVotes(IDictionary<string, int> firstChoiceCounts){
        int minimum = firstChoiceCounts.Values.Min();

        return firstChoiceCounts.Where(c => c.Value == minimum).Select(c => c.Key).ToArray();
    }

    //calcule le candidat par éliminiation
    public string GetWinner(VoteModel voteModel){
        // si le bulletin est pas vide
        if (voteModel.Votes.Count == 0 || voteModel.Votes.Any(v => v.Choices.Count == 0)){
            throw new ArgumentException("Un bulletin est vide");
        }

        var candidates = GetCandidates(voteModel);

        //boucle tant qu'il reste des candidats (+ qu'un candidat)
        while (candidates.Count > 1){
            var firstChoices = voteModel.Votes.Select(v => v.Choices.FirstOrDefault(c => candidates.Contains(c))).Where(c => c != null).Select(c => c!).ToList();

            var counts = CountFirstChoices(firstChoices);

            // Ajout candidat avec zéro voix
            foreach (var candidate in candidates){
                if (!counts.ContainsKey(candidate)){
                    counts[candidate] = 0;
                }
            }

            //candidats a eliminer
            var eliminated = GetCandidatesWithLeastVotes(counts);

            //verifie égalité
            if (eliminated.Length == candidates.Count){
                throw new InvalidOperationException("Egalite avec les candidats");
            }

            //élimine le candidat avec le moins de voix 
            foreach (var candidate in eliminated){
                candidates.Remove(candidate);
            }
        }

        //dernier candidat restant
        return candidates.Single();
    }
}
