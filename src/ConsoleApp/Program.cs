using System.Globalization;
using System.Text.Json;
using AStar.Infrastructure.Data;
using AStar.Infrastructure.Models;
using CsvHelper;
using CsvHelper.Configuration;

Console.WriteLine("Hello, World!");
// CreateNewFileList();

// await LoadFiles();

static async Task LoadFiles()
{
    var connection = new ConnectionString(){ Value= "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FilesDb"};
    var filesContext= new FilesContext(connection, new());

    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        NewLine = Environment.NewLine,
        IgnoreReferences = true,
    };

    IEnumerable<FileDetail> files = [];
    IEnumerable<FileAccessDetail> fileAccessDetails = [];
    using(var reader = new StreamReader("c:\\temp\\Files.csv"))
    using(var csv = new CsvReader(reader, config))
    {
        files = csv.GetRecords<FileDetail>().ToList();
    }

    using(var reader = new StreamReader("c:\\temp\\FileAccessDetails.csv"))
    using(var csv = new CsvReader(reader, config))
    {
        fileAccessDetails = csv.GetRecords<FileAccessDetail>().ToList();
    }

    var count = 0;
    foreach(var item in files.Take(10))
    {
        var fileAccessDetail = fileAccessDetails.Single(f=>f.Id == item.Id);
        item.FileAccessDetail = fileAccessDetail;

        _ = await filesContext.Files.AddAsync(item);
        count++;
        if(count != 10_000)
        {
            Console.WriteLine(item.Id);
            count = 0;
            _ = await filesContext.SaveChangesAsync();
        }
    }

    _ = await filesContext.SaveChangesAsync();
}

static void CreateNewFileList()
{
    var files = Directory.GetFiles(@"c:\temp2", "*.*",  SearchOption.AllDirectories);
    var filesToSave = new List<FileDetail>();
    foreach(var item in files)
    {
        var fileInfo = new FileInfo(item);
        var fileDetail = new FileDetail(fileInfo);
        var id = Guid.NewGuid();
        fileDetail.Id = id;
        fileDetail.FileAccessDetail.Id = id;
        filesToSave.Add(fileDetail);
    }

    File.WriteAllText(@"c:\temp\updated.files.json", JsonSerializer.Serialize(filesToSave));
}
