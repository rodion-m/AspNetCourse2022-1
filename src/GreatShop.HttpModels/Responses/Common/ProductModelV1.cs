using System.ComponentModel.DataAnnotations;

namespace GreatShop.HttpModels.Responses.Common;

public record ProductModelV1(
    Guid Id,
    Guid CategoryId,
    string Name,
    string ImageUri,
    decimal Price
);