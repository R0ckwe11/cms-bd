using System.Runtime.Caching;
using cms_bd.Data;
using cms_bd.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace cms_bd;

public class FileWatcher : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private MemoryCache _memCache;
    private CacheItemPolicy _cacheItemPolicy;
    private const int CacheTimeMilliseconds = 1000;

    public FileWatcher(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task Process()
    {
        _memCache = MemoryCache.Default;

        _cacheItemPolicy = new CacheItemPolicy()
        {
            RemovedCallback = OnRemovedFromCache
        };

        using var scope = _serviceScopeFactory.CreateScope();
        var watcher = new FileSystemWatcher();
        var folderName = Path.Combine("resources", "images");
        var pathToWatch = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        watcher.Path = pathToWatch;
        watcher.IncludeSubdirectories = false;
        watcher.NotifyFilter = NotifyFilters.FileName;
        watcher.Created += OnCreated;
        watcher.Deleted += OnDeleted;
        watcher.EnableRaisingEvents = true;
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        _cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddMilliseconds(CacheTimeMilliseconds);
        _memCache.AddOrGetExisting(e.Name, e, _cacheItemPolicy);
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        _cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddMilliseconds(CacheTimeMilliseconds);
        _memCache.AddOrGetExisting(e.Name, e, _cacheItemPolicy);

    }

    // Handle cache item expiring
    private void OnRemovedFromCache(CacheEntryRemovedArguments args)
    {
        if (args.RemovedReason != CacheEntryRemovedReason.Expired) return;

        // Now actually handle file event
        var e = (FileSystemEventArgs)args.CacheItem.Value;
        
        switch (e.ChangeType)
        {
            case WatcherChangeTypes.Created:
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                var metadata = dbContext.ImageMetadata.ToList();
        
                if (metadata.Any(t => t.FileName == e.Name) || !DbInitializer.IsAPhotoFile(e.Name)) return;
        
                dbContext.ImageMetadata.Update(new ImageMetadata { FileName = e.Name, CreatedBy = 1 });
                dbContext.SaveChanges();
                break;
            }
            case WatcherChangeTypes.Deleted:
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                var metadata = dbContext.ImageMetadata.ToList();
        
                if (metadata.All(t => t.FileName != e.Name) || !DbInitializer.IsAPhotoFile(e.Name)) return;
        
                var toDelete = dbContext.ImageMetadata.FirstOrDefault(t => t.FileName == e.Name);
                try
                {
                    dbContext.ImageMetadata.Remove(toDelete);
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException is SqlException) return;
                    throw;
                }
                
                break;
            }
            case WatcherChangeTypes.Changed:
                break;
            case WatcherChangeTypes.Renamed:
                break;
            case WatcherChangeTypes.All:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}