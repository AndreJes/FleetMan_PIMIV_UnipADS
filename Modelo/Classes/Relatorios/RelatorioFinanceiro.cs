﻿using Modelo.Classes.Desk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Classes.Relatorios
{
    public class RelatorioFinanceiro: Relatorio
    {
        public List<Financa> Financas { get; set; }
    }
}