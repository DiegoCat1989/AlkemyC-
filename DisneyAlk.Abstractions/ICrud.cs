namespace DisneyAlk.Abstractions
{
    public interface ICrud<T>
    {
        T Save(T entity);
        IList<T> GetAll();
        T GetbyId(int id);
        void Delete(int id);
    }
}