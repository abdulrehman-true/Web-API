using System.Text.Json.Serialization;

namespace Web_API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1,
        Mage = 2,
        Klerik = 3
        
    }
}