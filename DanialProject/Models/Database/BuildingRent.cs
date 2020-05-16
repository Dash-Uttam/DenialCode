using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class BuildingRent
	{
		[MaxLength(1000)]
		public string Access_Notes
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Additional_Cost
		{
			get;
			set;
		}

		public DanialProject.Models.Database.Agents Agents
		{
			get;
			set;
		}

		[ForeignKey("Agents")]
		public int? AgentsID
		{
			get;
			set;
		}

		public DateTime AvalibilityDate
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

		public bool CasebyCase
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Exclusivity
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Fee
		{
			get;
			set;
		}

		public int Fright_Elevators
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Guarantors
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
		public string Incentives
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string Landlord
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Lease_term
		{
			get;
			set;
		}

		[NotMapped]
		public DanialProject.Models.Database.Agents ListOfAgents
		{
			get;
			set;
		}

		[MaxLength(500)]
		public string Move_In_Cost
		{
			get;
			set;
		}

		public bool No_Fee
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string OP
		{
			get;
			set;
		}

		public int Passenger_Elevators
		{
			get;
			set;
		}

		public bool Room
		{
			get;
			set;
		}

		[NotMapped]
		public string[] SalesAgents
		{
			get;
			set;
		}

		public bool Under30lbs
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

		public BuildingRent()
		{
		}
	}
}