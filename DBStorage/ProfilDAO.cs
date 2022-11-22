using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBStorage.ClassMetier;

namespace DBStorage
{
    public interface ProfilDAO
    {
        public void Insert(Profil profil);
        public void Update(Profil profil);
        public void Delete(Profil profil);
        public string FindByIdProfil(string pseudo);
        public string FindAll();
        public bool VerifUserCreation(Profil profil);
    }
}
