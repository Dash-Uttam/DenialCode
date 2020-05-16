using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class SalesAgent
	{
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

		public int ID
		{
			get;
			set;
		}

		public SalesAgent()
		{
		}
	}
}