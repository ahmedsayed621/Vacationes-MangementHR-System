using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vacationes_MangementHR_System.Models
{
    public class VacationPlan:EntityBase
    {
        [Display(Name ="Vacation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="0:dd-MM-yyyy")]
        public DateTime? VacationDate { get; set; }

        public int RequedstVacationId { get; set; }
        [ForeignKey("RequedstVacationId")]
        public RequestVacation? requestVacation { get; set; }
    }
}
