using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Supplier
    {
        public int ID { get; set; }
  
        public string Name { get; set; }


        public int SNN { get; set; }


        public string Address { get; set; }

        public byte TaxcardImage { get; set; }


        public byte RecordImage { get; set; }
    }
}