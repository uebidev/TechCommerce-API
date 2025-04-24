using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Requests
{
	public class ProdutoRequestDto
	{
		public string Nome { get; set; }  // Obrigatório
		public int CategoriaId { get; set; }  // Obrigatório
		public decimal Preco { get; set; }  // Deve ser > 0
		public int Estoque { get; set; }  // Valor inicial
		public int FornecedorId { get; set; }  // Apenas o ID, não o objeto inteiro!
	}
}
