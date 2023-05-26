using System.Text.Json.Serialization;

namespace PetStore.API.Tests.DTO;
public class Root
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

