using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPIapi.Models.Dto
{
       public class PackingResult
    {
        public List<ItemDto> PackedItems { get; set; }
        public List<ItemDto> UnpackedItems { get; set; }
        public string PackingEfficiency { get; set; }
    }
}