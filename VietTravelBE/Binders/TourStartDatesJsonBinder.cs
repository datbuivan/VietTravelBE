using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using VietTravelBE.Dtos;

namespace VietTravelBE.Binders
{
    public class TourStartDatesJsonBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            if (valueProviderResult == ValueProviderResult.None)
            {
                // Không có giá trị, trả về danh sách rỗng
                bindingContext.Result = ModelBindingResult.Success(new List<TourStartDateDto>());
                return Task.CompletedTask;
            }


            try
            {
                var json = valueProviderResult.FirstValue;

                if (string.IsNullOrEmpty(json))
                {
                    bindingContext.Result = ModelBindingResult.Success(new List<TourStartDateDto>());
                    return Task.CompletedTask;
                }

                var list = JsonConvert.DeserializeObject<List<TourStartDateDto>>(json);
                bindingContext.Result = ModelBindingResult.Success(list);
            }
            catch (Exception)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid JSON format.");
            }

            return Task.CompletedTask;
        }
    }
}
