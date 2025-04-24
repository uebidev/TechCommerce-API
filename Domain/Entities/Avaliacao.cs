using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Avaliacao
	{
		public int Id { get; set; }
		public Produto Produto { get; set; }
		public int ProdutoId { get; set; }
		public Cliente Cliente { get; set; }
		public int ClienteId { get; set; }
		public int Nota { get; set; } // 1 a 5
		public string Comentario { get; set; }
		public DateTime Data { get; set; }
	}
}
