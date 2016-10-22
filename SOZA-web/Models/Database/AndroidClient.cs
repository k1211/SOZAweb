using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOZA_web.Models
{
    public class AndroidClient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ApplicationUser Guardian { get; set; }
    }
}