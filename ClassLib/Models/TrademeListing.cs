using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetCoreTrademeStats.ClassLib.Models {
	public abstract class TrademeListing {
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
		public virtual int Id { get; set; }
	}
}