using Core.Security;
using Core.Services;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    [PermissionCheckerByPermissionName("manage")]
    public class StatesController : Controller
    {
        private readonly ISiteToolsService _siteToolsService;
        public StatesController(ISiteToolsService siteToolsService)
        {
            _siteToolsService = siteToolsService;
        }
        public async Task<IActionResult> Index()
        {
            List<State> states = await _siteToolsService.GetStatesAsync();
            return View(states.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(State state)
        {
            if (!ModelState.IsValid)
            {
                return View(state);
            }
            _siteToolsService.CreateState(state);
            await _siteToolsService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AddFreight(int? StateId)
        {
            if (StateId == null)
            {
                return NotFound();
            }
            StateFreight stateFreight = new()
            {
                StateId = StateId.GetValueOrDefault(),
            };
            State? state = await _siteToolsService.GetStateAsync(StateId.Value);
            ViewData["StateName"] = state?.StateName;
            return View(stateFreight);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddFreight(StateFreight stateFreight)
        {
            if (!ModelState.IsValid)
            {
                return View(stateFreight);
            }
            _siteToolsService.CreateStateFreight(stateFreight);
            await _siteToolsService.SaveChangesAsync();
            return RedirectToAction(nameof(ShowStateFreights), new { stId = stateFreight.StateId });
        }
        public async Task<IActionResult> ShowStateFreights(int? stId)
        {
            if (stId == null)
            {
                return NotFound("استان مشخص نیست");
            }
            State? state = await _siteToolsService.GetStateAsync(stId.Value);
            if (state == null)
            {
                return NotFound();
            }


            return View(state);
        }
        public async Task<IActionResult> EditStateFreight(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StateFreight? stateFreight = await _siteToolsService.GetStateFreightAsync(id.Value);
            if (stateFreight == null)
            {
                return NoContent();
            }
            return View(stateFreight);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStateFreight(StateFreight stateFreight)
        {
            if (!ModelState.IsValid)
            {
                return View(stateFreight);
            }
            _siteToolsService.EditStateFreight(stateFreight);
            await _siteToolsService.SaveChangesAsync();
            return RedirectToAction(nameof(ShowStateFreights), new { stId = stateFreight.StateId });
        }

        public async Task<IActionResult> DeleteStateFreight(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StateFreight? stateFreight = await _siteToolsService.GetStateFreightAsync(id.Value);
            if (stateFreight == null)
            {
                return NoContent();
            }
            return View(stateFreight);
        }
        [HttpPost, ActionName("DeleteStateFreight")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStateFreightConfirmed(int staId)
        {
            StateFreight stateFreight = await _siteToolsService.GetStateFreightAsync(staId);
            if (stateFreight != null)
            {
                await _siteToolsService.DeleteStateFreight(staId);
                _siteToolsService.SaveChanges();
            }
            return RedirectToAction(nameof(ShowStateFreights), new { stId = staId });
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            State? state = await _siteToolsService.GetStateAsync(id.Value);
            if (state == null)
            {
                return NotFound();
            }
            return View(state);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(State state)
        {
            if (!ModelState.IsValid)
            {
                return View(state);
            }
            _siteToolsService.UpdateState(state);
            await _siteToolsService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> StateCounties(int? stId)
        {
            if (stId == null)
            {
                return NotFound();
            }
            State? state = await _siteToolsService.GetStateAsync(stId.Value);
            if (state == null)
            {
                return NotFound();
            }
            return View(state);
        }
    }
}
