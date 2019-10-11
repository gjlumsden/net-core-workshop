using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkWebApp.Entities
{
    [Table("CustomerAddress", Schema = "SalesLT")]
    public partial class CustomerAddress
    {
        [Column("CustomerID")]
        public int CustomerId { get; set; }
        [Column("AddressID")]
        public int AddressId { get; set; }
        [Required]
        [StringLength(50)]
        public string AddressType { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("AddressId")]
        [InverseProperty("CustomerAddress")]
        public virtual Address Address { get; set; }
        [ForeignKey("CustomerId")]
        [InverseProperty("CustomerAddress")]
        public virtual Customer Customer { get; set; }
    }
}
