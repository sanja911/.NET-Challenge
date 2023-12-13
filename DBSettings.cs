namespace Projects.Database;

public interface DatabaseSettings:IDBSettings
{
    public string CollectionName { get; set; }
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }

}