using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.TaskAPI
{
    public class PreAdviceDbContext:DbContext
    {
        public PreAdviceDbContext(DbContextOptions<PreAdviceDbContext> options):base(options){}

        public DbSet<PreAdvice> PreAdvices {get; set;}  

        protected override void OnModelCreating(ModelBuilder modelBuilder){

            modelBuilder.Entity<PreAdvice>().HasKey(p => p.preAdviceId);
            modelBuilder.Entity<PreAdvice>().Property(b => b.preAdviceId).ValueGeneratedOnAdd();
            modelBuilder.Entity<PreAdvice>().Property(b => b.depot).IsRequired();
            modelBuilder.Entity<PreAdvice>().Property(b => b.liner).IsRequired();

        }

        
    }
}