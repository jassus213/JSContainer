using ConsoleExample.Entity;

namespace ConsoleExample.Services
{
    public interface IJsonWriter<in TEntity> where TEntity : IJsonEntity
    {
        void WriteToFile(TEntity entity);
    }
}