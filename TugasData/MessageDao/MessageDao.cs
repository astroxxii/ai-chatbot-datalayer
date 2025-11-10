using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TugasData.MessageDao
{
    public class Messages
    {
        public Guid id { get; set; }

        public List<MessageDetail> messages { get; set; } = null!;
    }

    public class MessageDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string role { get; set; } = null!;

        public string content { get; set; } = null!;
    }
}
