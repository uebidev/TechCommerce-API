using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class ItemVenda
	{
		public int Id { get; set; }
		public int VendaId { get; set; }
		public int ProdutoId { get; set; }
		public int Quantidade { get; set; }
		public decimal PrecoUnitario { get; set; }
	}
}
