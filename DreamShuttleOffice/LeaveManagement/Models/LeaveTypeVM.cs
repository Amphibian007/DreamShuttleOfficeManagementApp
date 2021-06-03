using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Models
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name="Date of Creation")]
        public DateTime? DateCreated { get; set; }
    }
 
}
