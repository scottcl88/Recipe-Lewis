﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeLewis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecipeLewis.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Moderator")]
    public class DownloadRecipesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DownloadRecipesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var recipes = _context.Recipes.ToList();
            string json = JsonConvert.SerializeObject(recipes);
            Response.Headers.Add("Content-Disposition", "attachment; filename=RecipeData.json");
            return new FileContentResult(System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(json), "application/json");
        }
    }
}