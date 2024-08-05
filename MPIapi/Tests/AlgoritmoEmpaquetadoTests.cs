using MPIapi.Models.Dto;
using MPIapi.Models.Logica;
using System.Collections.Generic;
using Xunit;

namespace MPIapi.Tests
{
    public class AlgoritmoEmpaquetadoTests
    {
        [Fact]
        public void Test_PackObjects_WithFlammableItems()
        {
       var caja = new Caja { Length = 100, Width = 100, Height = 100 };
        var items = new List<ItemDto>
        {
            new ItemDto { Id = 1, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 2, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = true },
            new ItemDto { Id = 3, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 4, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = true },
            new ItemDto { Id = 5, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 6, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 7, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 8, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 9, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 10, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 11, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 12, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 13, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 14, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = true },
            new ItemDto { Id = 15, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = true },
            new ItemDto { Id = 16, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 17, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 18, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 19, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false },
            new ItemDto { Id = 20, Length = 50, Width = 50, Height = 50, Uom = "cm", Fragil = false, Inflamable = false }
       };

        var resultados = AlgoritmoEmpaquetado.PackObjects(items, caja);

        foreach (var resultado in resultados)
        {
            Console.WriteLine($"Caja con eficiencia de empaquetado: {resultado.PackingEfficiency}");
            foreach (var item in resultado.PackedItems)
            {
                Console.WriteLine($"ID: {item.Id}, Inflamable: {item.Inflamable}, PosX: {item.PosX}, PosY: {item.PosY}, PosZ: {item.PosZ}");
            }
            Console.WriteLine("Objetos no empaquetados:");
            foreach (var item in resultado.UnpackedItems)
            {
                Console.WriteLine($"ID: {item.Id}, Inflamable: {item.Inflamable}");
            }
        //     [Fact]
        //     public void ConMuchosObjetosYVariando()
        //     {
        //         // Arrange
        //         var items = new List<ItemDto>
        // {
        //     new ItemDto { Id = 1, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 2, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 3, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 4, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 5, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 6, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 7, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 8, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 9, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 10, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 11, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 12, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 13, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 14, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 15, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 16, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 17, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 18, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 19, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 20, Length = 1, Width = 1, Height = 1 },
        // };

        //         var container = new Caja { Length = 4, Width = 4, Height = 2, items = items };

        //         // Act
        //         var initialResult = AlgoritmoEmpaquetado.PackObjects(items, container);
        //         var initialEfficiency = AlgoritmoEmpaquetado.EvaluateAndAdjust(container);

        //         // Reorganización
        //         var adjustedEfficiency = AlgoritmoEmpaquetado.EvaluateAndAdjust(container);

        //         // Debugging information
        //         Console.WriteLine($"Initial Packed Items: {initialResult.PackedItems.Count}");
        //         Console.WriteLine($"Initial Packing Efficiency: {initialResult.PackingEfficiency}");
        //         Console.WriteLine($"Adjusted Packing Efficiency: {adjustedEfficiency}%");

        //         // Assert
        //         Assert.True(initialEfficiency <= adjustedEfficiency, "The efficiency should not decrease after reorganization.");
        //     }

        //     [Fact]
        //     public void Test_EficienciaInicialBajaYMejoraConReorganizacion()
        //     {
        //         // Arrange
        //         var items = new List<ItemDto>
        // {
        //     new ItemDto { Id = 1, Length = 2, Width = 2, Height = 1 }, // Volumen 4
        //     new ItemDto { Id = 2, Length = 1, Width = 1, Height = 2 }, // Volumen 2
        //     new ItemDto { Id = 3, Length = 1, Width = 1, Height = 1 }, // Volumen 1
        //     new ItemDto { Id = 4, Length = 1, Width = 1, Height = 1 }, // Volumen 1
        //     new ItemDto { Id = 5, Length = 1, Width = 1, Height = 1 }, // Volumen 1
        //     new ItemDto { Id = 6, Length = 1, Width = 1, Height = 1 }  // Volumen 1
        // };

        //         var container = new Caja { Length = 4, Width = 4, Height = 2, items = items };

        //         // Inicialmente colocar solo algunos objetos en el contenedor de manera subóptima
        //         // container.Place(items[0], 0, 0, 0);
        //         // container.Place(items[1], 2, 2, 0);
        //         // container.Place(items[2], 3, 3, 0);

        //         // Act
        //         var result = AlgoritmoEmpaquetado.PackObjects(items, container);
        //         double initialEfficiency = (container.PackedObjects.Sum(o => o.Volume) / (double)container.getVol()) * 100;
        //         Console.WriteLine($"Eficiencia inicial del empaquetado: {initialEfficiency}%");

        //         // Reorganización
        //         var adjustedEfficiency = AlgoritmoEmpaquetado.EvaluateAndAdjust(container);

        //         // Debugging information
        //         Console.WriteLine($"Adjusted Packing Efficiency: {adjustedEfficiency}%");

        //         // Assert
        //         Assert.True(initialEfficiency < adjustedEfficiency, "The efficiency should improve after reorganization.");
        //     }

      //  [Fact]
        //public void Test_PackObjectsInMultipleContainers()
        //{
            // Arrange
          //  var items = new List<ItemDto>
   // {
     //   new ItemDto { Id = 1, Length = 2, Width = 2, Height = 1 }, // Volumen 4
       // new ItemDto { Id = 2, Length = 2, Width = 1, Height = 1 }, // Volumen 2
        //new ItemDto { Id = 3, Length = 2, Width = 2, Height = 2 }, // Volumen 8
        //new ItemDto { Id = 4, Length = 1, Width = 1, Height = 1 }, // Volumen 1
        //new ItemDto { Id = 5, Length = 1, Width = 1, Height = 1 }, // Volumen 1
        //new ItemDto { Id = 6, Length = 1, Width = 1, Height = 1 }, // Volumen 1
        //new ItemDto { Id = 7, Length = 2, Width = 2, Height = 1 }, // Volumen 4
        //new ItemDto { Id = 8, Length = 2, Width = 1, Height = 1 }, // Volumen 2
        //new ItemDto { Id = 9, Length = 1, Width = 1, Height = 1 }, // Volumen 1
        //new ItemDto { Id = 10, Length = 1, Width = 1, Height = 1 }, // Volumen 1
        //new ItemDto { Id = 11, Length = 1, Width = 1, Height = 1 }, // Volumen 1
        //new ItemDto { Id = 12, Length = 1, Width = 1, Height = 1 }  // Volumen 1
//    };

//            var container = new Caja { Length = 2, Width = 2, Height = 2, items = items }; // Cambiar el tamaño de la caja

            // Act
  //          var results = AlgoritmoEmpaquetado.PackObjectsInMultipleContainers(items, container);

            // Debugging information
      //      for (int i = 0; i < results.Count; i++)
    //        {
        //        Console.WriteLine($"Caja {i + 1}:");
          //      Console.WriteLine($"  Eficiencia: {results[i].PackingEfficiency}%");
              //  Console.WriteLine($"  Items empaquetados: {results[i].PackedItems.Count}");
            //    Console.WriteLine($"  Items no empaquetados: {results[i].UnpackedItems.Count}");
            //}

            // Assert
          //  Assert.True(results.Count > 1, "Debería haber más de una caja utilizada.");
         //   Assert.True(results.All(r => r.PackedItems.Count > 0), "Todas las cajas deben contener elementos empaquetados.");
        //}



        //     [Fact]
        //     public void PackObjects_ShouldReturnUnpackedItems_WhenTheyDoNotFitInContainer()
        //     {
        //         // Arrange
        //         var items = new List<ItemDto>
        //         {
        //             new ItemDto { Id = 1, Length = 2, Width = 2, Height = 2 },
        //             new ItemDto { Id = 2, Length = 2, Width = 2, Height = 2 },
        //         };

        //         var container = new Caja { Length = 2, Width = 2, Height = 2 };

        //         // Act
        //         var result = AlgoritmoEmpaquetado.PackObjects(items, container);

        //         // Assert
        //         Assert.Single(result.PackedItems);
        //         Assert.Single(result.UnpackedItems);
        //     }



        //     [Fact]
        //     public void PackObjects_ShouldHave100PercentEfficiency_WhenContainerIsFullyPacked()
        //     {
        //         // Arrange
        //         var items = new List<ItemDto>
        //         {
        //             new ItemDto { Id = 1, Length = 1, Width = 1, Height = 1 },
        //             new ItemDto { Id = 2, Length = 1, Width = 1, Height = 1 },
        //             new ItemDto { Id = 3, Length = 1, Width = 1, Height = 1 },
        //             new ItemDto { Id = 4, Length = 1, Width = 1, Height = 1 },
        //             new ItemDto { Id = 5, Length = 1, Width = 1, Height = 1 },
        //             new ItemDto { Id = 6, Length = 1, Width = 1, Height = 1 },
        //             new ItemDto { Id = 7, Length = 1, Width = 1, Height = 1 },
        //             new ItemDto { Id = 8, Length = 1, Width = 1, Height = 1 }
        //         };

        //         var container = new Caja { Length = 2, Width = 2, Height = 2 };

        //         // Act
        //         var result = AlgoritmoEmpaquetado.PackObjects(items, container);

        //         // Debugging information
        //         Console.WriteLine($"Packed Items: {result.PackedItems.Count}");
        //         Console.WriteLine($"Unpacked Items: {result.UnpackedItems.Count}");
        //         foreach (var item in result.PackedItems)
        //         {
        //             Console.WriteLine($"Packed Item ID: {item.Id}, Pos: ({item.PosX}, {item.PosY}, {item.PosZ}), Volume: {item.Volume}");
        //         }
        //         foreach (var item in result.UnpackedItems)
        //         {
        //             Console.WriteLine($"Unpacked Item ID: {item.Id}");
        //         }
        //         Console.WriteLine($"Packing Efficiency: {result.PackingEfficiency}");

        //         // Assert
        //         Assert.Equal(8, result.PackedItems.Count);
        //         Assert.Empty(result.UnpackedItems);
        //         // Assert.Equal("100%", result.PackingEfficiency);
        //     }


        //     [Fact]
        //     public void ReorganizeObjects_ShouldNotEnterInfiniteLoop_WhenEfficiencyCannotBeImproved()
        //     {

        //         System.Console.WriteLine("ESTOY ACA 2.0");
        //         // Arrange
        //         var items = new List<ItemDto>
        // {
        //     new ItemDto { Id = 1, Length = 1, Width = 1, Height = 1 },
        //     new ItemDto { Id = 2, Length = 1, Width = 1, Height = 1 },
        // };

        //         var container = new Caja { Length = 2, Width = 2, Height = 2, items = items };
        //         var result = AlgoritmoEmpaquetado.PackObjects(items, container);

        //         // Act
        //         var efficiency = AlgoritmoEmpaquetado.EvaluateAndAdjust(container);

        //         // Assert
        //         Assert.Equal(2, result.PackedItems.Count);
        //         Assert.Empty(result.UnpackedItems);
        //         Assert.Equal("25%", result.PackingEfficiency);
        //         Assert.False(result.UnpackedItems.Count > 0 && efficiency < 80);
        //     }



    
        }
    }
    }
}
