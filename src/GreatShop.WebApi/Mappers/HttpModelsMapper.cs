using GreatShop.Domain.Entities;
using GreatShop.HttpModels.Responses.Common;

namespace GreatShop.WebApi.Mappers;

public class HttpModelsMapper
{
    public virtual ProductModelV1 MapProductModelV1(Product obj) 
        => new(obj.Id, obj.Name, obj.ImageUri, obj.Price);
}