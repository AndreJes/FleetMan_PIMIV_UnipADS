﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Classes.Usuarios
{
    public abstract class Usuario
    {
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
