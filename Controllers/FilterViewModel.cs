using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Projekt_DMP.Controllers
{
    public class FilterViewModel
    {
        public int RssFeedId { get; set; }

        [Required(ErrorMessage = "Zadejte datum od.")]
        [DataType(DataType.Date)]
        [Display(Name = "Od")]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "Zadejte datum do.")]
        [DataType(DataType.Date)]
        [Display(Name = "Do")]
        public DateTime ToDate { get; set; }
    }

}

