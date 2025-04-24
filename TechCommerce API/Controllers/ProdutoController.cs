using Microsoft.AspNetCore.Mvc;

namespace TechCommerce_API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProdutoController : ControllerBase
	{

		[HttpGet]
		public IActionResult GetProdutos()
		{
			return Ok();
		}

	}
}
