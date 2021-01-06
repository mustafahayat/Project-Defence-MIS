using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Project_MIS.Models
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
    }
}
