using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bcquery_mvc
{
    [Table("TranslationText", Schema = "Translation")]
    public class TranslationText
    {
        [Key]
        public int TranslationTextID { get; set; }

        [Required]
        public string TranslationTextValue { get; set; }

        [ForeignKey("Language")]
        public int LanguageID { get; set; }
        public virtual Language Language { get; set; }

        [ForeignKey("TranslationItem")]
        public int TranslationItemID { get; set; }
        public virtual TranslationItem TranslationItem { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}