using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class AdminContext : DbContext
	{
		public DbSet<DanialProject.Models.Database.Admins> Admins
		{
			get;
			set;
		}

		public DbSet<DanialProject.Models.Database.Agents> Agents
		{
			get;
			set;
		}

		public DbSet<DanialProject.Models.Database.BuildingFeatures> BuildingFeatures
		{
			get;
			set;
		}

		public DbSet<BuildingRent> BuildingRents
		{
			get;
			set;
		}

		public DbSet<DanialProject.Models.Database.Buildings> Buildings
		{
			get;
			set;
		}

		public DbSet<BuildingSale> BuildingSales
		{
			get;
			set;
		}

		public DbSet<DanialProject.Models.Database.Features> Features
		{
			get;
			set;
		}

		public DbSet<DanialProject.Models.Database.Images> Images
		{
			get;
			set;
		}

		public DbSet<DanialProject.Models.Database.LevelAccess> LevelAccess
		{
			get;
			set;
		}

		public DbSet<DanialProject.Models.Database.OpenDates> OpenDates
		{
			get;
			set;
		}

		public DbSet<SalesAgent> SalesAgents
		{
			get;
			set;
		}

		public DbSet<DanialProject.Models.Database.SecurityLevel> SecurityLevel
		{
			get;
			set;
		}

		public DbSet<Transportation> Transportations
		{
			get;
			set;
		}

		public AdminContext() : base("name=AdminContext")
		{
			base.Configuration.ProxyCreationEnabled = false;
			base.Configuration.LazyLoadingEnabled = false;
			base.Configuration.ProxyCreationEnabled = false;
		}
	}
}