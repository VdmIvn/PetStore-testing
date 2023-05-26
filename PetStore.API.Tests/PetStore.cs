using PetStore.API.Tests.DTO;
using RestSharp;
using System.Text.Json;

namespace PetStore.API.Tests;

[TestFixture]
public class APItests
{
    private RestClient _client;

    [SetUp]
    public void Setup()
    {
        var baseUrl = "https://petstore.swagger.io";
        _client = new RestClient(new RestClientOptions(baseUrl));
    }

    [Test, Order(0)]

    public void AddNewPet()
    {
        // Arrange
        var request = new RestRequest("/v2/pet", Method.Post);
        request.AddHeader("Content-Type", "application/json");


        var body = JsonSerializer.Serialize(new Root
        {
            id = 100,
            name = "Tom"
        });
        request.AddStringBody(body, DataFormat.Json);

        // Act
        RestResponse response = _client.Execute(request);
        var result = JsonSerializer.Deserialize<Root>(response.Content);

        // Assert
        Assert.AreEqual(200, (int)response.StatusCode);
        Assert.AreEqual("Tom", result.name);
    }

    [Test, Order(1)]

    public async Task FindPetByID()
    {
        // Arrange
        var request = new RestRequest("/v2/pet/100", Method.Get);

        // Act
        RestResponse response = await _client.ExecuteAsync(request);
        var result = JsonSerializer.Deserialize<Root>(response.Content);

        // Assert
        Assert.AreEqual("OK", response.StatusCode.ToString());
        Assert.AreEqual("Tom", result.name);
    }

    [Test, Order(2)]

    public async Task AttemptToFindNonexistentPet()
    {
        // Arrange
        var request = new RestRequest("/v2/pet/100000", Method.Get);

        // Act
        RestResponse response = await _client.ExecuteAsync(request);
        var result = JsonSerializer.Deserialize<Root>(response.Content);

        // Assert
        Assert.AreEqual("NotFound", response.StatusCode.ToString());
        Assert.AreEqual("Pet not found", result.message);
    }

    [Test, Order(3)]
    public async Task DeletePet()
    {
        // Arrange
        var request = new RestRequest("/v2/pet/100", Method.Delete);

        // Act
        RestResponse response = await _client.ExecuteAsync(request);
        var result = JsonSerializer.Deserialize<Root>(response.Content);

        // Assert
        Assert.AreEqual("OK", response.StatusCode.ToString());
        Assert.AreEqual("unknown", result.name);
        Assert.AreEqual("100", result.message);
    }
}
