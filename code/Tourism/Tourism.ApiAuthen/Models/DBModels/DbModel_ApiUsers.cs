using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.ApiAuthen.Models.DBModels
{
    [Table("tb_ApiUsers")]
    public class DbModel_ApiUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(32)]
        public string AppKey { get; set; }
        [MaxLength(32)]
        public string AppName { get; set; }
        [MaxLength(32)]
        public  string AppSecrect { get; set; }
        [MaxLength(32)]
        public string Token { get; set; }
        [MaxLength(32)]
        public string Salt { get; set; }
        public string CreateTokenTime { get; set; }
    }
}
