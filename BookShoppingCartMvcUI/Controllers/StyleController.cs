using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudPart3.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class StyleController : Controller
    {
        private readonly IStyleRepository _styleRepo;
        
        public StyleController(IStyleRepository styleRepo)
        {
            _styleRepo = styleRepo;
        }

        public async Task<IActionResult> Index()
        {
            var styles = await _styleRepo.GetStyles();
            return View(styles);
        }

        public IActionResult AddStyle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStyle(StyleDTO style)
        {
            if(!ModelState.IsValid)
            {
                return View(style);
            }
            try
            {
                var styleToAdd = new Style { StyleName = style.StyleName, Id = style.Id };
                await _styleRepo.AddStyle(styleToAdd);
                TempData["successMessage"] = "Style added successfully";
                return RedirectToAction(nameof(AddStyle));
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = "Style could not added!";
                return View(style);
            }

        }

        public async Task<IActionResult> UpdateStyle(int id)
        {
            var style = await _styleRepo.GetStyleById(id);
            if (style is null)
                throw new InvalidOperationException($"Style with id: {id} does not found");
            var styleToUpdate = new StyleDTO
            {
                Id = style.Id,
                StyleName = style.StyleName
            };
            return View(styleToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStyle(StyleDTO styleToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(styleToUpdate);
            }
            try
            {
                var style = new Style { StyleName = styleToUpdate.StyleName, Id = styleToUpdate.Id };
                await _styleRepo.UpdateStyle(style);
                TempData["successMessage"] = "Style is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Style could not updated!";
                return View(styleToUpdate);
            }

        }

        public async Task<IActionResult> DeleteStyle(int id)
        {
            var style = await _styleRepo.GetStyleById(id);
            if (style is null)
                throw new InvalidOperationException($"Style with id: {id} does not found");
            await _styleRepo.DeleteStyle(style);
            return RedirectToAction(nameof(Index));

        }

    }
}
