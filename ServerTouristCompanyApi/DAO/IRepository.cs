using System.Collections.Generic;

namespace ServerTouristCompanyApi.DAO
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
    }
}
