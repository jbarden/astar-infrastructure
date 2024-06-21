using System.Text.Json;
using AStar.Infrastructure.Data;
using AStar.Infrastructure.Models;

namespace AStar.Infrastructure.Fixtures;

public class MockFilesContext : IDisposable
{
    private bool disposedValue;

    public MockFilesContext()
    {
        Context = new FilesContext(new(), new() { InMemory = true });

        _ = Context.Database.EnsureCreated();

        AddMockFiles(Context);
        _ = Context.SaveChanges();
    }

    public FilesContext Context { get; private set; }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if(!disposedValue)
        {
            if(disposing)
            {
                Context.Dispose();
            }

            disposedValue = true;
        }
    }

    private static void AddMockFiles(FilesContext mockFilesContext)
    {
        var filesAsJson = File.ReadAllText(@"TestFiles\files.json");

        var listFromJson = JsonSerializer.Deserialize<IEnumerable<FileDetail>>(filesAsJson)!;

        foreach(var item in listFromJson)
        {
            if(mockFilesContext.Files.FirstOrDefault(f => f.FileName == item.FileName && f.DirectoryName == item.DirectoryName) == null)
            {
                mockFilesContext.Files.Add(item);
                mockFilesContext.SaveChanges();
            }
        }
    }
}
