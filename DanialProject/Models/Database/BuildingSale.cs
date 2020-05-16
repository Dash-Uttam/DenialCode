using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class BuildingSale
	{
		[MaxLength(1000)]
		public string Access_Notes
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Building_Access
		{
			get;
			set;
		}

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

		[MaxLength(50)]
		public string Commision
		{
			get;
			set;
		}

		public int Fright_Elevators
		{
			get;
			set;
		}

		public int ID
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Maintenance
		{
			get;
			set;
		}

		public int Passenger_Elevators
		{
			get;
			set;
		}

		public double PriceSqFt
		{
			get;
			set;
		}

		public bool Private
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string SqFt
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Tax
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Unit_Access
		{
			get;
			set;
		}

		public BuildingSale()
		{
		}
	}
}