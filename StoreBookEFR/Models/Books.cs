using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreBookEFR.Models
{

        public class Books
        {
            public int Id { get; set; }
            [Required]
            [MaxLength(200)]
            public string Title { get; set; }
            [Required]
            public string Description { get; set; }
            public int Price { get; set; }
            [Required]
            public DateTime Date { get; set; }

            public virtual ApplicationUser Author { get; set; }

        }
}
