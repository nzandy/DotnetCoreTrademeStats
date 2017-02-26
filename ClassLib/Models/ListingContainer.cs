using System.Collections.Generic;

namespace DotnetCoreTrademeStats.Models {
	public class ListingContainer<T> where T : TrademeListing {
		public int TotalCount { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
		public IEnumerable<T> List { get; set; }
	}
}