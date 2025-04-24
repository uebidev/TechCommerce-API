using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Context
{
	public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		DbSet<Produto> Produtos { get; set; }
		DbSet<Categoria> Categorias { get; set; }
		DbSet<Fornecedor> Fornecedores { get; set; }
		DbSet<Avaliacao> Avaliacoes { get; set; }
		DbSet<Venda> Vendas { get; set; }
		DbSet<ItemVenda> ItemVendas { get; set; }
	}
}
