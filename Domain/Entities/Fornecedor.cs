using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Fornecedor
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string CNPJ { get; set; }
		public bool Ativo { get; set; }
		public string Cidade { get; set; }
		public string UF { get; set; }
	}
}
