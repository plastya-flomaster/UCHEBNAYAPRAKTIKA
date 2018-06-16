using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCHEBNAYAPRAKTIKA.Models
{
  
    public class Repo
    {
        private ProcurementRegEntities db = new ProcurementRegEntities();

        public List<ResponsiblePerson> GetPeople(Guid id)
        {
            var list = from i in db.ResponsiblePersons
                       where i.ClientKey == id
                       select i;
            return list.ToList();
        }
        public List<ResponsiblePerson> GetPeople()
        {
            var list = from i in db.ResponsiblePersons
                       select i;
            return list.ToList();
        }
    }
}