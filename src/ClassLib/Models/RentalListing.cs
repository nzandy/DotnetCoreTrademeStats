﻿using System;
using System.Text.RegularExpressions;
using DotnetCoreTrademeStats.ClassLib.Attributes;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace DotnetCoreTrademeStats.ClassLib.Models {

	public class Agency {
		
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsRealEstateAgency { get; set; }
	}

	[TrademeListing("v1/Search/Property/Rental.json?")]
	public class RentalListing : TrademeListing{

		[JsonProperty("ListingId")]
		[Column("ListingId")]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
		public override int Id {get; set;}		
		public string Title { get; set; }

		public int RegionId { get; set; }
		public string Region { get; set; }
		public int SuburbId { get; set; }
		public string Suburb { get; set; }
		public int DistrictId { get; set; }
		public string District { get; set; }

		// Property Details
		public int RentPerWeek { get; set; }
		public int Bedrooms { get; set; }
		public int Bathrooms { get; set; }		
		public Agency Agency { get; set; }

		private string _startDate;
		public string StartDate {
			get { return _startDate; }
			set {
				_startDate = value;
				StartDateConverted = GetDateTimeFromUnixEpoch(GetUnixEpochFromTrademeDate(value)); }
		}

		public DateTime? StartDateConverted { get; private set; }

		public double GetUnixEpochFromTrademeDate(string trademeDate) {
			string insideQuotes = Regex.Match(trademeDate, @"\(([^)]*)\)").Groups[1].Value;
			return Double.Parse(insideQuotes);
		}

		public DateTime? GetDateTimeFromUnixEpoch(double unixEpoch) {
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return dateTime.AddMilliseconds(unixEpoch);
		}
	}
}
