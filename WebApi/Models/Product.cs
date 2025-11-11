using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Xml.Linq;

namespace WebApi.Models
{
    public class Product
    {

        
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

   
}