using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class Buildings
	{
		[MaxLength(500)]
		public string Address
		{
			get;
			set;
		}

		[NotMapped]
		public string AgentName
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

		[NotMapped]
		public int[] Array
		{
			get;
			set;
		}

		public double Baths
		{
			get;
			set;
		}

		public double Beds
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Borough
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string BuildingStatus
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string BuildingType
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		[MaxLength(15)]
		public string DraftOrSave
		{
			get;
			set;
		}

		[NotMapped]
		public List<BuildingFeatures> Features
		{
			get;
			set;
		}

		public int ID
		{
			get;
			set;
		}

		[NotMapped]
		public string Image
		{
			get;
			set;
		}

		public double Latitude
		{
			get;
			set;
		}

		public double Longitude
		{
			get;
			set;
		}

		[MaxLength(100)]
		public string Name
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Neighbourhood
		{
			get;
			set;
		}

		[NotMapped]
		public bool No_Fee
		{
			get;
			set;
		}

		public string Notes
		{
			get;
			set;
		}

		[NotMapped]
		public BuildingRent OtherDetails
		{
			get;
			set;
		}

		public double Price
		{
			get;
			set;
		}

		public bool PrivateListing
		{
			get;
			set;
		}

		public string RejectNote
		{
			get;
			set;
		}

		[NotMapped]
		public BuildingSale SaleBuilding
		{
			get;
			set;
		}

		[MaxLength(15)]
		public string Status
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Type
		{
			get;
			set;
		}

		[MaxLength(50)]
		public string Unit
		{
			get;
			set;
		}

		public Buildings()
		{
		}
	}
}