﻿using Modelo.Classes.Desk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Classes.Relatorios
{
    public class RelatorioAcidentes : Relatorio
    {
        public List<Sinistro> Sinistros { get; set; }

    }
}