using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace SchoolService.API.Filters
{
    public class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var methodName = context.MethodInfo.Name;
            var controllerName = context.MethodInfo.DeclaringType?.Name.Replace("Controller", "");

            switch (methodName)
            {
                case "GetCompanies":
                    operation.Summary = "Retrieve all schools";
                    operation.Description = "Returns a list of all schools in the system. This endpoint provides comprehensive information about each school including their basic details.";
                    break;

                case "GetSchoolById":
                    operation.Summary = "Get school by ID";
                    operation.Description = "Retrieves detailed information about a specific school using its unique identifier. Returns 404 if the school is not found.";
                    break;

                case "CreateSchool":
                    operation.Summary = "Create a new school";
                    operation.Description = "Creates a new school record in the system. Requires valid school information including name, address, and other mandatory details.";
                    break;

                case "UpdateSchool":
                    operation.Summary = "Update existing school";
                    operation.Description = "Updates the information of an existing school. All fields can be modified except the school ID. Returns 404 if the school is not found.";
                    break;

                case "DeleteSchool":
                    operation.Summary = "Delete a school";
                    operation.Description = "Removes a school from the system using its unique identifier. This operation is irreversible. Returns 404 if the school is not found.";
                    break;
            }

            // Add response descriptions
            if (operation.Responses.ContainsKey("200"))
            {
                operation.Responses["200"].Description = "Successfully completed the operation";
            }
            if (operation.Responses.ContainsKey("400"))
            {
                operation.Responses["400"].Description = "Invalid request data or parameters";
            }
            if (operation.Responses.ContainsKey("404"))
            {
                operation.Responses["404"].Description = "The requested resource was not found";
            }
            if (operation.Responses.ContainsKey("500"))
            {
                operation.Responses["500"].Description = "Internal server error occurred";
            }
        }
    }
} 