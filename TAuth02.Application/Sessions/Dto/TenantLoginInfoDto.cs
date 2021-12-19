using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using TAuth02.MultiTenancy;

namespace TAuth02.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}