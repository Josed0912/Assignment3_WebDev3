using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Assignment3_Torres_JoseDavid.Models;

namespace Assignment3_Torres_JoseDavid.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Assignment3_Torres_JoseDavid.Models.ContactModel> ContactModel { get; set; }
    }
}
