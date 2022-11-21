// using Dapper;
// using GreatShop.Domain.Entities;
//
// namespace GreatShop.Data.Dapper;
//
// public static class Tables
// {
//     public const string Products = "products";
// }
//
// public class ProductRepository
// {
//     public async Task<Product> FindById(Guid id, CancellationToken cancellationToken = default)
//     {
//         const string sql = @$"
//                 SELECT p.Id, p.CategoryId, p.Name, p.Price, p.ImageUri
//                 FROM {Tables.Products} as p
//                 WHERE p.id = @Id;";
//
//         var parameters = new
//         {
//             Id = id,
//         };
//         var commandDefinition = new CommandDefinition(
//             sql,
//             parameters: parameters,
//             commandTimeout: _timeout,
//             cancellationToken: cancellationToken
//         );
//         var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
//         return await _queryExecutor.Execute(
//             async () =>
//             {
//                 var products = await connection.QueryAsync<Product>(
//                     commandDefinition,
//                     (product) => new Product(product.Id,
//                         skuModel.Id,
//                         skuModel.Name,
//                         new ItemType(itemType.Id, itemType.Name),
//                         clothingSize?.Id is not null
//                             ? new ClothingSize(clothingSize.Id.Value, clothingSize.Name)
//                             : null,
//                         stock.Quantity,
//                         stock.MinimalQuantity));
//                 
//                 return products.First();
//             });
//     }
// }