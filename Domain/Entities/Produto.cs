using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Produto
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public Categoria Categoria { get; set; }
		public int CategoriaId { get; set; }
		public decimal Preco { get; set; }
		public int Estoque { get; set; }
		public bool Ativo { get; set; }
		public DateTime DataCadastro { get; set; }
		public List<Avaliacao> Avaliacoes { get; set; } = new();
		public Fornecedor Fornecedor { get; set; }
		public int FornecedorId { get; set; } 
	}
}
