using VotePreferentiel;

namespace VotePreferentiel.Tests;

public class VoteCounterTests
{
    //Cas nominal
    [Test]
    public async Task GetWinner_ShouldReturnChloe(){
        // Arrange
        var voteModel = new VoteModel{
            Votes = new List<Vote>{
                new Vote { Choices = new() { "Alice", "Bilal", "Chloé" } },
                new Vote { Choices = new() { "Alice", "Chloé", "Bilal" } },
                new Vote { Choices = new() { "Bilal", "Chloé", "Alice" } },
                new Vote { Choices = new() { "Chloé", "Bilal", "Alice" } },
                new Vote { Choices = new() { "Chloé", "Alice", "Bilal" } }
            }
        };

        var counter = new VoteCounter();

        // Act
        var winner = counter.GetWinner(voteModel);

        // Assert
        await Assert.That(winner).IsEqualTo("Chloé");
    }

    //Un seul candidat 
    [Test]
    public async Task GetWinner_ShouldReturnSingleCandidate(){
        // Arrange
        var voteModel = new VoteModel{
            Votes = new List<Vote>
            {
                new Vote { Choices = new() { "Alice" } }
            }
        };

        var counter = new VoteCounter();

        // Act
        var winner = counter.GetWinner(voteModel);

        // Assert
        await Assert.That(winner).IsEqualTo("Alice");
    }

    //Report des préférences
    [Test]
    public async Task GetWinner_ShouldTransferVotes()
    {
        // Arrange
        var voteModel = new VoteModel();

        for (int i = 0; i < 4; i++){
            voteModel.Votes.Add(new Vote{
                Choices = new() { "Alice", "Bilal", "Chloé", "Diane" }
            });
        }

        for (int i = 0; i < 3; i++){
            voteModel.Votes.Add(new Vote
            {
                Choices = new() { "Bilal", "Chloé", "Alice", "Diane" }
            });
        }

        for (int i = 0; i < 3; i++){
            voteModel.Votes.Add(new Vote{
                Choices = new() { "Chloé", "Alice", "Bilal", "Diane" }
            });
        }

        voteModel.Votes.Add(new Vote{
            Choices = new() { "Diane", "Chloé", "Bilal", "Alice" }
        });

        var counter = new VoteCounter();

        // Act
        var winner = counter.GetWinner(voteModel);

        // Assert
        await Assert.That(winner).IsEqualTo("Chloé");
    }

    //Egalité en dernière position
    [Test]
    public async Task GetWinner_ShouldEliminateTiedCandidates()
    {
        // Arrange
        var voteModel = new VoteModel{
            Votes = new List<Vote>{
                new Vote { Choices = new() { "Alice", "Chloé", "Bilal" } },
                new Vote { Choices = new() { "Bilal", "Chloé", "Alice" } },
                new Vote { Choices = new() { "Chloé", "Alice", "Bilal" } },
                new Vote { Choices = new() { "Chloé", "Bilal", "Alice" } }
            }
        };

        var counter = new VoteCounter();

        // Act
        var winner = counter.GetWinner(voteModel);

        // Assert
        await Assert.That(winner).IsEqualTo("Chloé");
    }

    //Bulletin vide
    [Test]
    public async Task GetWinner_ShouldThrowIfBallotIsEmpty(){
        // Arrange
        var voteModel = new VoteModel{
            Votes = new List<Vote>{
                new Vote { Choices = new() }
            }
        };

        var counter = new VoteCounter();

        // Act & Assert
        await Assert.That(() => counter.GetWinner(voteModel)).Throws<ArgumentException>();
    }
}
