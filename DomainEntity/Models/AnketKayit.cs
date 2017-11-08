using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Models
{[Table("tblSoru")]   //db do olusacak tablonun adı
    public class Soru
    {   [Key]
        public int SoruID { get; set; }
        [Required]//SoruCumlesizorunlu boş gecilemez
        public string SoruCumlesi { get; set; }

    }
    [Table("tblKisi")]
    public class Kisi
    {
        public int KisiID { get; set; }
        [Required]
        public string AdSoyad { get; set; }

    }
    [Table("tblCevap")]
    public  class Cevap
    {
        [Key]
        public int CevapID { get; set; }
        [ForeignKey("CevabıVerenKisi")]// bu fk nın dolduracagı virtual navigation property cevabverenkisidir.
        public int KisiID { get; set; }
        [ForeignKey("Sorusu")]
        public int SoruID { get; set; }
        public Yanit Yanit { get; set; }

        public virtual Kisi CevabıVerenKisi { get; set; }
        public virtual Soru Sorusu { get; set; }
    }

    public enum Yanit
    { Hayir, Evet }
}
