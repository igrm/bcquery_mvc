using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bcquery_mvc
{
    [Table("TranslationItem", Schema = "Translation")]
    public class TranslationItem
    {
        [Key]
        public int TranslationItemID { get; set; }

        [Required]
        [StringLength(255)]
        public string TranslationItemCode { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}