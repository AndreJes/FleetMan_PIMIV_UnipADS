﻿using Modelo.Classes.Desk;
using Persistencia.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DAL.Desk
{
    public class MultaDAL
    {
        public IEnumerable<Multa> ObterMultasOrdPorId()
        {
            try
            {
                using EFContext Context = new EFContext();
                return Context.Multas.OrderBy(m => m.MultaId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Multa ObterMultaPorId(long? id)
        {
            try
            {
                using EFContext Context = new EFContext();
                return Context.Multas.Where(m => m.MultaId == id).Include(m => m.Veiculo).Include(m => m.Motorista).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GravarMulta(Multa multa)
        {
            try
            {
                using EFContext Context = new EFContext();
                if (multa.MultaId == null)
                {
                    Context.Multas.Add(multa);
                }
                else
                {
                    AttachItem(multa, Context);
                    Context.Entry(multa).State = EntityState.Modified;
                }
                Context.SaveChanges();
            }
            catch (DbUpdateException ex) when ((ex.InnerException.InnerException is SqlException && (ex.InnerException.InnerException as SqlException).Number == 2601))
            {
                throw new Exception("Já existe multa com Código idêntico registrada", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemoverMultaPorId(long? id)
        {
            try
            {
                using EFContext Context = new EFContext();
                Multa multa = ObterMultaPorId(id);
                AttachItem(multa, Context);
                Context.Multas.Remove(multa);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AttachItem(Multa multa, EFContext context)
        {
            if (!context.Multas.Local.Contains(multa))
            {
                context.Multas.Attach(multa);
            }
        }
    }
}
