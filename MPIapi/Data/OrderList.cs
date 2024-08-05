using MPIapi.Models.Dto;

namespace MPIapi.Data
{
    public class OrderList
    {
        public static List<OrderDto> Orders = new List<OrderDto>
        {
            new OrderDto{Id=1, Name="Primera OrdenDtoList", cantProductos=5},
            new OrderDto{Id=2, Name="Segunda OrdenDtoList", cantProductos=10}
        };
    }
}
