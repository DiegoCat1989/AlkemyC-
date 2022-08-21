using DisneyAlk.Abstractions;
using DisneyAlk.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection; 
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace DisneyAlk.DataAccess
{
    public class DBContext<T> : IDBContext<T> where T : class,IEntity
    {
        DbSet<T> _items;
        AppDBContext _ctx;
        public DBContext(AppDBContext ctx )
        {
            _ctx = ctx;
            _items = ctx.Set<T>();

            
            }

       




        public void Delete(int id)
        {

            var x = _items.Where(z=>z.Id.Equals(id)).FirstOrDefault();
            if (x != null) 
            { 
            _items.Remove(x);
            _ctx.SaveChanges();

            }
        }

        public IList<T> GetAll()
        {
            ////foreach (var navigationEntry in _ctx.Entry(_items).Navigations)
            ////{
            ////    navigationEntry.Load();
            ////};
            ///
            var x = _items.ToList();
            foreach (var item in x) {
                foreach (var navigationEntry in _ctx.Entry(item).Navigations) 
                {
                    navigationEntry.Load();
                };
            
            }

            return x;
           
        }

        public T GetbyId(int id)
        {

            var x = _items.Where(i => i.Id.Equals(id)).FirstOrDefault();

            if (x != null) 
            { 
                foreach (var navigationEntry in _ctx.Entry(x).Navigations) 
                {
                navigationEntry.Load();
                };
            };
            return x;
                
        }

        


        public T Save(T entity)
        {
            
            //var existing = _items.Find(entity.Id);
            var existing = this.GetbyId(entity.Id);
            if (existing == null)
            {

                _items.Add(entity);
              
                
            }
            else
            {
             
                

                foreach (var navObj in _ctx.Entry(entity).Navigations)
                {

                    

                    foreach (var navExist in _ctx.Entry(existing).Navigations)
                    {
                        if (navObj.Metadata.Name == navExist.Metadata.Name)
                            navExist.CurrentValue = navObj.CurrentValue;
                    }
                }



                _ctx.Entry(existing).CurrentValues.SetValues(entity);
                //_ctx.Update(entity);
                _ctx.Entry(existing).State = EntityState.Modified;

            }
            _ctx.SaveChanges();
            return entity;
        }
    }
}