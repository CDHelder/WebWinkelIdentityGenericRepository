using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Web.Application.Commands;

namespace WebWinkelIdentity.Web.Areas.Brands.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Brand Brand { get; set; }
        public string FormResult { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = mediator.Send(new CreateBrandCommand(Brand)).Result;

            if (result.IsFailure)
            {
                FormResult = result.Error;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
