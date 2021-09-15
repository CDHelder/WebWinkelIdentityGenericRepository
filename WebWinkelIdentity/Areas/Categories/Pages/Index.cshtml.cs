using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Categories.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public List<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = mediator.Send(new AllCategoriesQuery()).Result.Value;
        }
    }
}
