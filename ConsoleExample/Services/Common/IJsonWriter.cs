using ConsoleExample.Services.Employee.Entity;

namespace ConsoleExample.Services.Common
{
    public interface IJsonWriter<in TEntity> where TEntity : IJsonEntity
    {
        void WriteToFile(TEntity entity);
    }
}