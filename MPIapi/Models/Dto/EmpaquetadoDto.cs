using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DocumentFormat.OpenXml.Office2010.Excel;
using Newtonsoft.Json;

namespace MPIapi.Models.Dto
{
    public class EmpaquetadoDto
    {

        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("CantidadCajas", NullValueHandling = NullValueHandling.Ignore)]
        public int? CajasTotales { get; set; }

        [JsonProperty("Cajas", NullValueHandling = NullValueHandling.Ignore)]
        public List<Caja> Cajas { get; set; }

    }
}