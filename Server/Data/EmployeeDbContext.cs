﻿using BlazorCRUDSingalR.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCRUDSingalR.Server.Data
{
    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            :base(options)
        {

        }

        public DbSet<EmployeeInfo> EmployeeInfos { get; set; }
    }
}
