using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Role
{
    public record GetRoleModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}