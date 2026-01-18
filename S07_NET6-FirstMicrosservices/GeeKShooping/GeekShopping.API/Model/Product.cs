using GeeKShooping.Infra;
using GeeKShooping.Infra.Helps;
using GeekShopping.API.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.API.Model
{
    [Table("product")]
    public class Product: BaseEntity
    {
        [Column("name")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Column("price")]
        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; }

        [Column("category_name")]
        [StringLength(50)]
        public string CategoryName { get; set; }

        [Column("image_url")]
        [StringLength(300)]
        public string ImageURL { get; set; }
        public bool Validate()
        {
            Extensions.ValidateString(this.Name, "Nome deve ser preenchido.");
            Extensions.ValidateMaxStringLength(this.Name, 150, "Nome deve ter no máximo 150 caracteres.");
            Extensions.ValidateMinStringLength(this.Name, 4, "Nome deve ter no mínimo 5 caracteres.");
            Extensions.ValidateMaxStringLength(this.Description, 500, "Description deve ter no máximo 500 caracteres.");
            Extensions.ValidateMaxStringLength(this.CategoryName, 50, "Nome deve ter no máximo 50 caracteres.");
            Extensions.ValidateMaxStringLength(this.ImageURL, 300, "Url deve ter no máximo 300 caracteres.");
            Extensions.ValidateDecimal(this.Price, "Preço deve ser preenchido.");
            return Notification.IsValid();
        }
    }
}
