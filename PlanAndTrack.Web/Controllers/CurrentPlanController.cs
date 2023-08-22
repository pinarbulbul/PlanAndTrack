using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanAndTrack.Application.Models.Plan;
using PlanAndTrack.Application.Models.TestRequest;
using PlanAndTrack.Application.Services;
using PlanAndTrack.Application.Services.Interfaces;
using PlanAndTrack.Domain.Enums;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanAndTrack.Web.Controllers
{
    [Authorize]
    public class CurrentPlanController : Controller
    {
        private readonly IPlanService _planService;

        public CurrentPlanController(IPlanService planService)
        {
            _planService = planService;
        }

        public async Task<IActionResult> CurrentPlan()
        {
            return View(await _planService.GetCurrentPlan());
        }

        [Authorize(Roles = "Manager,Tester")]
        public async Task<IActionResult> EditTestRequest(int planRequestId)
        {
            if (planRequestId == 0)
            {
                return NotFound();
            }
            var updateVm = await _planService.GetUpdateTestVmAsync(planRequestId);
            return View(updateVm);
        }

        [Authorize(Roles = "Manager,Tester")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTestRequest(UpdateTestVm updateVm)
        {
            if (updateVm.PlanTestRequestId == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _planService.UpdateTestAsync(updateVm);
                return RedirectToAction(nameof(CurrentPlan));
            }
            return View(updateVm);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finish(int id) 
        {
            await _planService.FinishAsync(id);
            return RedirectToAction("List", "Plan");
        }
    }
}

