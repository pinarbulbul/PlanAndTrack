using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanAndTrack.Application.Models.TestRequest;
using PlanAndTrack.Application.Services.Interfaces;

namespace PlanAndTrack.Web.Controllers
{
    [Authorize]
    public class TestRequestController : Controller
    {
        private readonly ITestRequestService _testRequestService;

        public TestRequestController(ITestRequestService testRequestService)
        {
            _testRequestService = testRequestService;
        }

        public async Task<IActionResult> List()
        {
            var list = await _testRequestService.GetAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View(_testRequestService.GetCreateVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVm createVm)
        {
            if (ModelState.IsValid)
            {
                await _testRequestService.CreateAsync(createVm);

                return RedirectToAction(nameof(List));
            }
            return View(createVm);
        }

        [HttpGet]
        public async Task<FileResult?> DownloadFile(int id)
        {
            var fileBytes = await _testRequestService.GetFileBytesAsync(id);
            if (fileBytes != null)
            {
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Test Dokümanı "+ id);
            }
            return null;
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var editVm = await _testRequestService.GetUpdateVmAsync(id);
            return View(editVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateVm updateVm)
        {
            if (updateVm.Id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _testRequestService.UpdateAsync(updateVm);
                return RedirectToAction(nameof(List));
            }
            return View(updateVm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var updateVm = await _testRequestService.GetUpdateVmAsync(id);
            return View(updateVm);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            await _testRequestService.DeleteAsync(id);

            return RedirectToAction(nameof(List));
        }

        [Authorize(Roles ="Manager")]
        public async Task<IActionResult> Approve(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var editVm = await _testRequestService.GetApproveVmAsync(id);
            return View(editVm);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(ApproveVm approveVm)
        {
            if (approveVm.Id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _testRequestService.ApproveAsync(approveVm);
                return RedirectToAction(nameof(List));
            }
            return View(approveVm);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            await _testRequestService.RejectAsync(id);

            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var detailVm = await _testRequestService.GetDetailVmAsync(id);
            return View(detailVm);
        }

    }
}

