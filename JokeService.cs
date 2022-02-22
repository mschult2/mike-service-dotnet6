using System.Net.Http.Json;
using System.Text.Json;

namespace App.MikeService;

public class JokeService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private const string JokeApiUrl =
        "https://karljoke.herokuapp.com/jokes/programming/random";

    public JokeService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<string> GetJokeAsync()
    {
        try
        {
            // The API returns an array with a single entry.
            Joke[]? jokes = await _httpClient.GetFromJsonAsync<Joke[]>(
                JokeApiUrl, _options);

            Joke? joke = jokes?[0];

            return joke is not null
                ? $"{joke.Setup}{Environment.NewLine}{joke.Punchline}"
                : "No joke here...";
        }
        catch (Exception ex)
        {
            return $"That's not funny! {ex}";
        }
    }
}

// A "record" is a new reference type you can use besides "class".
// It has the benfits of being:
//   1. Value-based equality (the values of the fields)
//   2. Properties can be immutable
//
// Records are useful for immutable data models, ike a service DTO.
public record Joke(int Id, string Type, string Setup, string Punchline);