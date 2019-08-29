﻿using Modelo.Classes.Auxiliares;
using Modelo.Classes.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Classes.Clientes
{
    public abstract class Cliente
    {
        #region Props Principais
        public long? ClienteId { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        #endregion

        #region Props de carregamento
        public List<Veiculo> Veiculos{ get; set; }
        public List<Motorista> Motoristas { get; set; }
        public List<Aluguel> Alugueis { get; set; }
        #endregion

        #region Props não Mapeadas
        [NotMapped]
        public string AtivoTxt
        {
            get
            {
                return Ativo ? "Ativo" : "Inativo";
            }
        }
        #endregion

    }
}
