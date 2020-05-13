namespace Devanooga.Jobs.Data.Entity.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Job : IEntityTypeConfiguration<Job>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public void Configure(EntityTypeBuilder<Job> builder)
        {
        }
    }
}