using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.ApiAuthen.Models.DBModels
{
    [Table("tb_apiUserIps")]
    public class DbModel_ApiUserIps
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(32)]
        public string AppKey { get; set; }
        [MaxLength(32)]
        public string AllowIp { get; set; }

    }
}
