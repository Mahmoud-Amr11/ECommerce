using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace ECommerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(e => e.Value.Errors.Any())
                .Select(e => new ValidationError
                {
                    Field = e.Key,
                    Errors = e.Value.Errors.Select(x => x.ErrorMessage)
                } );
            var response = new ValidationErrorToReturn
            {
                ValidationErrors = errors
            };
            return new BadRequestObjectResult(response);
        }
    }
}
