using MPIapi.Models.Dto;
using System.Collections.Generic;
using System.Linq;

namespace MPIapi.Models.Logica
{
    public class AlgoritmoEmpaquetado
    {
        public static List<PackingResult> PackObjects(List<ItemDto> objects, Caja initialContainer)
        {
            var results = new List<PackingResult>();

            // Separar los artículos inflamables y no inflamables
            var nonFlammableItems = objects.Where(o => !o.Inflamable).ToList();
            var flammableItems = objects.Where(o => o.Inflamable).ToList();

            // Empaquetar objetos no inflamables
            if (nonFlammableItems.Any())
            {
                var nonFlammableResults = PackObjectsInMultipleContainers(nonFlammableItems, initialContainer);
                results.AddRange(nonFlammableResults);
            }

            // Empaquetar objetos inflamables
            if (flammableItems.Any())
            {
                var flammableResults = PackObjectsInMultipleContainers(flammableItems, initialContainer);
                results.AddRange(flammableResults);
            }

            return results;
        }

        public static List<PackingResult> PackObjectsInMultipleContainers(List<ItemDto> objects, Caja initialContainer)
        {
            var results = new List<PackingResult>();
            var remainingItems = new List<ItemDto>(objects);

            while (remainingItems.Any())
            {
                var result = PackObjectsInSingleContainer(remainingItems, initialContainer);
                results.Add(result);

                // Eliminar los objetos empaquetados de los restantes
                remainingItems = result.UnpackedItems;

                // Si aún quedan elementos sin empaquetar, crear una nueva caja
                if (remainingItems.Any())
                {
                    initialContainer = new Caja { Length = initialContainer.Length, Width = initialContainer.Width, Height = initialContainer.Height };
                }
            }

            return results;
        }

        private static PackingResult PackObjectsInSingleContainer(List<ItemDto> objects, Caja container)
        {
            var sortedObjects = objects.OrderByDescending(o => o.Volume).ToList();
            List<ItemDto> unpackedItems = new List<ItemDto>();

            foreach (var obj in sortedObjects)
            {
                bool placed = false;

                for (double x = 0; x <= container.Length - obj.Length; x += 1)
                {
                    for (double y = 0; y <= container.Width - obj.Width; y += 1)
                    {
                        for (double z = 0; z <= container.Height - obj.Height; z += 1)
                        {
                            if (container.Fits(obj, x, y, z))
                            {
                                container.Place(obj, x, y, z);
                                placed = true;
                                break;
                            }
                        }
                        if (placed) break;
                    }
                    if (placed) break;
                }

                if (!placed)
                {
                    unpackedItems.Add(obj);
                }
            }

            double efficiency = Evaluate(container);

            return new PackingResult
            {
                PackedItems = container.PackedObjects.Select(o => new ItemDto
                {
                    Id = o.Id,
                    PosX = o.PosX,
                    PosY = o.PosY,
                    PosZ = o.PosZ,
                    Length = o.Length,
                    Width = o.Width,
                    Height = o.Height,
                    Uom = o.Uom,
                    Fragil = o.Fragil,
                    Inflamable = o.Inflamable
                }).ToList(),
                UnpackedItems = unpackedItems.Select(o => new ItemDto
                {
                    Id = o.Id,
                    Length = o.Length,
                    Width = o.Width,
                    Height = o.Height,
                    Uom = o.Uom,
                    Fragil = o.Fragil,
                    Inflamable = o.Inflamable
                }).ToList(),
                PackingEfficiency = $"{efficiency}%"
            };
        }

        private static double Evaluate(Caja caja)
        {
            long totalVolume = caja.getVol();
            double usedVolume = caja.PackedObjects.Sum(o => o.Volume);

            double efficiency = (usedVolume / totalVolume) * 100;

            return efficiency;
        }
    }
}
