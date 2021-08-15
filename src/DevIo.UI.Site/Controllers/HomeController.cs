using DevIO.UI.Site.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.UI.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;

        public HomeController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public IActionResult Index() //Pode ser usado assim tambem: [FromServices] IPedidoRepository pedidoRepository
        {
            var pedido = _pedidoRepository.ObterPedido();

            return View();
        }
    }
}
