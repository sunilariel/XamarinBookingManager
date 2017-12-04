using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_App.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string CreationDate { get; set; }
        public string EntityStatus { get; set; }
    }

    public class AssignCategory
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string CreationDate { get; set; }
        public string EntityStatus { get; set; }
        public bool Confirmed { get; set; }
    }
}
