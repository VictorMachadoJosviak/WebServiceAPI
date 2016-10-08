using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebServicesApi.Models
{
    [Table("Product")]
    public class Product
    {
        public Product()
        {
            this.AcquireDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public double Preco { get; set; }

        public DateTime AcquireDate { get; set; }

        public bool IsActive { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}