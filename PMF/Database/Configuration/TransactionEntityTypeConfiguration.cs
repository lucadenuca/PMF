using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMF.Database.Entities;
using PMF.Model;

namespace PMF.Database.Configuration
{                                                                               // TransactionEntity origigi
    public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public TransactionEntityTypeConfiguration()
        {
        }
                                                        //isto
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("transactions");
            // primary key
            builder.HasKey(x => x.Id);
            //builder.HasKey(x => x.Code);
            // definition of columns
            builder.Property(x => x.Id).IsRequired().HasMaxLength(64);
            builder.Property(x => x.BeneficiaryName).HasMaxLength(64);
            builder.Property(x => x.Date);
            builder.Property(x => x.Direction).HasMaxLength(64);
            builder.Property(x => x.Amount);
            builder.Property(x => x.Description).HasMaxLength(128);
            builder.Property(x => x.Currency).HasMaxLength(16);
            builder.Property(x => x.Mcc).HasMaxLength(16);
            builder.Property(x => x.Kind).HasMaxLength(32);
            /*builder.HasOne(x => x.CategoryEntity) // ovo dodato               
            .WithMany()                            
            .HasForeignKey(x => x.CatCode)         
            .OnDelete(DeleteBehavior.SetNull);*/
            

        }
    }
    
}
