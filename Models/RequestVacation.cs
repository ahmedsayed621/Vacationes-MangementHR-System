using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vacationes_MangementHR_System.Models
{
    public class RequestVacation:EntityBase
    {
        [Display(Name ="Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? employee { get; set; }
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }
        [Display(Name ="Vacation Type")]
        public int VacationTypeId { get; set; }
        [ForeignKey("VacationTypeId")]
        public VacationType? vacationType { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Display(Name ="Comment")]
        public string? Comment { get; set; }

        public bool Approved { get; set; }
        public DateTime? DateApproved { get; set; }

        public List<VacationPlan> vacationPlans { get; set; } = new List<VacationPlan>();
    }
}
