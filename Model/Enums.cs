using System.Text.Json.Serialization;

namespace Model
{
    public class Enums
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Gender
        {
            Male,
            Female
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Role
        {
            Employee,
            Manager
        }
    }
}
