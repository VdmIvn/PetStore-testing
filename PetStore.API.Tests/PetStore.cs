using PetStore.API.Tests.DTO;
using RestSharp;
using System.Text.Json;

namespace PetStore.API.Tests;

[TestClass]
public class APItests
{

    [TestMethod]
    public async Task AddNewPet()
    {
        // Arrange
        var options = new RestClientOptions("https://petstore.swagger.io")
        {
            MaxTimeout = -1,
        };
        var client = new RestClient(options);
        var request = new RestRequest("/v2/pet", Method.Post);
        request.AddHeader("Content-Type", "application/json");

        var expected = new Root
        {
            Name = "Tom",
        };
    //    var body = @"{
    //" + "\n" +
    //@"    ""id"": 100,
    //" + "\n" +
    //@"    ""name"": ""Tom""
    //" + "\n" + @"}";

        var body = JsonSerializer.Serialize(new Root
        {
            Id = 100,
            Name = "Tom"
        });
        request.AddStringBody(body, DataFormat.Json);

        // Act
        RestResponse response = await client.ExecuteAsync(request);
        var result = JsonSerializer.Deserialize<Root>(response.Content);

        // Assert
        Assert.AreEqual(200, (int)response.StatusCode);
        Assert.AreEqual(expected.Name, result.Name);
    }

    [TestMethod]

    public async Task FindPetByID()
    {
        var options = new RestClientOptions("https://petstore.swagger.io")
        {
            MaxTimeout = -1,
        };
        var client = new RestClient(options);
        var request = new RestRequest("/v2/pet/100", Method.Get);
        RestResponse response = await client.ExecuteAsync(request);
        Console.WriteLine(response.Content);

        Assert.AreEqual("OK", response.StatusCode.ToString());
    }

    [TestMethod]

    public async Task AttemptToFindUnexistingPet()
    {
        var options = new RestClientOptions("https://petstore.swagger.io")
        {
            MaxTimeout = -1,
        };
        var client = new RestClient(options);
        var request = new RestRequest("/v2/pet/100000", Method.Get);
        RestResponse response = await client.ExecuteAsync(request);
        Console.WriteLine(response.Content);

        Assert.AreEqual("NotFound", response.StatusCode.ToString());
        //Assert.AreEqual("Pet not found", response.message);
    }

}
