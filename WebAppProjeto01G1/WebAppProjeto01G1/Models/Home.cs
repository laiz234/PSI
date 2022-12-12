using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProjeto01G1.Models
{
    public class Home
    {
        public IQueryable<Produto> listaProdutoLancamento;
        public IQueryable<Produto> listaProdutoDestaques;
    }
}