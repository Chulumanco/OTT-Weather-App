using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Entities;

namespace WeatherApp.Helper
{
    public class WeatherDbContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
       : base(options)
        {
        }
    }
}