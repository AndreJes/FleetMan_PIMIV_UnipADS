﻿using Persistencia.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DAL
{
    public abstract class DALContext
    {
        protected EFContext Context
        {
            get
            {
                return new EFContext();
            }
        }
    }
}
