using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using RecipeLewis.Data;
using RecipeLewis.Models;
using RecipeLewis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

        [Inject]
        protected DialogService DialogService { get; set; }
        public RecipeModel Model { get; set; }
        public bool ShowEditData { get; set; }
        public bool ShowViewData { get; set; }
        public bool IsLoading { get; set; }
        public bool IsSaving { get; set; }
        private readonly string[] permittedExtensions = new string[] { "gif", "jpg", "jpeg", "png", "tif", "tiff" };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                DialogService.OnClose += (res) => Close(res);
                await LoadRecipes();
            }
        }

        private async Task LoadRecipes()
        {
            IsLoading = true;
            StateHasChanged();
            var result = await RecipeService.GetAllRecipes();
            if (result.Success)
            {
                Recipes = result.DataList;
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed to load", result.Message, 6000);
            }
            IsLoading = false;
            StateHasChanged();
        }
        public void ChangeTime(string value, string inputName)
        {
            try
            {
                var regexResult = Regex.Matches(value, @"([0-9]+\s+)(hour|hr|minute|min)?((s)?(\s)?(and)?(&)?(\s)?)");
                if (regexResult.Count == 0)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Invalid time entered");
                    return;
                }
                TimeSpan result = new TimeSpan();
                if (regexResult.Count == 1)
                {
                    if (!string.IsNullOrEmpty(regexResult[0].Groups[0].Value))
                    {
                        var timeStr = regexResult[0].Groups[1].Value;
                        int time = Int32.Parse(timeStr);
                        if (regexResult[0].Groups[0].Value.Contains("hour") || regexResult[0].Groups[0].Value.Contains("hr"))
                        {
                            result = result.Add(TimeSpan.FromHours(time));
                        }
                        else
                        {
                            result = result.Add(TimeSpan.FromMinutes(time));
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(regexResult[0].Groups[0].Value))
                    {
                        var hourStr = regexResult[0].Groups[1].Value;
                        int hours = Int32.Parse(hourStr);
                        result = result.Add(TimeSpan.FromHours(hours));
                    }
                    if (!string.IsNullOrEmpty(regexResult[1].Groups[0].Value))
                    {
                        var hourStr = regexResult[1].Groups[1].Value;
                        int hours = Int32.Parse(hourStr);
                        result = result.Add(TimeSpan.FromMinutes(hours));
                    }
                }
                switch (inputName)
                {
                    case "PrepTimeStr":
                        {
                            Model.PrepTime = result;
                            break;
                        }
                    case "CookTimeStr":
                        {
                            Model.CookTime = result;
                            break;
                        }
                    case "InactiveTimeStr":
                        {
                            Model.InactiveTime = result;
                            break;
                        }
                    case "TotalTimeStr":
                        {
                            Model.TotalTime = result;
                            Model.TotalTimeCalculated = false;
                            break;
                        }
                }
                if (Model.TotalTimeCalculated)
                {
                    Model.TotalTime = Model.PrepTime + Model.CookTime + Model.InactiveTime;
                    Model.TotalTimeStr = $"{Model.TotalTime.Hours} hours and {Model.TotalTime.Minutes} minutes";
                }
                NotificationService.Notify(NotificationSeverity.Success, "Parsed time to: " + result.ToString());
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", ex.Message, 6000);
            }
        }
        public async Task HandleValidSubmit()
        {
            ServiceResult result;
            IsSaving = true;
            StateHasChanged();
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
                await LoadRecipes();
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", result.Message, 6000);
            }
            IsSaving = false;
            StateHasChanged();
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
            ShowViewData = false;
            Model = model;
            StateHasChanged();
        }
        public void ViewData(MouseEventArgs e, RecipeModel model)
        {
            ShowViewData = true;
            ShowEditData = false;
            Model = model;
            StateHasChanged();
        }
        public void CancelEditData(MouseEventArgs e)
        {
            ShowEditData = false;
            ShowViewData = false;
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
            IsSaving = true;
            var result = await RecipeService.DeleteRecipe(Model);
            if (result.Success)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Deleted successfully");
                ShowEditData = false;
                await LoadRecipes();
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", result.Message, 6000);
            }
            IsSaving = false;
        }

        public async Task Close(dynamic result)
        {
            if (result)
            {
                await DeleteData();
            }
        }

        public async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            try
            {
                if(e.FileCount > 3)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Cannot upload more than 3 files at once");
                    return;
                }
                foreach (var file in e.GetMultipleFiles(3))
                {
                    if (file.Size > 10485760)
                    {
                        NotificationService.Notify(NotificationSeverity.Error, "Each file cannot exceed 10mb");
                        return;
                    }
                    var extensionIndex = file.Name.LastIndexOf('.');
                    var extension = file.Name.Substring(extensionIndex, file.Name.Length - extensionIndex).Replace(".", "");
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        NotificationService.Notify(NotificationSeverity.Error, "Invalid file extension");
                        return;
                    }
                    string imageStr = "";
                    byte[] fileBytes = new byte[0];
                    if (file.Size > 0)
                    {
                        using (var ms = file.OpenReadStream(maxAllowedSize: 10485760))
                        {
                            using (var ms2 = new MemoryStream())
                            {
                                await ms.CopyToAsync(ms2);
                                fileBytes = ms2.ToArray();
                                imageStr = Convert.ToBase64String(fileBytes);
                            }
                        }                        
                    }
                    var newDoc = new DocumentModel()
                    {
                        Bytes = fileBytes,
                        ContentType = file.ContentType,
                        Filename = file.Name,
                        Extension = extension
                    };
                    var imgSrc = $"data:image/{extension};base64,{imageStr}";
                    newDoc.ImageSource = imgSrc;
                    Model.Documents.Add(newDoc);
                }
            }
            catch(Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed to upload files", ex.Message, 6000);
            }
        }
    }
}
