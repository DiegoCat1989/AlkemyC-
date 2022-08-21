using DisneyAlk.Abstractions;
using DisneyAlk.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyAlk.DataAccess
{
    public class AppDBContext:IdentityDbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        //public DbSet<CharacterMovie> CharacterMovie { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext>options):base(options) {
        
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder) {

            modelbuilder.Ignore<Entity>();
           
            base.OnModelCreating(modelbuilder);
        
        }
    }
}
