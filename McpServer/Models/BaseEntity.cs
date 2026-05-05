namespace McpServer.Models;

public abstract class BaseEntity<T> where T : notnull
{
    public T Id { get; set; }

    protected BaseEntity(T id)
    {
        Id = id;
    }
}