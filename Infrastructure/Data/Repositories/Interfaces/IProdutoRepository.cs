using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories.Interfaces
{
	public interface IProdutoRepository
	{
		Task<IEnumerable<ProdutoResponseDto>> ObterTodosProdutosAsync();
		Task<ProdutoResponseDto> ObterProdutoPorIdAsync(int id);
		Task<IEnumerable<ProdutoResponseDto>> ObterProdutosPorCategoriaAsync(int categoriaId);
		Task<IEnumerable<ProdutoResponseDto>> ObterProdutosPorFornecedorAsync(int fornecedorId);
		Task<IEnumerable<ProdutoResponseDto>> ObterProdutosEmFaltaAsync();
		Task<IEnumerable<ProdutoResponseDto>> ObterProdutosMaisVendidosAsync(int quantidade);
	
		Task AdicionarProdutoAsync(ProdutoRequestDto produto);
		Task AtualizarProdutoAsync(ProdutoUpdateDto produto, int produtoId);
		Task RemoverProdutoAsync(int id);
	}
}
