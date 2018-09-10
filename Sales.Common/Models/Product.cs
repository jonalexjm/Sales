

namespace Sales.Common.Models

{// se tiene que habilitar las migraciones automaticas

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]//only you can to write 50 characters
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]

        public string Remarks { get; set; }

        [Display (Name = "Image")]

        public string ImagePath { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]

        public Decimal Price { get; set; }

        [Display(Name = "Is Available")]

        public Boolean IsAvailable { get; set; }

        [Display(Name = "Publish On")]
        [DataType(DataType.Date)]

        public DateTime PublishOn { get; set; }

        [NotMapped]
        public byte[] ImageArray { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "NoImage";
                }

                return $"https://salesapijonalexjm.azurewebsites.net/{this.ImagePath.Substring(1)}";
            }
           
           
        }

    

        public override string ToString()
        {
            return this.Description;
        }


    }
}
