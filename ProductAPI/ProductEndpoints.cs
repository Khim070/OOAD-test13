using productlib;
using ProductLib;
using RestClientLib;
using System.Net;

namespace ProductAPI
{
    public static class ProductEndpoints
    {

        public static void MapProductEndpoints(this IEndpointRouteBuilder app, string tag, string corsPolicy)
        {
            var group = app.MapGroup("api/products").WithTags(tag).RequireCors(corsPolicy);
            group.MapGet("", GetProducts).WithName(nameof(GetProducts)).RequireCors(corsPolicy);
            group.MapGet("/{key}", GetProduct).WithName(nameof(GetProduct)).RequireCors(corsPolicy);
            group.MapPost("", CreateProduct).WithName(nameof(CreateProduct)).RequireCors(corsPolicy);
            group.MapPut("", UpdateProduct).WithName(nameof(UpdateProduct)).RequireCors(corsPolicy);
            group.MapDelete("/{key}", DeleteProduct).WithName(nameof(DeleteProduct)).RequireCors(corsPolicy);
        }

        private static async Task<IResult> GetProducts(ProductService service)
        {
            var result = service.ReadAll();
            return await Task.FromResult(Results.Ok(result));
        }

        private static async Task<IResult> GetProduct(ProductService service, string key)
        {
            key = key.Trim();
            if (string.IsNullOrEmpty(key))
            {
                return await Task.FromResult(Results.BadRequest(
                                Result<ProductResponse?>.Fail(HttpStatusCode.BadRequest, "Request's key is required")));
            }

            var result = service.Read(key);
            if(result.Data == null)
            {
                result.Message = $"Not found any product with id/code, {key}";
                result.StatusCode = HttpStatusCode.NotFound;
                return await Task.FromResult(Results.NotFound(result));
            }
            return await Task.FromResult(Results.Ok(result));
        }

        private static async Task<IResult> CreateProduct(ProductService service, ProductCreateReq req)
        {
            var result = service.Create(req);
            return await Task.FromResult(
                result.StatusCode switch
                {
                    HttpStatusCode.BadRequest => Results.BadRequest(result),
                    HttpStatusCode.NotFound => Results.NotFound(result),
                    _ => Results.Created("", result)
                });
        }

        private static async Task<IResult> UpdateProduct(ProductService service, ProductUpdateReq req)
        {
            if (string.IsNullOrEmpty(req.Key))
            {
                return await Task.FromResult(Results.BadRequest(
                                   Result<string>.Fail(HttpStatusCode.BadRequest, "Request's key is required")));
            }

            var result = service.Update(req);
            if(result.Succeded == false)
            {
                return await Task.FromResult(Results.NotFound(result));
            }
            return await Task.FromResult(Results.Ok(result));
        }

        private static async Task<IResult> DeleteProduct(ProductService service, string key)
        {
            var result = service.Delete(key);
            if(result.Succeded == false)
            {
                return await Task.FromResult(Results.NotFound(result));
            }
            return await Task.FromResult(Results.Ok(result));
        }
    }
}
