namespace VotePreferentiel;

// ensemble des vote
public class VoteModel{
    public List<Vote> Votes { get; set; } = new();
}

// bulletin avec les choix ddu votant
public class Vote{
    public List<string> Choices { get; set; } = new();
}
