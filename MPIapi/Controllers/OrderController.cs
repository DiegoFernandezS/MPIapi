using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MPIapi.Data;
using MPIapi.Models.Dto;


namespace MPIapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        //Crear End point 
        [HttpGet] // Tipo de verbo HTTP
        [ProducesResponseType(StatusCodes.Status200OK)] // buenas practicas, documentar las respuestas
        public ActionResult<IEnumerable<OrderDto>> GetOrderDto()
        {
            return Ok(OrderList.Orders);  // (OK).... devuelve un codigo de estado: 200
        }

        //Name ... Se agrega un nombre al endpoint
        [HttpGet("Id", Name = "GetOrder")] // Id ... Retorna un objeto con ese Id de la lista Orders
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // buenas practicas para desarrolladores....
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OrderDto> GetOrderDto(int Id)
        {
            if (Id == 0)
            {
                return BadRequest(); // Codigo de estado: 400
            }

            var order = OrderList.Orders.FirstOrDefault(o => o.Id == Id);

            if (order == null)
            {
                return NotFound(); // codigo de estado: 404
            }

            return Ok(order); // codigo 200.
        }

        // endpoint metodo POST (crear)
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // buenas practicas para desarrolladores....
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OrderDto> CreateOrderDto([FromBody] OrderDto orderdto) //FromBody indica recepcion de datos.
        {
            if (!ModelState.IsValid) // si el modelo es valido
            {
                return BadRequest(ModelState);
            }
            if (OrderList.Orders.FirstOrDefault(v => v.Name.ToLower() == orderdto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "Ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (orderdto == null)
            {
                return BadRequest(orderdto);
            }
            if (orderdto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            orderdto.Id = OrderList.Orders.OrderByDescending(o => o.Id).FirstOrDefault().Id + 1;
            OrderList.Orders.Add(orderdto);
            return CreatedAtRoute("GetOrder", new { id = orderdto.Id }, orderdto);
        }

        // endpoint para el metodo DELETE
        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // buenas practicas para desarrolladores....
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteOrder(int Id) // Actionresult.... Aqui no hace falta el modelo porque devuelve un NoContent()
        {
            if (Id == 0)
            {
                return BadRequest();
            }
            var order = OrderList.Orders.FirstOrDefault(v => v.Id == Id);
            if (order == null)
            {
                return NotFound();
            }
            OrderList.Orders.Remove(order);
            return NoContent();
        }

        // endpoint para el metodo PUT  (update)
        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // buenas practicas para desarrolladores....

        public IActionResult UpdateOrder(int Id, [FromBody] OrderDto orderdto) // Recibe el Id y el objeto a actualizar
                                                                               //  porque devuelve un NoContent()
        {
            if (orderdto == null)
            {
                return BadRequest();
            }
            var order = OrderList.Orders.FirstOrDefault(v => v.Id == Id);
            order.Name = orderdto.Name;
            order.cantProductos = orderdto.cantProductos;

            return NoContent();
        }


        // endpoint para el metodo PATCH
        [HttpPatch("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // buenas practicas para desarrolladores....

        public IActionResult UpdateOrder(int Id, JsonPatchDocument<OrderDto> patchdto) // Hace referencia a la libreria agregada
        {
            if (patchdto == null || Id == 0)
            {
                return BadRequest();
            }

            var order = OrderList.Orders.FirstOrDefault(v => v.Id == Id);
            patchdto.ApplyTo(order, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
        [HttpPost("pack")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<PackingResult>> PackObjects([FromBody] PackRequest request)
        {
            if (request == null || request.Caja == null || request.Items == null || !request.Items.Any())
            {
                return BadRequest("La solicitud de empaquetado no es válida.");
            }

            var caja = new Caja
            {
                Length = request.Caja.Length,
                Width = request.Caja.Width,
                Height = request.Caja.Height
            };

            var result = AlgoritmoEmpaquetado.PackObjects(request.Items, caja);

            return Ok(result);
        }

        public class PackRequest
        {
            public Caja Caja { get; set; }
            public List<ItemDto> Items { get; set; }
        }
    }}





