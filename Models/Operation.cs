using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Operation
    {
        public int Id { get; set; }


        public DateTime BillDate { get; set; }


        public int BillNumber { get; set; }


        public int DeliveryReceiptNumber { get; set; }


        public string Type { get; set; }


        public string Unit { get; set; }


        public int PriceInTons { get; set; }


        public int Quantity { get; set; }


        public int Tax14 { get; set; }


        public int Tax1 { get; set; }


        public DateTime DueDate { get; set; }

        public string Creditor { get; set; }
        public string Debtor { get; set; }


        public int Subtotal { get; set; }


        public string Notes { get; set; }
    }
}