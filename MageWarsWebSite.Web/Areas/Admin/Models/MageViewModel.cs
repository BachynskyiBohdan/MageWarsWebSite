using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MageWarsWebSite.Domain.Entities;

namespace MageWarsWebSite.Web.Areas.Admin.Models
{
    public class MageViewModel
    {
        public Mage Mage { get; set; }

        public string PrimarySchool { get; set; }
        public string SecondarySchool { get; set; }

        public List<SelectListItem> Schools { get; set; }

        public MageViewModel()
        {
            Mage = new Mage();
            Schools = new List<SelectListItem>();
            PrimarySchool = "";
            SecondarySchool = "";
        }
    }
}