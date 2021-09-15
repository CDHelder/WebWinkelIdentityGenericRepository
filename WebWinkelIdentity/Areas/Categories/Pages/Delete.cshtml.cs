using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Web.Application.Commands;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Categories.Pages
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IMediator mediator;

        public DeleteModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public Category Category { get; set; }
        public string FormResult { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = mediator.Send(new CategoryQuery(id)).Result;

            if (result.IsFailure)
            {
                FormResult = result.Error;
                return NotFound();
            }

            Category = result.Value;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = mediator.Send(new DeleteCategoryCommand(id)).Result;

            if (result.IsFailure)
            {
                FormResult = result.Error;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
