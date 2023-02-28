using DataModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace App_Data.Context
{
    public partial class UserContext : DbContext
    {
        public virtual DbSet<UserTokenTbl> UserTokenTbl { get; set; }
        public virtual DbSet<UserTbl> IdentityTbl { get; set; }
        public virtual DbSet<ApiLogEntry> ApiLogEntry { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<PayLog> PayLog { get; set; }
        public virtual DbSet<Regin> Regin { get; set; }
        public virtual DbSet<PreClient> PreClient { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().Property(e => e.Title).HasMaxLength(50);
            

            modelBuilder.Entity<Client>().Property(e => e.DocumentUploadedPath).IsUnicode(false);

            modelBuilder.Entity<Client>().Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

            modelBuilder.Entity<Client>().Property(e => e.Family)
                    .IsRequired()
                    .HasMaxLength(80);

            modelBuilder.Entity<Client>().Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

            modelBuilder.Entity<Client>().Property(e => e.NationalCode).HasMaxLength(10).IsFixedLength(); 

            modelBuilder.Entity<Client>().Property(e => e.Occupation).HasMaxLength(50);

            modelBuilder.Entity<Client>().Property(e => e.Phone).HasMaxLength(11).IsFixedLength(); 

            modelBuilder.Entity<Client>().Property(e => e.TellPhone).HasMaxLength(11).IsFixedLength(); 

            modelBuilder.Entity<Client>().Property(e => e.VisitPlace).HasMaxLength(50);

            modelBuilder.Entity<Client>().HasRequired(d => d.Region)
                .WithMany(p => p.Client)
                .HasForeignKey(d => d.RegionId);
      

            modelBuilder.Entity<PayLog>()
                 .Property(e => e.Cost).HasColumnType("decimal(18, 0)");
            modelBuilder.Entity<PayLog>()
                            .Property(e => e.Guid)
                               .HasColumnName("guid")
                               .HasMaxLength(16)
                               .IsUnicode(false);

            modelBuilder.Entity<PayLog>().HasRequired(d => d.Client)
                               .WithMany(p => p.PayLog)
                               .HasForeignKey(d => d.ClientId);


            modelBuilder.Entity<Regin>().Property(e => e.Code).HasMaxLength(10).IsFixedLength();
            modelBuilder.Entity<Regin>().Property(e => e.Name).HasMaxLength(50);
            modelBuilder.Entity<Regin>().HasRequired(d => d.City)
                    .WithMany(p => p.Regin)
                    .HasForeignKey(d => d.CityId);
            
            modelBuilder.Entity<UserTbl>()
                 .HasKey(a => a.UserId)
                 .Property(e => e.Birthdate).HasColumnType("datetime");

            modelBuilder.Entity<UserTbl>().Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

            modelBuilder.Entity<UserTbl>().Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

            modelBuilder.Entity<UserTbl>().Property(e => e.Mobile).HasMaxLength(11);

            modelBuilder.Entity<UserTbl>().Property(e => e.Telephone).HasMaxLength(13);
            modelBuilder.Entity<UserTbl>().Property(e => e.City).HasMaxLength(100);
            modelBuilder.Entity<UserTbl>().HasMany(a => a.UserTokenTbl)
           .WithRequired(b => b.UserTbl);





            modelBuilder.Entity<UserTokenTbl>()

                .HasKey(e => e.Id);
            modelBuilder.Entity<UserTokenTbl>().HasRequired(d => d.UserTbl)
        .WithMany(p => p.UserTokenTbl)
        .HasForeignKey(d => d.UserId);

        }
    }

 

}
