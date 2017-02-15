using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bcquery_mvc
{
    [Table("Language", Schema ="Translation")]
    public class Language
    {
        [Key]
        public int LanguageID { get; set; }

        [Required]
        [StringLength(3,MinimumLength =3)]
        public string LanguageCode { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}