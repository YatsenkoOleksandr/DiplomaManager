﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DiplomaManager.DAL.EF;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.DAL.Interfaces;

namespace DiplomaManager.DAL.Repositories
{
    public class UserRepository<T> : IRepository<T>
        where T: User
    {
        private readonly DiplomaManagerContext _db;

        public UserRepository(DiplomaManagerContext context)
        {
            _db = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>()
                        .Include(u => u.FirstNames)
                        .Include(u => u.LastNames)
                        .Include(u => u.Patronymics);
        }

        public T Get(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _db.Set<T>()
                        .Include(u => u.FirstNames)
                        .Include(u => u.LastNames)
                        .Include(u => u.Patronymics)
                        .Where(predicate);
        }

        public void Create(T item)
        {
            _db.Set<T>().Add(item);
        }

        public void Update(T item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public bool IsEmpty()
        {
            return !_db.Set<T>().Any();
        }

        public void Delete(int id)
        {
            var u = _db.Set<T>().Find(id);
            if (u != null)
                _db.Set<T>().Remove(u);
        }
    }
}