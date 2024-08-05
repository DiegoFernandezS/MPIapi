using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MPIapi.Models.Dto
{
    public class PedidoDto
    {

        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("Items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ItemDto> Items { get; set; }

        //UNIDAD DE MEDIDA
        [JsonProperty("WeightUOM", NullValueHandling = NullValueHandling.Ignore)]
        public string WeightUom { get; set; }

        [JsonProperty("Weight", NullValueHandling = NullValueHandling.Ignore)]
        public long? Weight { get; set; }

        // [JsonProperty("TrackingNumber", NullValueHandling = NullValueHandling.Ignore)]
        // public string TrackingNumber { get; set; }

        // [JsonProperty("ExtendedTrackingNumber", NullValueHandling = NullValueHandling.Ignore)]
        // public string ExtendedTrackingNumber { get; set; }

        // [JsonProperty("Labels", NullValueHandling = NullValueHandling.Ignore)]
        // public List<Label> Labels { get; set; }

        // [JsonProperty("Description", NullValueHandling = NullValueHandling.Ignore)]
        // public string Description { get; set; }


        // [JsonProperty("MetroSCGCartonId", NullValueHandling = NullValueHandling.Ignore)]
        // public string MetroScgCartonId { get; set; }

        // FIJARSE LUEGO EN EL JSON POR SI HAY QUE IMPRIMIR ETIQUETA
        // [JsonProperty("PackageDetails", NullValueHandling = NullValueHandling.Ignore)]
        // public PackageDetails PackageDetails { get; set; }


    }
}