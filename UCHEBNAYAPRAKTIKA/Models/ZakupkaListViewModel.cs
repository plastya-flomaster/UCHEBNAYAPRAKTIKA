using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCHEBNAYAPRAKTIKA.Models
{
    public class ZakupkaListViewModel
    {
        
            public IEnumerable<Zakupka> Zakupkas { get; set; }
            public SelectList Regions { get; set; }
            //public SelectList Clients { get; set; }
      }
}