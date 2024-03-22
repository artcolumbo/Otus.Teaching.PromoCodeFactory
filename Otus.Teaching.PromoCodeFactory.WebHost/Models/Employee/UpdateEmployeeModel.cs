namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Employee;

public class UpdateEmployeeModel
{
    public Guid Id { get; set; }
    
    public string? FirstName { get; set; }
        
    public string? LastName { get; set; }

    public string? Email { get; set; }
    
    public int AppliedPromoCodesCount { get; set; }

    public List<Core.Domain.Administration.Role>? Roles { get; set; }
}