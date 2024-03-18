using Otus.Teaching.PromoCodeFactory.WebHost.Models.Role;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Employee;

public record CreateEmployeeModel
{
    public string? FirstName { get; set; }
        
    public string? LastName { get; set; }

    public string? Email { get; set; }

    public List<Core.Domain.Administration.Role>? Roles { get; set; }
}