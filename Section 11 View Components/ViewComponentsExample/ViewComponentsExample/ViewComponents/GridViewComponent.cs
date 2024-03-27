using Microsoft.AspNetCore.Mvc;
using ViewComponentsExample.Models;

namespace ViewComponentsExample.ViewComponents
{
    public class GridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PersonGridModel personGridModel)
        {
            return View("Sample", personGridModel); //invokes a partial view in Views/Shared/Components/Grid/Sample.cshtml
        }
    }
}
