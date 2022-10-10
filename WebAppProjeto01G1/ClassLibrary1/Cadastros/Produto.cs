using Modelo.Tabelas;
using System.Collections.Generic;
using System;
using System.Web;
using System.Linq;
using Modelo.Cadastros;

namespace Modelo.Cadastros
{
    public class Produto
    {
        public long? ProdutoId { get; set; }
        public string Nome { get; set; }
        public long? CategoriaId { get; set; }
        public long? FabricanteId { get; set; }
        public Categoria Categoria { get; set; }
        public Fabricante Fabricante { get; set; }
    }
}
