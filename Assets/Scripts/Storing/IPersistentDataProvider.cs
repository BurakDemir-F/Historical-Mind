namespace Storing
{
    public interface IPersistentDataProvider<T>
    {
        void Save(T data);
        T Load();
    }
}