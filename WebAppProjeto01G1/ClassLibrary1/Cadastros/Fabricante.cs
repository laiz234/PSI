using System.Collections.Generic;
using System;
using System.Web;
using System.Linq;
using Modelo.Cadastros;

namespace Modelo.Cadastros
{
    public class Fabricante
    {
        public long FabricanteId { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}