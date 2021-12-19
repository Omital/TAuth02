using TAuth02.EntityFramework;
using EntityFramework.DynamicFilters;

namespace TAuth02.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly TAuth02DbContext _context;

        public InitialHostDbBuilder(TAuth02DbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
