using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AgentRaiting
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("AgentId")]
        public Guid AgentId { get; set; }

        public Agent? Agent { get; set; } = null;

        public double SingleRaiting { get; set; }
    }
}
