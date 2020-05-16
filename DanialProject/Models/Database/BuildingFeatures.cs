using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class BuildingFeatures
	{
		public DanialProject.Models.Database.Buildings Buildings
		{
			get;
			set;
		}

		[ForeignKey("Buildings")]
		public int? BuildingsID
		{
			get;
			set;
		}

		public DanialProject.Models.Database.Features Features
		{
			get;
			set;
		}

		[ForeignKey("Features")]
		public int? FeaturesID
		{
			get;
			set;
		}

		public int ID
		{
			get;
			set;
		}

		public BuildingFeatures()
		{
		}
	}
}