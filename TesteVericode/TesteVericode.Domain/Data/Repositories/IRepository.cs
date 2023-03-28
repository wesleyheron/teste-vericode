namespace TesteVericode.Domain.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T Get(object id);
        void Add(T entity);
        void Update(T entity);
        void Delete(object id);
        int Count();
    }
}
