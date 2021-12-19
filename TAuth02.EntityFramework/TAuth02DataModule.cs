using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using TAuth02.EntityFramework;

namespace TAuth02
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(TAuth02CoreModule))]
    public class TAuth02DataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TAuth02DbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
