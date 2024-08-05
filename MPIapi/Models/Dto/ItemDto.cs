using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MPIapi.Models.Dto
{
    public class ItemDto
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("UnitWeight", NullValueHandling = NullValueHandling.Ignore)]
        public long? UnitWeight { get; set; }

        [JsonProperty("UnitHeigth", NullValueHandling = NullValueHandling.Ignore)]
        public long? UnitHeigth { get; set; }

        [JsonProperty("UnitLength", NullValueHandling = NullValueHandling.Ignore)]
        public long? UnitLength { get; set; }

        [JsonProperty("UnitWidth", NullValueHandling = NullValueHandling.Ignore)]
        public long? UnitWidth { get; set; }

        [Required]
        [JsonProperty("Uom", NullValueHandling = NullValueHandling.Ignore)]
        public string Uom { get; set; }

        [JsonProperty("Fragil", NullValueHandling = NullValueHandling.Ignore)]
        public bool Fragil { get; set; }

        [JsonProperty("Inflamable", NullValueHandling = NullValueHandling.Ignore)]
        public bool Inflamable { get; set; }

        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public double Volume => Length * Width * Height;

        public double PosX { get; set; }
        public double PosY { get; set; }
        public double PosZ { get; set; }
    }
}
