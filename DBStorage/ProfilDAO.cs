﻿using System;
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
        public string FindByIdProfil(int id);
        public bool VerifUserCreation(Profil profil);
        public int FindIdByPseudoProfil(string pseudo);
    }
}