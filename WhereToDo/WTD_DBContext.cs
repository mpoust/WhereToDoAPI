////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
 * CIT368 - Secure Software Development and Testing - Spring 2019
 * 
 * FileName: WTD_DBContext.cs
 * Author: Michael Poust
		   mbp3@pct.edu
 * Description: Database context class
 * References: 
 *   
 * (c) Michael Poust, 2019
 */
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToDo.Entities;

namespace WhereToDo
{
    public class WTD_DBContext : IdentityDbContext<UserEntity, UserRoleEntity, int>
    {
        public WTD_DBContext(DbContextOptions options)
            : base(options) { }

        // Provides connection to intermediate DB table, linking userID and their listIDs
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User_ListEntity>().HasKey(t => new
            {
                t.UserId,
                t.ListId
            });
        }

        // List table
        public DbSet<ListEntity> List { get; set; }

        // User_List table - configured with above mapping
        public DbSet<User_ListEntity> User_List { get; set; }

        // Notes - Not currently implemented
        // TODO: Configure Notes properly
        public DbSet<NotesEntity> Notes { get; set; }
    }
}
