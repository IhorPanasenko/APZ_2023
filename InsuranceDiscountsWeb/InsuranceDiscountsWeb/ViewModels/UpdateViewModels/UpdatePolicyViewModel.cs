namespace InsuranceDiscountsWeb.ViewModels.UpdateViewModels
{
    public class UpdatePolicyViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double CoverageAmount { get; set; } = -1;

        public double Price { get; set; } = -1;

        public int TimePeriod { get; set; } = -1;

        public Guid? CompanyId { get; set; }

        public Guid? CategoryId { get; set; }
    }
}
