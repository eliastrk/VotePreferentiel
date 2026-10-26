using System.Net.Http;
using System.Net.Http.Json;

namespace VotePreferentiel;

public class VoteProvider
{
    private readonly HttpClient _httpClient;

    // réc upère client http
    public VoteProvider(HttpClient httpClient){
        _httpClient = httpClient;
    }

    // télécharge le json pour le mettre en objet VoteModel
    // pour l'utiliser le json exemple ou celui avec 100k lignes, mettre en commentaire l'autre
    public async Task<VoteModel> GetVotes(){
        //string url = "https://raw.githubusercontent.com/JLou/dotnet-course/refs/heads/main/eval-1/votes.example.json";
        string url = "https://raw.githubusercontent.com/JLou/dotnet-course/refs/heads/main/eval-1/votes.json";

        var votes = await _httpClient.GetFromJsonAsync<VoteModel>(url);

        return votes ?? throw new Exception("Impossible de récupérer les vote");
    }
}
