using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.CustomUI;
using Newtonsoft.Json;

namespace MPIapi.Models.Dto
{
    public class Caja
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("Height", NullValueHandling = NullValueHandling.Ignore)]
        public long? Height { get; set; }

        [JsonProperty("Length", NullValueHandling = NullValueHandling.Ignore)]
        public long? Length { get; set; }

        [JsonProperty("Width", NullValueHandling = NullValueHandling.Ignore)]
        public long? Width { get; set; }

        [JsonProperty("Tipo", NullValueHandling = NullValueHandling.Ignore)]
        public string? Tipo { get; set; }

        [JsonProperty("Item", NullValueHandling = NullValueHandling.Ignore)]
        public List<ItemDto> items { get; set; } = new List<ItemDto>();

        public List<ItemDto> PackedObjects { get; set; } = new List<ItemDto>();

        //Agregar atributo con disposición de las cajas

        public long getVol()
        {
            return (long)(Width * Height * Length);
        }

        public bool Fits(ItemDto obj, double posX, double posY, double posZ)
        {
            // Verificar si el objeto cabe dentro de las dimensiones del contenedor en la posición especificada
            if (posX + obj.Length > this.Length || posY + obj.Width > this.Width || posZ + obj.Height > this.Height)
            {
                return false;
            }

            // Verificar superposición con otros objetos ya colocados
            foreach (var packedObj in PackedObjects)
            {
                if (IsOverlapping(packedObj, obj, posX, posY, posZ))
                {
                    return false;
                }
            }

            // Si no excede las dimensiones del contenedor y no se superpone con otros objetos, entonces cabe
            return true;
        }

        private bool IsOverlapping(ItemDto packedObj, ItemDto obj, double posX, double posY, double posZ)
        {
            // Verificar superposición en todas las dimensiones
            bool overlapX = posX < (packedObj.PosX + packedObj.Length) && (posX + obj.Length) > packedObj.PosX;
            bool overlapY = posY < (packedObj.PosY + packedObj.Width) && (posY + obj.Width) > packedObj.PosY;
            bool overlapZ = posZ < (packedObj.PosZ + packedObj.Height) && (posZ + obj.Height) > packedObj.PosZ;

            return overlapX && overlapY && overlapZ;
        }

        public void Place(ItemDto obj, double posX, double posY, double posZ)
        {
            // Coloca el objeto en la posición dada y actualiza sus posiciones
            obj.PosX = posX;
            obj.PosY = posY;
            obj.PosZ = posZ;
            PackedObjects.Add(obj);
        }
    }

}
