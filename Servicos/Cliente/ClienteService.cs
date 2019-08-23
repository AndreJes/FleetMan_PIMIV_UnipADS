﻿using Modelo.Classes.Clientes;
using Persistencia.DAL.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Cliente
{
    public class ClienteService
    {
        private ClienteDAL Context = new ClienteDAL();

        public  IEnumerable<Modelo.Classes.Clientes.Cliente> ObterClientesOrdPorId()
        {
            return Context.ObterTodosOsClientesOrdPorId();
        }
    }
}