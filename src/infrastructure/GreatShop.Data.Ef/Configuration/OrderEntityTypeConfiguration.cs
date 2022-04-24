using GreatShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreatShop.Data.Ef.Configuration;

class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> cartConfiguration)
    {
        cartConfiguration.ToTable("carts");
        cartConfiguration.HasKey(o => o.Id);

        //orderConfiguration.Ignore(b => b.DomainEvents);

        cartConfiguration.Property<string>("Description").IsRequired(false);

        var navigation = cartConfiguration.Metadata.FindNavigation(nameof(Cart.Items));

        // DDD Patterns comment:
        //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
