using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Responses
{
	public class ProdutoResponseDto
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Categoria { get; set; } //retorna apenas o nome da categoria
		public decimal Preco { get; set; }
		public int Estoque { get; set; }
		public bool Ativo { get; set; }
		public DateTime DataCadastro { get; set; }
		public double AvaliacaoMedia { get; set; }  // Calculado a partir das avaliações
		public int NumeroAvaliacoes { get; set; }  // Contagem, não a lista inteira
		public FornecedorSimplificadoDto Fornecedor { get; set; }  
	}
	public class FornecedorSimplificadoDto
	{
		public int Id { get; set; }
		public string Nome { get; set; }
	}
}
