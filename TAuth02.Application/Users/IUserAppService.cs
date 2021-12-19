using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TAuth02.Roles.Dto;
using TAuth02.Users.Dto;

namespace TAuth02.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}