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
		public DbSet<Produto> Produtos { get; set; }
		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<Fornecedor> Fornecedores { get; set; }
		public DbSet<Avaliacao> Avaliacoes { get; set; }
		public DbSet<Venda> Vendas { get; set; }
		public DbSet<ItemVenda> ItemVendas { get; set; }
		public DbSet<Cliente> Clientes { get; set; }
	}
}
