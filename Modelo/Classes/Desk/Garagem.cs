﻿using Modelo.Classes.Auxiliares;
using Modelo.Classes.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Modelo.Classes.Desk
{
    public class Garagem
    {
        #region Props principais
        public long? GaragemId { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
        public string CNPJ { get; set; }
        public int Capacidade { get; set; }
        #endregion

        #region Props não mapeadas
        [NotMapped]
        public string CNPJTxt
        {
            get
            {
                return CNPJ.FormatarCNPJ();
            }
        }

        [NotMapped]
        public string TelefoneTxt
        {
            get
            {
                return Telefone.FormatarNumTelefone();
            }
        }

        [NotMapped]
        public string EnderecoParcial
        {
            get
            {
                return $"{Endereco.Rua}, {Endereco.Numero} - {Endereco.Bairro} - {Endereco.CEP}";
            }
        }

        [NotMapped]
        public string Cidade
        {
            get
            {
                return Endereco.Cidade;
            }
        }

        [NotMapped]
        public string UF
        {
            get
            {
                return Endereco.UF.ToString();
            }
        }

        #endregion

        public List<Veiculo> Veiculos { get; set; }
    }
}
