using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechJobs6Persistent.Data;
using TechJobs6Persistent.Models;
using TechJobs6Persistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobs6Persistent.Controllers
{
    public class EmployerController : Controller
    { 
        private readonly JobDbContext _jobDbContext;

        public EmployerController(JobDbContext jobDbContext)
        {
            _jobDbContext = jobDbContext;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Employer> employer = _jobDbContext.Employers.ToList();
            
            return View(employer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel();
            return View(addEmployerViewModel);
        }

        [HttpPost]
        public IActionResult ProcessCreateEmployerForm(AddEmployerViewModel addEmployerViewModel)
        {
            if (ModelState.IsValid)
            {
                Employer newEmployer = new Employer
                {
                    Name = addEmployerViewModel.Name,
                    Location = addEmployerViewModel.Location
                };

                _jobDbContext.Employers.Add(newEmployer);
                _jobDbContext.SaveChanges();

                return Redirect("Index");
            }

            return View(addEmployerViewModel);
        }

        public IActionResult About(int id)
        {
            Employer? newEmployer = _jobDbContext.Employers.Find(id);
            return View(newEmployer);
        }

    }
}

