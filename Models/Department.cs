using System.ComponentModel.DataAnnotations;

namespace Vacationes_MangementHR_System.Models
{
    public class Department:EntityBase
    {
        
        [Display(Name ="Department Name")]
        [MaxLength(100)]
        public string Name { get; set; }=string.Empty;
        public string ?Description { get; set; }
    }
}
