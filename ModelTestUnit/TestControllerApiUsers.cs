using DBStorage.ClassMetier;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core;
using Xunit.Sdk;

namespace ModelTestUnit
{
    public class TestControllerApiUsers
    {
        [Fact]
        public void connexionTest()
        {
            var controller = new RISKAPI.Controllers.UsersController();

            var c = controller.connexion("") as JsonResult;
            
            Assert.Null(c);

            var c1 = controller.connexion("romain") as JsonResult;

            Assert.NotNull(c1);
            c1 = controller.connexion("romain") as JsonResult;
            Profil p = new Profil("romain");
            p.Id = 1;
            Assert.Equal(c1.Value.ToString(), p.ToString());

        }

        [Fact]
        public void inscriptionTest()
        {
            string pseudo = "oui";
            var controller = new RISKAPI.Controllers.UsersController();

            var ins = controller.inscription(pseudo);

        }
    }
}
