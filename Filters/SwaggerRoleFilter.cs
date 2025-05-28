using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementAPI.Filters
{
    public class SwaggerRoleFilter : IDocumentFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SwaggerRoleFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var userRole = _httpContextAccessor.HttpContext?.Items["UserRole"] as string;

            if (userRole != "Admin")
            {
                //  admin-only operations
                var adminOnlyOperations = new Dictionary<string, string[]>
                {
                    { "/api/auth/create-admin", new[] { "post" } },
                    { "/api/category", new[] { "post", "put", "delete" } },
                    { "/api/category/{id}", new[] { "put", "delete" } },
                    { "/api/supplier", new[] { "post", "put", "delete" } },
                    { "/api/supplier/{id}", new[] { "put", "delete" } },
                    { "/api/product/{id}", new[] { "put", "delete" } }
                };

                foreach (var path in swaggerDoc.Paths)
                {
                    if (adminOnlyOperations.ContainsKey(path.Key))
                    {
                        var operationsToRemove = adminOnlyOperations[path.Key];
                        foreach (var operation in operationsToRemove)
                        {
                            switch (operation.ToLower())
                            {
                                case "get":
                                    path.Value.Operations.Remove(OperationType.Get);
                                    break;
                                case "post":
                                    path.Value.Operations.Remove(OperationType.Post);
                                    break;
                                case "put":
                                    path.Value.Operations.Remove(OperationType.Put);
                                    break;
                                case "delete":
                                    path.Value.Operations.Remove(OperationType.Delete);
                                    break;
                            }
                        }

                        // If no operations remain, remove the entire path
                        if (path.Value.Operations.Count == 0)
                        {
                            swaggerDoc.Paths.Remove(path.Key);
                        }
                    }
                }
            }
        }
    }
} 