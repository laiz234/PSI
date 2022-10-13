﻿using Modelo.Cadastros;
using Persistenciaa.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistenciaa.DAL.Cadastro
{
    public class FabricanteDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Fabricante> ObterFabricantesClassificadosPorNome()
        {
            return context.Fabricantes.OrderBy(b => b.Nome);
        }
    }
}
