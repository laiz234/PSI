using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProjeto01G1.Areas.Seguranca.Models
{
    public class Papel
    {
        public Papel() : base() { }
        public Papel(string name) : base(name) { }

        public IEnumerable<object> Users { get; internal set; }
    }
}