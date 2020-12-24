using Microsoft.AspNetCore.Components;
using Radzen;
using RecipeLewis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.Pages
{
    public class RecipesBase: ComponentBase
    {
        public List<Recipe> Recipes { get; set; }
        [Inject]
        public RecipeService RecipeService { get; set; }
        [Inject]
        public NotificationService NotificationService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {

                //var logger = LoggerFactory.CreateLogger<Recipe>();
                //logger.LogDebug("Forecasts init!");
                Console.WriteLine("Forecasts init");
                //forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
                Recipes = await RecipeService.GetForecastsAsync();
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", ex.Message, 6000);

            }

        }
    }
}
