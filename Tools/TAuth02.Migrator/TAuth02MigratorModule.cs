using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using TAuth02.EntityFramework;

namespace TAuth02.Migrator
{
    [DependsOn(typeof(TAuth02DataModule))]
    public class TAuth02MigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<TAuth02DbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}