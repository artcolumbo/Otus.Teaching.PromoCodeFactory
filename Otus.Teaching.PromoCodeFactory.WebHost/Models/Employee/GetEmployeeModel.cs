using System;
using System.Collections.Generic;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models.Role;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Employee
{
    public record GetEmployeeModel : GetEmployeesModel
    {
        public List<GetRoleModel>? Roles { get; set; }

        public int AppliedPromoCodesCount { get; set; }
    }
}