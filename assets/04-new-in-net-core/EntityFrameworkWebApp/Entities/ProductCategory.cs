using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkWebApp.Entities
{
    [Table("ProductCategory", Schema = "SalesLT")]
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            InverseParentProductCategory = new HashSet<ProductCategory>();
            Product = new HashSet<Product>();
        }

        [Column("ProductCategoryID")]
        public int ProductCategoryId { get; set; }
        [Column("ParentProductCategoryID")]
        public int? ParentProductCategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("ParentProductCategoryId")]
        [InverseProperty("InverseParentProductCategory")]
        public virtual ProductCategory ParentProductCategory { get; set; }
        [InverseProperty("ParentProductCategory")]
        public virtual ICollection<ProductCategory> InverseParentProductCategory { get; set; }
        [InverseProperty("ProductCategory")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
