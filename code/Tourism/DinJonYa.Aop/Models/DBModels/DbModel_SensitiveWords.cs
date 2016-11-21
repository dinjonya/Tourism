using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinJonYa.Aop.Models.DBModels
{
    [Table("tb_SensitiveWords")]
    public class DbModel_SensitiveWords
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(32)]
        public string SensitiveWord { get; set; }

    }
}
