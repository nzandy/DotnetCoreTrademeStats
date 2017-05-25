using System.ComponentModel.DataAnnotations;

namespace DotnetCoreTrademeStats.ClassLib.Models {
	public abstract class TrademeListing {
		[Key]
		public virtual int Id { get; set; }
	}
}