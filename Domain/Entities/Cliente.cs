﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Cliente
	{

		public int Id { get; set; }
		public string Nome { get; set; }
		public string CPF { get; set; }
		public bool Ativo { get; set; }
	
	}
}
