using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;
using TAuth02.Authorization.Roles;

namespace TAuth02.Roles.Dto
{
    public class RoleDto : EntityDto<int>
    {
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [StringLength(Role.MaxDescriptionLength)]
        public string Description { get; set; }

        public bool IsStatic { get; set; }

        public List<string> GrantedPermissions { get; set; }
    }
}
