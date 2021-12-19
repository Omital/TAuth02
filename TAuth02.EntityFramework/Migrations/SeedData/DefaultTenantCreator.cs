using System.Linq;
using TAuth02.EntityFramework;
using TAuth02.MultiTenancy;

namespace TAuth02.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly TAuth02DbContext _context;

        public DefaultTenantCreator(TAuth02DbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
