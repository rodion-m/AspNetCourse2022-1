using GreatShop.HttpModels.Responses.Common;

namespace GreatShop.HttpModels.Responses.Catalog.V1;

public record GetProductsResponseV1(IEnumerable<ProductModelV1> Products);