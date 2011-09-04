using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace SimProNo
{
    public class MockDbSet<T>: IDbSet<T> where T : class
    {
        private ObservableCollection<T> objects = new ObservableCollection<T>();

        public T Add(T entity)
        {
            objects.Add(entity);
            return entity;
        }
        
        public T Attach(T entity)
        {
            objects.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public System.Collections.ObjectModel.ObservableCollection<T> Local
        {
            get { return objects; }
        }

        public T Remove(T entity)
        {
            objects.Remove(entity);
            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return objects.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return objects.GetEnumerator();
        }

        public Type ElementType
        {
            get { return objects.AsQueryable<T>().ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return objects.AsQueryable<T>().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return objects.AsQueryable<T>().Provider; }
        }
    }
}