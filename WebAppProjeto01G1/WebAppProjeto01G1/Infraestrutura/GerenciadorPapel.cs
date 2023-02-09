using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppProjeto01G1.Areas.Seguranca.Models;
using WebAppProjeto01G1.DAL;

namespace WebAppProjeto01G1.Infraestrutura
{
    public class GerenciadorPapel
    {
        public GerenciadorPapel(RoleStore<Papel> store) : base(store)
        {
        }
        public static GerenciadorPapel Create(IdentityFactoryOptions<GerenciadorPapel> options, IOwinContext context)
        {
            return new GerenciadorPapel(new RoleStore<Papel>(context.Get<IdentityDbContextAplicacao>()));
        }

        internal IdentityResult Delete(Papel user)
        {
            throw new NotImplementedException();
        }
    }
}