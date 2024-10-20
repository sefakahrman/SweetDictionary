

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SweetDictionary.Models.Entities;

namespace SweetDictionary.Repository.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("UserId");
        builder.Property(c => c.CreatedTime).HasColumnName("CreatedTime");
        builder.Property(c => c.UpdatedTime).HasColumnName("UpdatedTime");
        builder.Property(c => c.Username).HasColumnName("Username");
        builder.Property(c => c.FirstName).HasColumnName("Firstname");
        builder.Property(c => c.LastName).HasColumnName("Lastname");
        builder.Property(c => c.Email).HasColumnName("Email");
        builder.Property(c => c.Password).HasColumnName("Password");

        builder.HasMany(x => x.Posts).WithOne(x=>x.Author).HasForeignKey(X=>X.AuthorId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Comments).WithOne(x => x.User).HasForeignKey(X => X.UserId).OnDelete(DeleteBehavior.NoAction); 

        builder.HasData(
            new User() { Id = 1,
                Email="ismail732yilmaz@gmail.com",
                FirstName = "İsmail",
                LastName = "Yılmaz",
                Password = "1213456789",
                CreatedTime = DateTime.Now,
                Username = "yılancı_osman"
            }


            );

        builder.Navigation(x => x.Posts).AutoInclude();
    }
}
