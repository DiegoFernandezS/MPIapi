using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPIapi.Models.Dto;

namespace MPIapi.Data
{
    public class AlgoritmoEmpaquetado
    {
         public static List<PackingResult> PackObjectsInMultipleContainers(List<ItemDto> objects, Caja initialContainer)
        {
            var results = new List<PackingResult>();
            var remainingItems = new List<ItemDto>(objects);
            Caja currentContainer = initialContainer;

            while (remainingItems.Any())
            {
                var result = PackObjects(remainingItems, currentContainer);
                results.Add(result);

                // Eliminar los objetos empaquetados de los restantes
                remainingItems = result.UnpackedItems;

                // Si aún quedan elementos sin empaquetar, crear una nueva caja
                if (remainingItems.Any())
                {
                    currentContainer = new Caja { Length = initialContainer.Length, Width = initialContainer.Width, Height = initialContainer.Height, items = remainingItems };
                }
            }

            return results;
        }

        public static PackingResult PackObjects(List<ItemDto> objects, Caja container)
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

            double efficiency = EvaluateAndAdjust(container);

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
                }).ToList(),
                UnpackedItems = unpackedItems.Select(o => new ItemDto
                {
                    Id = o.Id,
                    Length = o.Length,
                    Width = o.Width,
                    Height = o.Height,
                }).ToList(),
                PackingEfficiency = $"{efficiency}%"
            };
        }

        internal static double EvaluateAndAdjust(Caja caja)
        {
            long totalVolume = caja.getVol();
            double usedVolume = caja.PackedObjects.Sum(o => o.Volume);

            Console.WriteLine($"Total Volume: {totalVolume}");
            Console.WriteLine($"Used Volume: {usedVolume}");

            double efficiency = (usedVolume / totalVolume) * 100;
            Console.WriteLine($"Eficiencia del empaquetado: {efficiency}%");

            // Intentar reorganizar los objetos solo una vez
            if (efficiency < 80 || caja.items.Count > caja.PackedObjects.Count)
            {
                Console.WriteLine("Reorganizando los objetos para mejorar la eficiencia...");
                ReorganizeObjects(caja);
                usedVolume = caja.PackedObjects.Sum(o => o.Volume);
                efficiency = (usedVolume / totalVolume) * 100;
                Console.WriteLine($"Nueva eficiencia del empaquetado: {efficiency}%");
            }

            return efficiency;
        }

        private static void ReorganizeObjects(Caja caja)
        {
            // Guardar el estado actual de los objetos empaquetados
            var originalPackedObjects = new List<ItemDto>(caja.PackedObjects);
            caja.PackedObjects.Clear();

            // Intentar diferentes órdenes de colocación
            var permutations = GetPermutations(originalPackedObjects, originalPackedObjects.Count).Take(3); // Limitamos a 3 intentos

            double bestEfficiency = 0;
            List<ItemDto> bestPacking = null;
            int attempts = 0;

            foreach (var perm in permutations)
            {
                caja.PackedObjects.Clear();
                if (TryPlaceObjects(caja, perm.ToList(), 0))
                {
                    double usedVolume = caja.PackedObjects.Sum(o => o.Volume);
                    double efficiency = (usedVolume / caja.getVol()) * 100;
                    if (efficiency > bestEfficiency)
                    {
                        bestEfficiency = efficiency;
                        bestPacking = new List<ItemDto>(caja.PackedObjects);
                    }

                    if (caja.PackedObjects.Count == caja.items.Count && efficiency >= 80)
                    {
                        // Si todos los objetos están empaquetados y la eficiencia es >= 80%, no es necesario continuar
                        break;
                    }
                }

                attempts++;
                if (attempts >= 3)
                {
                    // Si se alcanzó el número máximo de intentos, detener el proceso
                    Console.WriteLine("Se alcanzó el número máximo de intentos.");
                    break;
                }
            }

            if (bestPacking != null)
            {
                caja.PackedObjects = bestPacking;
            }
            else
            {
                // Restaurar el estado original si no se encuentra una mejor disposición
                caja.PackedObjects = originalPackedObjects;
            }

            Console.WriteLine($"Número de intentos de reorganización: {attempts}");
        }

        private static bool TryPlaceObjects(Caja caja, List<ItemDto> objects, int index)
        {
            if (index >= objects.Count)
            {
                return true; // Todos los objetos han sido colocados con éxito
            }

            var obj = objects[index];

            for (double x = 0; x <= caja.Length - obj.Length; x += 1)
            {
                for (double y = 0; y <= caja.Width - obj.Width; y += 1)
                {
                    for (double z = 0; z <= caja.Height - obj.Height; z += 1)
                    {
                        if (caja.Fits(obj, x, y, z))
                        {
                            caja.Place(obj, x, y, z);

                            if (TryPlaceObjects(caja, objects, index + 1))
                            {
                                return true; // Si todos los objetos restantes se colocan con éxito, devuelve true
                            }

                            // Si no se pudo colocar el siguiente objeto, quitar el objeto actual y continuar
                            caja.PackedObjects.Remove(obj);
                        }
                    }
                }
            }

            return false; // No se pudo colocar el objeto actual
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                            (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
    }