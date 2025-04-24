using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Domain.Entities;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
	public class ProdutoRepository(AppDbContext context) : IProdutoRepository
	{
		public async Task AdicionarProdutoAsync(ProdutoRequestDto produto)
		{
			if (produto != null)
			{
				var entidadeProduto = new Produto
				{
					Nome = produto.Nome,
					CategoriaId = produto.CategoriaId,
					Preco = produto.Preco,
					Estoque = produto.Estoque,
					FornecedorId = produto.FornecedorId,
					Ativo = true,
					DataCadastro = DateTime.UtcNow
				};

				context.Produtos.Add(entidadeProduto);
				await context.SaveChangesAsync();
			}
			else
			{
				throw new ArgumentNullException(nameof(produto), "Produto não pode ser nulo.");
			}
		}

		public async Task AtualizarProdutoAsync(ProdutoUpdateDto produto, int produtoId)
		{
			if (produto != null)
			{
				var entidadeProduto = await context.Produtos.FindAsync(produtoId);
				if (entidadeProduto != null)
				{
					entidadeProduto.Nome = produto.Nome ?? entidadeProduto.Nome;
					entidadeProduto.CategoriaId = produto.CategoriaId != 0 ? produto.CategoriaId : entidadeProduto.CategoriaId;
					entidadeProduto.Preco = produto.Preco ?? entidadeProduto.Preco;
					entidadeProduto.Estoque = produto.Estoque ?? entidadeProduto.Estoque;
					entidadeProduto.FornecedorId = produto.FornecedorId ?? entidadeProduto.FornecedorId;
					context.Produtos.Update(entidadeProduto);
					await context.SaveChangesAsync();
				}
				else
				{
					throw new KeyNotFoundException($"Produto com ID {produtoId} não encontrado.");
				}
			}
			else
			{
				throw new ArgumentNullException(nameof(produto), "Produto não pode ser nulo.");
			}
		}

		public async Task<ProdutoResponseDto> ObterProdutoPorIdAsync(int id)
		{
			if (id > 0)
			{
				var produto = await context.Produtos
					.Where(p => p.Id == id)
					.Select(p => new ProdutoResponseDto
					{
						Id = p.Id,
						Nome = p.Nome,
						Categoria = p.Categoria.Nome,
						Preco = p.Preco,
						Estoque = p.Estoque,
						Ativo = p.Ativo,
						DataCadastro = p.DataCadastro,
						AvaliacaoMedia = p.Avaliacoes.Any() ? p.Avaliacoes.Average(a => a.Nota) : 0,
						NumeroAvaliacoes = p.Avaliacoes.Count,
						Fornecedor = new FornecedorSimplificadoDto
						{
							Id = p.Fornecedor.Id,
							Nome = p.Fornecedor.Nome
						}
					})
					.FirstOrDefaultAsync();
				return produto;
			}
			else
			{
				throw new ArgumentOutOfRangeException(nameof(id), "ID do produto deve ser maior que zero.");
			}
		}


		public async Task<IEnumerable<ProdutoResponseDto>> ObterProdutosEmFaltaAsync()
		{
			var listProdutosEmFalta = context.Produtos.Where(x => x.Estoque == 0);
			return await listProdutosEmFalta
				.Select(p => new ProdutoResponseDto
				{
					Id = p.Id,
					Nome = p.Nome,
					Categoria = p.Categoria.Nome,
					Preco = p.Preco,
					Estoque = p.Estoque,
					Ativo = p.Ativo,
					DataCadastro = p.DataCadastro,
					AvaliacaoMedia = p.Avaliacoes.Any() ? p.Avaliacoes.Average(a => a.Nota) : 0,
					NumeroAvaliacoes = p.Avaliacoes.Count,
					Fornecedor = new FornecedorSimplificadoDto
					{
						Id = p.Fornecedor.Id,
						Nome = p.Fornecedor.Nome
					}
				})
				.ToListAsync();
		}

		public async Task<IEnumerable<ProdutoResponseDto>> ObterProdutosMaisVendidosAsync(int quantidade)
		{
			if (quantidade <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade deve ser maior que zero.");
			}

			var maisVendidos = await context.ItemVendas
				.Include(iv => iv.Produto)
				.ThenInclude(p => p.Categoria)
				.Include(iv => iv.Produto.Fornecedor)
				.GroupBy(iv => iv.ProdutoId)
				.OrderByDescending(g => g.Sum(iv => iv.Quantidade))
				.Take(quantidade)
				.Select(g => new ProdutoResponseDto
				{
					Id = g.Key,
					Nome = g.First().Produto.Nome,
					Categoria = g.First().Produto.Categoria.Nome,
					Preco = g.First().Produto.Preco,
					Estoque = g.First().Produto.Estoque,
					Ativo = g.First().Produto.Ativo,
					DataCadastro = g.First().Produto.DataCadastro,
					AvaliacaoMedia = g.First().Produto.Avaliacoes.Any() ? g.First().Produto.Avaliacoes.Average(a => a.Nota) : 0,
					NumeroAvaliacoes = g.First().Produto.Avaliacoes.Count,
					Fornecedor = new FornecedorSimplificadoDto
					{
						Id = g.First().Produto.Fornecedor.Id,
						Nome = g.First().Produto.Fornecedor.Nome
					}
				})
				.ToListAsync();

			return maisVendidos;
		}

		public async Task<IEnumerable<ProdutoResponseDto>> ObterProdutosPorCategoriaAsync(int categoriaId)
		{
			if (categoriaId <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(categoriaId), "A categoria deve ser informada");
			}
			var produtosPorCategoria = await context.Produtos
				   .Where(p => p.CategoriaId == categoriaId)
				   .Select(p => new ProdutoResponseDto
				   {
					   Id = p.Id,
					   Nome = p.Nome,
					   Categoria = p.Categoria.Nome,
					   Preco = p.Preco,
					   Estoque = p.Estoque,
					   Ativo = p.Ativo,
					   DataCadastro = p.DataCadastro,
					   AvaliacaoMedia = p.Avaliacoes.Any() ? p.Avaliacoes.Average(a => a.Nota) : 0,
					   NumeroAvaliacoes = p.Avaliacoes.Count,
					   Fornecedor = new FornecedorSimplificadoDto
					   {
						   Id = p.Fornecedor.Id,
						   Nome = p.Fornecedor.Nome
					   }
				   })
				   .ToListAsync();

			return produtosPorCategoria;
		}

		public async Task<IEnumerable<ProdutoResponseDto>> ObterProdutosPorFornecedorAsync(int fornecedorId)
		{
			var produtosPorFornecedor = await context.Produtos.Where(p => p.FornecedorId == fornecedorId).Select(p => new ProdutoResponseDto
			{
				Id = p.Id,
				Nome = p.Nome,
				Categoria = p.Categoria.Nome,
				Preco = p.Preco,
				Estoque = p.Estoque,
				Ativo = p.Ativo,
				DataCadastro = p.DataCadastro,
				AvaliacaoMedia = p.Avaliacoes.Any() ? p.Avaliacoes.Average(a => a.Nota) : 0,
				NumeroAvaliacoes = p.Avaliacoes.Count,
				Fornecedor = new FornecedorSimplificadoDto
				{
					Id = p.Fornecedor.Id,
					Nome = p.Fornecedor.Nome
				}
			}).ToListAsync();
			return produtosPorFornecedor;
		}

		public async Task<IEnumerable<ProdutoResponseDto>> ObterTodosProdutosAsync()
		{
			var allProducts = await context.Produtos.Select(p => new ProdutoResponseDto
			{
				Id = p.Id,
				Nome = p.Nome,
				Categoria = p.Categoria.Nome,
				Preco = p.Preco,
				Estoque = p.Estoque,
				Ativo = p.Ativo,
				DataCadastro = p.DataCadastro,
				AvaliacaoMedia = p.Avaliacoes.Any() ? p.Avaliacoes.Average(a => a.Nota) : 0,
				NumeroAvaliacoes = p.Avaliacoes.Count,
				Fornecedor = new FornecedorSimplificadoDto
				{
					Id = p.Fornecedor.Id,
					Nome = p.Fornecedor.Nome
				}
			}).ToListAsync();
			return allProducts;
		}

		public async Task RemoverProdutoAsync(int id)
		{
			if (id <= 0)
				throw new ArgumentOutOfRangeException(nameof(id), "A categoria deve ser informada");

			var produto = await context.Produtos.FindAsync(id);
			if (produto != null)
			{
				context.Produtos.Remove(produto);
				await context.SaveChangesAsync();
			}
			else
			{
				throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");
			}
		}
	}
}
