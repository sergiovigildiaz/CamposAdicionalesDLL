﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamposAdicionales.Persistence
{
    class PersistenceFactory
    {
        public static Queries Queries(string conexion)
        {
            return new Queries(conexion);
        }
    }
}
