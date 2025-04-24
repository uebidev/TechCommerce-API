﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Venda
	{
		public int Id { get; set; }
		public DateTime Data { get; set; }
		public int ClienteId { get; set; }
		public List<ItemVenda> Itens { get; set; } = new();
		public int Status { get; set; }
	}
}
