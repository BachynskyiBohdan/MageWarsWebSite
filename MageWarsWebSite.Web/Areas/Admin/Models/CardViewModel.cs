using MageWarsWebSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MageWarsWebSite.Web.Models;

namespace MageWarsWebSite.Web.Areas.Admin.Models
{
    public class CardViewModel
    {
        public Card Card { get; set; }

        public List<string> SubTypes { get; set; }
        public List<SelectListItem> SubTypesList { get; set; }

        public List<string> Types { get; set; }
        public List<SelectListItem> TypesList { get; set; }

        public List<string> Schools { get; set; }
        public List<SelectListItem> SchoolsList { get; set; }

        public CardViewModel()
        {
            Card = new Card();
            SubTypes = new List<string>();
            SubTypesList = new List<SelectListItem>();

            Types = new List<string>();
            TypesList = new List<SelectListItem>();

            Schools = new List<string>();
            SchoolsList = new List<SelectListItem>();
        }
    }
}