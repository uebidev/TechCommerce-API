using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Requests
{
	public class ProdutoUpdateDto
	{
		// Sem Id (virá na URL)
		public string Nome { get; set; }
		public int CategoriaId { get; set; }
		public decimal? Preco { get; set; }  // Nullable - permite atualização parcial
		public int? Estoque { get; set; }  // Nullable - permite atualização parcial
		public bool? Ativo { get; set; }  // Nullable - permite atualização parcial
		public int? FornecedorId { get; set; }  // Nullable - permite atualização parcial
												// Sem DataCadastro (imutável após criação)
												// Sem Avaliacoes (endpoint separado)
	}
}
