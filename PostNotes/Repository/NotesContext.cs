using Microsoft.EntityFrameworkCore;
using PostNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostNotes.Repository
{
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions<NotesContext> options) : base(options)
        {

        }

        public DbSet<Notes> Notes { get; set; }
    }
}
