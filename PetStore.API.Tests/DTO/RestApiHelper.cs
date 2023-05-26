using RestSharp;

namespace PetStore.API.Tests.DTO;

public class RestApiHelper
{
    public RestClient restClient;
    public RestRequest restRequest;
    public string baseUrl = "https://petstore.swagger.io";
}

