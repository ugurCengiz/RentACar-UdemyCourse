using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.ToTable("Models").HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("Name").IsRequired();
        builder.Property(x => x.BrandId).HasColumnName("BrandId").IsRequired();
        builder.Property(x => x.FuelId).HasColumnName("FuelId").IsRequired();
        builder.Property(x => x.TransmissionId).HasColumnName("TransmissionId").IsRequired();
        builder.Property(x => x.DailyPrice).HasColumnName("DailyPrice").IsRequired();
        builder.Property(x => x.ImageUrl).HasColumnName("ImageUrl").IsRequired();
        builder.Property(x => x.TransmissionId).HasColumnName("TransmissionId").IsRequired();


        builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(x => x.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(x => x.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(indexExpression: x => x.Name, name: "UK_Models_Name").IsUnique();

        builder.HasOne(x => x.Brand);
        builder.HasOne(x => x.Fuel);
        builder.HasOne(x => x.Transmission);
        builder.HasMany(x => x.Cars);


        builder.HasQueryFilter(x => !x.DeletedDate.HasValue);

    }
    

}