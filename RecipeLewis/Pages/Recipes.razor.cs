using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using RecipeLewis.Data;
using RecipeLewis.Models;
using RecipeLewis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.Pages
{
    public class RecipesBase : ComponentBase
    {
        public RecipesBase()
        {
            Model = new RecipeModel();
            ShowEditData = false;
            Recipes = new List<RecipeModel>();
        }
        public List<RecipeModel> Recipes { get; set; }
        [Inject]
        public RecipeService RecipeService { get; set; }
        [Inject]
        public NotificationService NotificationService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            var result = RecipeService.GetAllRecipes();
            if (result.Success)
            {
                Recipes = result.Data;
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed to load", result.Message, 6000);
            }
        }

        [Inject]
        protected DialogService DialogService { get; set; }
        public RecipeModel Model { get; set; }
        public bool ShowEditData { get; set; }
        public void Change(object value)
        {
            try
            {
                StateHasChanged();
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", ex.Message, 6000);
            }
        }
        public async Task HandleValidSubmit()
        {
            ServiceResult result;
            if (Model.RecipeID > 0)
            {
                result = await RecipeService.UpdateRecipe(Model);
            }
            else
            {
                result = await RecipeService.AddRecipe(Model);
            }
            if (result.Success)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
                ShowEditData = false;
                LoadRecipes();
                StateHasChanged();
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", result.Message, 6000);
            }
        }
        public void AddData(MouseEventArgs e)
        {
            ShowEditData = true;
            Model = new RecipeModel
            {
                CreatedDateTime = DateTime.UtcNow
            };
            StateHasChanged();
        }
        public void EditData(MouseEventArgs e, RecipeModel model)
        {
            ShowEditData = true;
            Model = model;
            StateHasChanged();
        }
        public void CancelEditData(MouseEventArgs e)
        {
            ShowEditData = false;
            StateHasChanged();
        }
        public string NewIngredient { get; set; }
        public void AddIngredient(MouseEventArgs e)
        {
            Model.Ingredients.Add(new IngredientModel() { Title = NewIngredient });
            NewIngredient = string.Empty;
        }
        public string NewStep { get; set; }
        public void AddStep(MouseEventArgs e)
        {
            Model.Steps.Add(new StepModel() { Title = NewStep });
            NewStep = string.Empty;
        }
        public async Task DeleteData()
        {
            var result = await RecipeService.DeleteRecipe(Model);
            if (result.Success)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Deleted successfully");
                ShowEditData = false;
                LoadRecipes();
                StateHasChanged();
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", result.Message, 6000);
            }
        }

        public async Task Close(dynamic result)
        {
            if (result)
            {
                await DeleteData();
            }
        }
    }
}
