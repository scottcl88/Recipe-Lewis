using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
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
            AllTags = new List<TagModel>();
        }

        public List<RecipeModel> OriginalRecipes { get; set; }
        public List<RecipeModel> Recipes { get; set; }

        public List<TagModel> AllTags { get; set; }
        public string AvailableTags { get; set; }
        [Inject]
        public RecipeService RecipeService { get; set; }
        [Inject]
        public TagService TagService { get; set; }


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
                OriginalRecipes = result.DataList;
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed to load", result.Message, 6000);
            }
            IsLoading = false;
            StateHasChanged();
        }
        private async Task LoadTags()
        {
            if (AllTags.Count > 0) return;//Already loadded it
            IsLoading = true;
            StateHasChanged();
            var result = await TagService.GetAllTags();
            if (result.Success)
            {
                AllTags = result.DataList.Distinct().GroupBy(x => x.Title).Select(x => x.First()).ToList();
                AvailableTags = string.Join(",", AllTags.Select(x => x.Title));
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed to load", result.Message, 6000);
            }
            IsLoading = false;
            StateHasChanged();
        }

        public async Task HandleValidSubmit()
        {
            ServiceResult result;
            IsSaving = true;
            StateHasChanged();

            //Make sure they only add new unique tags            
            List<TagModel> newTags = Model.Tags.ToList();
            foreach (var newTag in newTags)
            {
                var existingTag = AllTags.FirstOrDefault(x => x.Title == newTag.Title);
                if (existingTag != null)
                {
                    newTag.TagID = existingTag.TagID;
                }
            }
            Model.Tags = newTags;

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

        public async Task AddData(MouseEventArgs e)
        {
            await LoadTags();
            ShowEditData = true;
            Model = new RecipeModel
            {
                CreatedDateTime = DateTime.UtcNow
            };
            StateHasChanged();
        }

        public async Task EditData(MouseEventArgs e, RecipeModel model)
        {
            ShowEditData = true;
            ShowViewData = false;
            var fullModel = await RecipeService.GetRecipe(model.RecipeID);
            Model = fullModel.Data;
            await LoadTags();
            StateHasChanged();
        }

        public async Task ViewData(MouseEventArgs e, RecipeModel model)
        {
            ShowViewData = true;
            ShowEditData = false;
            var fullModel = await RecipeService.GetRecipe(model.RecipeID);
            Model = fullModel.Data;
            StateHasChanged();
        }

        public void CancelEditData(MouseEventArgs e)
        {
            ShowEditData = false;
            ShowViewData = false;
            StateHasChanged();
        }

        public void CancelEditData(DialogService ds)
        {
            ds.Close();
            ShowEditData = false;
            ShowViewData = false;
            StateHasChanged();
        }
        public string NewIngredient { get; set; }

        public void AddIngredient(MouseEventArgs e)
        {
            if (ShowViewData) return;
            int order = 1;
            if (Model.Ingredients.Any())
            {
                order = Model.Ingredients.OrderByDescending(x => x.DisplayOrder).First().DisplayOrder + 1;
            }
            Model.Ingredients.Add(new IngredientModel() { Title = NewIngredient, DisplayOrder = order });
            NewIngredient = string.Empty;
        }
        public void RemoveIngredient(int ingredientId, int tempId, DialogService ds)
        {
            if (ShowViewData) return;
            ds.Close();
            IngredientModel foundIngredient = null;
            if (ingredientId <= 0)
            {
                foundIngredient = Model.Ingredients.FirstOrDefault(x => x.TempID == tempId);
            }
            else
            {
                foundIngredient = Model.Ingredients.FirstOrDefault(x => x.IngredientID == ingredientId);
            }
            if (foundIngredient != null)
            {
                foundIngredient.DeletedDateTime = DateTime.UtcNow;
                StateHasChanged();
            }
        }

        public string NewStep { get; set; }

        public void AddStep(MouseEventArgs e)
        {
            if (ShowViewData) return;
            int order = 1;
            if (Model.Steps.Any())
            {
                order = Model.Steps.OrderByDescending(x => x.DisplayOrder).First().DisplayOrder + 1;
            }
            Model.Steps.Add(new StepModel() { Title = NewStep, DisplayOrder = order });
            NewStep = string.Empty;
        }
        public void RemoveStep(int stepId, int tempId, DialogService ds)
        {
            if (ShowViewData) return;
            ds.Close();
            StepModel foundStep = null;
            if (stepId <= 0)
            {
                foundStep = Model.Steps.FirstOrDefault(x => x.TempID == tempId);
            }
            else
            {
                foundStep = Model.Steps.FirstOrDefault(x => x.StepID == stepId);
            }
            if (foundStep != null)
            {
                foundStep.DeletedDateTime = DateTime.UtcNow;
                StateHasChanged();
            }
        }


        public async Task DeleteRecipe(DialogService ds)
        {
            if (ShowViewData) return;
            ds.Close();
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

        public string SearchText { get; set; }
        public void Search(ChangeEventArgs changeEvent)
        {
            SearchText = (string)changeEvent?.Value;
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var text = SearchText.ToLower();
                Recipes = OriginalRecipes.Where(x => (!string.IsNullOrWhiteSpace(x.Title) && x.Title.ToLower().Contains(text))
                || (!string.IsNullOrWhiteSpace(x.Author) && x.Author.ToLower().Contains(text))
                || (x.Tags.Any(y => y.Title.ToLower().Contains(text)))
                || (!string.IsNullOrWhiteSpace(x.Description) && x.Description.ToLower().Contains(text))).ToList();
            }
            else
            {
                Recipes = OriginalRecipes;
            }
            StateHasChanged();
        }
        public string NewTag { get; set; }
        public void AddTag()
        {
            if (ShowViewData) return;
            //Don't add dupe tags
            var existingTag = Model.Tags.FirstOrDefault(x => x.Title == NewTag);
            if (existingTag == null)
            {
                Model.Tags.Add(new TagModel() { Title = NewTag });
            }
            NewTag = "";
            StateHasChanged();
        }
        public void RemoveTag(int tagId, int tempId)
        {
            if (ShowViewData) return;
            TagModel foundTag = null;
            if (tagId <= 0)
            {
                foundTag = Model.Tags.FirstOrDefault(x => x.TempID == tempId);
            }
            else
            {
                foundTag = Model.Tags.FirstOrDefault(x => x.TagID == tagId);
            }
            if (foundTag != null)
            {
                foundTag.DeletedDateTime = DateTime.UtcNow;
                StateHasChanged();
            }
        }
        public void RemoveDocument(int documentId, int tempId, DialogService ds)
        {
            if (ShowViewData) return;
            ds.Close();
            DocumentModel foundDoc = null;
            if (documentId <= 0)
            {
                foundDoc = Model.Documents.FirstOrDefault(x => x.TempID == tempId);
            }
            else
            {
                foundDoc = Model.Documents.FirstOrDefault(x => x.DocumentID == documentId);
            }
            if (foundDoc != null)
            {
                foundDoc.DeletedDateTime = DateTime.UtcNow;
                StateHasChanged();
            }
        }

        public bool IsUploading { get; set; }

        public async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            try
            {
                IsUploading = true;
                if (ShowViewData) return;
                if (e.FileCount > 3)
                {
                    IsUploading = false;
                    NotificationService.Notify(NotificationSeverity.Error, "Cannot upload more than 3 files at once");
                    return;
                }
                List<DocumentModel> newDocuments = new List<DocumentModel>();
                foreach (var file in e.GetMultipleFiles(3))
                {
                    if (file.Size > DocumentModel.MaxSize)
                    {
                        IsUploading = false;
                        NotificationService.Notify(NotificationSeverity.Error, "Each file cannot exceed 10mb");
                        return;
                    }
                    var extensionIndex = file.Name.LastIndexOf('.');
                    var extension = file.Name.Substring(extensionIndex, file.Name.Length - extensionIndex).Replace(".", "");
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        IsUploading = false;
                        NotificationService.Notify(NotificationSeverity.Error, "Invalid file extension");
                        return;
                    }
                    string imageStr = "";
                    byte[] fileBytes = new byte[0];
                    if (file.Size > 0)
                    {
                        using (var ms = file.OpenReadStream(maxAllowedSize: DocumentModel.MaxSize))
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
                    newDocuments.Add(newDoc);
                }
                Model.Documents.AddRange(newDocuments);
                IsUploading = false;
            }
            catch (Exception ex)
            {
                IsUploading = false;
                NotificationService.Notify(NotificationSeverity.Error, "Failed to upload files", ex.Message, 6000);
            }
        }
    }
}