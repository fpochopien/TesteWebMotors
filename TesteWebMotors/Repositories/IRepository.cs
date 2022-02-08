namespace TesteWebMotors.Repositories
{
    public interface IRepository<T>
    {
        int Create(T t);
        List<T> Read(int id);
        int Update(T t);
        int Delete(int id);
    }
}