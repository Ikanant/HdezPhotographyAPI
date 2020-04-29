using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HdezPhotography.Api.Helpers {
    public class ArrayModelBinder : IModelBinder { // <<< This means implementing only ONE method:
        public Task BindModelAsync (ModelBindingContext bindingContext) { // The binding context contains metadata (among other things) that I can use to implement the logic in this function 
            if (!bindingContext.ModelMetadata.IsEnumerableType) {
                bindingContext.Result = ModelBindingResult.Failed(); // <<< Fail the model binding IFF we are not dealing with an IEnumerable
                return Task.CompletedTask;
            }

            // Get the inputted value from the ValueProvider... Only thing to get the values is the pass in the model name.
            // We get the value as a string first
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();

            // If the value is null or whitespace, we return null
            if (string.IsNullOrWhiteSpace(value)) {
                // This means we have properly binded things but we return null as the value
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            // If we pass the 2 checks above.... then that means we need to get the ACTUAL enumerable type and a CONVERTER to convert to the ints we need 

            // The value is not null or white space, and the type of the model is enumerable....
            // Get the enumerable's type and a converter
            // REFLECTION: GetTypeInfo
            var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0]; // get the first generic type
            var converter = TypeDescriptor.GetConverter(elementType); // Converter to int in our case (or GUIDS for example)

            // Actually convert the values... and create an array from it
            var values = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                              .Select(s => converter.ConvertFromString(s.Trim()))
                              .ToArray();

            // Create an array of that type, and set it as the Model value
            // Use reflection with the Array.CreateInstance
            var typedValues = Array.CreateInstance(elementType, values.Length);
            values.CopyTo(typedValues, 0);

            // Once done we set the model of our binding context to our newly created typedValues
            bindingContext.Model = typedValues;

            // Return a successful result, passing in the Model
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
