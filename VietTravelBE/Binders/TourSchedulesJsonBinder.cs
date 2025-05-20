using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using VietTravelBE.Dtos;

namespace VietTravelBE.Binders
{
    public class TourSchedulesJsonBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                try
                {
                    var json = valueProviderResult.FirstValue;
                    var list = JsonConvert.DeserializeObject<List<TourScheduleDto>>(json);
                    bindingContext.Result = ModelBindingResult.Success(list);
                }
                catch (Exception)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid JSON format.");
                }
            }
            return Task.CompletedTask;
        }
    }
}
