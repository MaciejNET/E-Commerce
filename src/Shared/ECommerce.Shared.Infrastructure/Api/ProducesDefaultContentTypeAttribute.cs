using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Shared.Infrastructure.Api;

public class ProducesDefaultContentTypeAttribute : ProducesAttribute
{
    public ProducesDefaultContentTypeAttribute(Type type) : base(type)
    {
    }

    public ProducesDefaultContentTypeAttribute(string contentType, params string[] additionalContentTypes) 
        : base("application/json", additionalContentTypes)
    {
    }
}