﻿using CityManagerAPİ.Models;
using Microsoft.EntityFrameworkCore;

namespace CityManagerAPİ.Data;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options)
		: base(options) { }


    public DbSet<City> Cities { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CityImage> CityImages { get; set; }

}

