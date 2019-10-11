using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkWebApp.Entities
{
    [Table("ProductModel", Schema = "SalesLT")]
    public partial class ProductModel
    {
        public ProductModel()
        {
            Product = new HashSet<Product>();
            ProductModelProductDescription = new HashSet<ProductModelProductDescription>();
        }

        [Column("ProductModelID")]
        public int ProductModelId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "xml")]
        public string CatalogDescription { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("ProductModel")]
        public virtual ICollection<Product> Product { get; set; }
        [InverseProperty("ProductModel")]
        public virtual ICollection<ProductModelProductDescription> ProductModelProductDescription { get; set; }
    }
}
