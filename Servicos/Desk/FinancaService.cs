﻿using Modelo.Classes.Desk;
using Persistencia.DAL.Desk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Desk
{
    public class FinancaService
    {
        private FinancaDAL Context = new FinancaDAL();

        public IEnumerable<Financa> ObterFinancasOrdPorId()
        {
            return Context.ObterFinancasOrdPorId();
        }

        public Financa ObterFinancaPorId(long? id)
        {
            return Context.ObterFinancaPorId(id);
        }

        public void GravarFinanca(Financa financa)
        {
            Context.GravarFinanca(financa);
        }

        public void RemoverFinancaPorId(long? id)
        {
            Context.RemoverFinancaPorId(id);
        }
    }
}
