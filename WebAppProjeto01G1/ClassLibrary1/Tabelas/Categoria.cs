using System.Collections.Generic;
using System;
using System.Web;
using System.Linq;
using Modelo.Cadastros;
using Modelo.Tabelas;

namespace Modelo.Tabelas
{
    public class Categoria
    {
        public long CategoriaId { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}