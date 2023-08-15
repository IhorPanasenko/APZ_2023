using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class AgentRaitingViewModel
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid AgentId { get; set; }

        [Required]
        [Range(0, 10)]
        public double SingleRaiting { get; set; }
    }
}
