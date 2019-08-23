﻿using Modelo.Classes.Desk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DAL.Desk
{
    public class MultaDAL : DALContext
    {
        public IEnumerable<Multa> ObterMultasOrdPorId()
        {
            return Context.Multas.OrderBy(m => m.MultaId);
        }
    }
}