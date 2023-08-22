using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanAndTrack.Application.Models.Plan;
using PlanAndTrack.Application.Services.Interfaces;
using PlanAndTrack.Domain.Enums;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanAndTrack.Web.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        public async Task<IActionResult> List()
        {
            var list = await _planService.GetAllAsync();
            return View(list);
        }


        [Authorize(Roles = "Manager")]
        public IActionResult CreateAsync()
        {
            return View(_planService.GetCreateVmAsync());
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVm createVm, string submitPlan) 
        {
            if (ModelState.IsValid)
            {
                createVm.AppliedType = (PlanTypes)Enum.Parse(typeof(PlanTypes), submitPlan, true); 
                await _planService.CreateAsync(createVm);

                return RedirectToAction(nameof(List));
            }
            return View(await _planService.GetPlanTasksAsync(createVm));
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlanTasksAsync(CreateVm createVm)
        {
            if (ModelState.IsValid)
            {
                return View("Create", await _planService.GetPlanTasksAsync(createVm));
            }
            return View("Create", createVm);
        }

        public async Task<IActionResult> DetailAsync(int id)
        {
            return View(await _planService.GetDetailVmAsync(id));
        }
    }
}

