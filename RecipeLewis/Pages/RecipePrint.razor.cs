using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;
using RecipeLewis.Data;
using RecipeLewis.Models;
using RecipeLewis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecipeLewis.Pages
{
    public class RecipePrintBase : ComponentBase
    {
        public RecipePrintBase()
        {
            Model = new RecipeModel();
        }
        [Parameter]
        public int RecipeID { get; set; }
        [Inject]
        public RecipeService RecipeService { get; set; }
        [Inject]
        public NotificationService NotificationService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        public RecipeModel Model { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadRecipe();
                await JSRuntime.InvokeVoidAsync("window.print");
            }
        }

        private async Task LoadRecipe()
        {
            var result = await RecipeService.GetRecipe(RecipeID);
            if (result.Success)
            {
                Model = result.Data;
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed to load", result.Message, 6000);
            }
            StateHasChanged();
        }
        public async Task Print(MouseEventArgs e)
        {
            await JSRuntime.InvokeVoidAsync("window.print");
        }
        public async Task Close(MouseEventArgs e)
        {
            await JSRuntime.InvokeVoidAsync("window.history.back");
        }
    }
}
