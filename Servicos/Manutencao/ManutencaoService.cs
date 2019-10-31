﻿using Modelo.Classes.Manutencao;
using Modelo.Classes.Manutencao.Associacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Manutencao
{
    public class ManutencaoService
    {
        private Persistencia.DAL.Manutencao.ManutencaoDAL Context = new Persistencia.DAL.Manutencao.ManutencaoDAL();

        public IEnumerable<Modelo.Classes.Manutencao.Manutencao> ObterManutencoesOrdPorId()
        {
            return Context.ObterManutencoesOrdPorId();
        }

        public Modelo.Classes.Manutencao.Manutencao ObterManutencaoPorId(long? id)
        {
            return Context.ObterManutencaoPorId(id);
        }

        public void AdicionarManutencao(Modelo.Classes.Manutencao.Manutencao manutencao, IList<PecasManutencao> pecas)
        {
            Context.AdicionarManutencao(manutencao, pecas);
        }

        public void AlterarManutencao(Modelo.Classes.Manutencao.Manutencao manutencao, IList<PecasManutencao> pecas)
        {
            Context.AlterarManutencao(manutencao, pecas);
        }

        public void RemoverManutencaoPorId(long? id)
        {
            Context.RemoverManutencaoPorId(id);
        }
    }
}