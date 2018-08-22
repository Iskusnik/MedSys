using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSys
{
    class ControlFunctions
    {
        ModelMedContainer dbContext = new ModelMedContainer();

        Person LoginResult(string login, string password)
        {
            string fullnName = login.Split('_')[0];
            DateTime birthDate = DateTime.Parse(login.Split('_')[1]);
            Person searchResult = (Person)from Person p in dbContext.PersonSet where p.BirthDate == birthDate && p.FullName == fullnName select p;
            if (searchResult.Password == password)
                return searchResult;
            else
                return null;  
        }
    }
}
