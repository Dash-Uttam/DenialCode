using System;
using System.Runtime.CompilerServices;

namespace DanialProject.Models.Database
{
	public class FilterFeatures
	{
		public int[] Array
		{
			get;
			set;
		}

		public DateTime AvailibilityDate
		{
			get;
			set;
		}

		public string Brough
		{
			get;
			set;
		}

		public bool CasebyCase
		{
			get;
			set;
		}

		public int FrieghtElevators
		{
			get;
			set;
		}

		public string Guarantors
		{
			get;
			set;
		}

		public string[] Neighborhood
		{
			get;
			set;
		}

		public bool NoFee
		{
			get;
			set;
		}

		public int PassengerElevators
		{
			get;
			set;
		}

		public bool Under30lbs
		{
			get;
			set;
		}

		public FilterFeatures()
		{
		}
	}
}