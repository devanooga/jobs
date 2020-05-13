namespace Devanooga.Jobs.Data.Entity
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Devanooga.Jobs.Data.Entity.Models;
    using Microsoft.EntityFrameworkCore;

    public class JobContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }

        public JobContext(DbContextOptions<JobContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var types = typeof(JobContext).GetTypeInfo()
                            .Assembly
                            .GetTypes()
                            .Where(t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract
                                                                && t.GetInterfaces().Any(i => i.GetTypeInfo().IsGenericType &&
                                                                                              i.GetGenericTypeDefinition() ==
                                                                                              typeof(IEntityTypeConfiguration<>)))
                            .ToList();
            foreach (var type in types)
            {
                dynamic model = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(model);
            }
        }
    }
}