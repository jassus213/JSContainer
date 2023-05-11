namespace JSInjector.Tests.Common
{
    public interface IJsonTestWriter<in TEntity> where TEntity : class
    {
        void WriteTestInfo(TEntity entity);
    }
}