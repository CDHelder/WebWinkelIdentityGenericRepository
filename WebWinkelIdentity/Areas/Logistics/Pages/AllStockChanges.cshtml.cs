﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data;
using WebWinkelIdentity.Data.Service.Interfaces;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    public class AllStockChangesModel : PageModel
    {
        private readonly IMediator mediator;

        public AllStockChangesModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public List<ProductStockChange> ProductStockChange { get;set; }

        //TODO: Maak Functie om meerdere Rollbacks te maken (Tip: Pak de relevante rollbacks via de datum en tijd)
        public void OnGetAsync()
        {
            ProductStockChange = mediator.Send(new AllProductStockChangesStockQuery()).Result.Value;
        }
    }
}
