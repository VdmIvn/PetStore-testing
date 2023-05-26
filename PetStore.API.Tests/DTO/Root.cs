using System.Text.Json.Serialization;

namespace PetStore.API.Tests.DTO;
public class Root
{
    public int id { get; set; }
    public string name { get; set; }
    public int code { get; set; }
    public string type { get; set; }
    public string message { get; set; }
}

