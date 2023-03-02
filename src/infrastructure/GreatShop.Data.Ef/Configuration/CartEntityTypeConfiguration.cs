using GreatShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreatShop.Data.Ef.Configuration;

internal class CartEntityTypeConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> conf)
    {
        conf.ToTable("carts");
        conf.HasKey(o => o.Id);

        //orderConfiguration.Ignore(b => b.DomainEvents);

        conf.Property<string>("Description").IsRequired(false);

        IMutableNavigation? navigation = conf.Metadata.FindNavigation(nameof(Cart.Items));
        // DDD Patterns comment:
        //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        
        //conf.Navigation(it => it.Items).AutoInclude();
    }
}
