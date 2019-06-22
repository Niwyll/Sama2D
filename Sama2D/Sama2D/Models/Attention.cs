using Newtonsoft.Json;
using System.Linq;

namespace Sama2D.Models
{
    public class Attention
    {
        [JsonProperty("ratio")]
        public double Ratio { get; set; }
        [JsonProperty("alpha")]
        public double Alpha { get; set; }
        [JsonProperty("theta")]
        public double Theta { get; set; }
    }
}