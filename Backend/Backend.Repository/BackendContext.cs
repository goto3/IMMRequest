using Backend.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class BackendContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> Sessions { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicType> TopicTypes { get; set; }
        public DbSet<AdditionalField> AdditionalFields { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<AdditionalFieldData> AdditionalFieldsData { get; set; }

        public BackendContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSession>().HasKey(us => us.Token);
            modelBuilder.Entity<Request>().HasOne(r => r.TopicType);
            modelBuilder.Entity<Request>().HasOne(r => r.Applicant);
            modelBuilder.Entity<Area>().HasMany(a => a.Topics).WithOne(t => t.Area);
            modelBuilder.Entity<Topic>().HasMany(t => t.TopicTypes).WithOne(tt => tt.Topic);
            modelBuilder.Entity<TopicType>().HasMany(tt => tt.AdditionalFields);
            modelBuilder.Entity<TopicType>().HasMany(tt => tt.Requests).WithOne(r => r.TopicType).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<AdditionalField>()
                            .Property(e => e.PossibleValues)
                            .HasConversion(v => string.Join(";", v), v => v.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<AdditionalFieldData>()
                            .Property(e => e.Data)
                            .HasConversion(v => string.Join(";", v), v => v.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
        }

    }
}