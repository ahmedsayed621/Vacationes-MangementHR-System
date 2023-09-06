using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vacationes_MangementHR_System.Data;
using Vacationes_MangementHR_System.Models;

namespace Vacationes_MangementHR_System.Controllers
{
    public class VacationPlansController : Controller
    {
        private readonly VacationDbContext _context;

        public VacationPlansController(VacationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.RequestVacations
                .Include(x=>x.employee).Include(x=>x.vacationType)
                .OrderByDescending(x=>x.RequestDate).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VacationPlan model, int[] DayOfWeekCheckbox)
        {
            if(ModelState.IsValid)
            {
                var result = _context.VacationPlans.Where(x => x.requestVacation.EmployeeId == model.requestVacation.EmployeeId
            && x.VacationDate >= model.requestVacation.StartDate && x.VacationDate <= model.requestVacation.EndDate).FirstOrDefault();

                if(result!= null)
                {
                    ViewBag.ErrorVacation = false;
                    return View(model);
                }
                for (DateTime date = model.requestVacation.StartDate; date <= model.requestVacation.EndDate; date=date.AddDays(1))
                {
                    if(Array.IndexOf(DayOfWeekCheckbox,(int)date.DayOfWeek)!= -1)
                    {
                        model.Id = 0;
                        model.VacationDate=date;
                        model.requestVacation.RequestDate=DateTime.Now;
                        _context.VacationPlans.Add(model);
                        _context.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Employees = _context.Employees.OrderBy(x=>x.Name).ToList();

            ViewBag.VacationTypes = _context.VacationTypes.OrderBy(x=>x.Id).ToList();

            return View(_context.RequestVacations.Include(x=>x.employee)
                .Include(x=>x.vacationType)
                .Include(x=>x.vacationPlans).FirstOrDefault(x=>x.Id==id));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(RequestVacation model)
        {
            if(ModelState.IsValid)
            {
                if(model.Approved==true)
                {
                    model.DateApproved = DateTime.Now;
                }

                _context.RequestVacations.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.Employees = _context.Employees.OrderBy(x => x.Name).ToList();
            ViewBag.VacationType = _context.VacationTypes.OrderBy(x => x.Id).ToList();
            return View(model);
        }

        public IActionResult GetVacationCount(int id)
        {
            return Json(_context.VacationPlans.Where(x=>x.RequedstVacationId==id).Count());
        }

        public IActionResult GetVacationTypes()
        {
            return Json(_context.VacationTypes.OrderBy(x=>x.Id).ToList());
        }

        public IActionResult Delete(int? id)
        {
            return View(_context.RequestVacations
                .Include(x => x.employee)
                .Include(x => x.vacationType)
                .Include(x => x.vacationPlans)
                .FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult Delete(RequestVacation model)
        {
            if (model != null)
            {
                _context.RequestVacations.Remove(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public IActionResult ViewReportVacationPlan()
        {
            ViewBag.Employees = _context.Employees.ToList();

            return View();
        }

        public IActionResult GetReportVacationPlan(int employeeId,DateTime fromDate,
            DateTime toDate)
        {
            string id = "";
            if(employeeId != 0 && employeeId.ToString() != "") {

                id = $"and Employees.Id={employeeId}";


            }

            var sqlQuery = _context.SqlDataTable($@"SELECT distinct dbo.Employees.Id, dbo.Employees.Name, dbo.Employees.VacationBalance,
                              sum(dbo.VacationTypes.NumberDays) as TotalVacations,
                              dbo.Employees.VacationBalance - sum(dbo.VacationTypes.NumberDays) as Total
                              FROM     dbo.Employees INNER JOIN
                              dbo.RequestVacations ON dbo.Employees.Id = dbo.RequestVacations.EmployeeId INNER JOIN
                              dbo.VacationPlans ON dbo.RequestVacations.Id = dbo.VacationPlans.RequedstVacationId INNER JOIN
                              dbo.VacationTypes ON dbo.RequestVacations.VacationTypeId = dbo.VacationTypes.Id
                              where VacationPlans.VacationDate between
                              '" + fromDate.ToString("yyyy-MM-dd") + "' and '" + toDate.ToString("yyyy-MM-dd") + "' " +
                              "and RequestVacations.Approved = 'True' " +
                              $"{id}  group by dbo.Employees.Id, dbo.Employees.Name, dbo.Employees.VacationBalance");


            ViewBag.Employees = _context.Employees.ToList();


            return View("ViewReportVacationPlan", sqlQuery);
        }

    }
}
