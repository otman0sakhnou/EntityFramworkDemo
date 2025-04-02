using EntityFrameWorkTp.Data;
using EntityFrameWorkTp.Infrastructure.Interfaces;
using EntityFrameWorkTp.Infrastructure.Repositories;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace EntityFrameWorkTp;

public class DependencyConfig
{
    public static Container Configure()
    {
        var container = new Container();
            
        // Set the default scoped lifestyle based on application type
        container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            
        // Register DbContext
        container.Register<AppDbContext>(Lifestyle.Scoped);
            
        // Register generic repositories
        container.Register(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Scoped);
        container.Register(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>), Lifestyle.Scoped);
            
        // Register Unit of Work
        container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            
        // Verify the container's configuration
        container.Verify();
            
        return container;
    }
}