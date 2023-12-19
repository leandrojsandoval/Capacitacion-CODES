﻿using Microsoft.EntityFrameworkCore.Storage;
using ARQ.Entidades;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ARQ.Servicios.Interfaces
{
    public interface IServicioGenerico : IServicioBase
    {
        T Get<T>(Func<T, bool> predicate) where T : class;
        T GetById<T>(object id) where T : class;
        T Get<T, W>(Func<T, bool> predicate, Expression<Func<T, W>> include) where T : class where W : class;
        IEnumerable<T> GetAll<T>() where T : class;
        IEnumerable<T> GetAll<T>(Func<T, bool> predicate) where T : class;
        void Add<T>(T p_Entity, bool saveChanges = true) where T : class;
        void AddRange<T>(IEnumerable<T> p_Entity) where T : class;
        void AddAsync<T>(T p_Entity) where T : class;
        void Update<T>(T p_Entity,bool saveChanges = true) where T : EntidadBase;
        void UpdateRange<T>(IEnumerable<T> p_Entity) where T : class;
        void Delete<T>(T p_Entity) where T : class;
        void Deactivate<T>(T p_Entity) where T : EntidadBase;
        void Activate<T>(T p_Entity) where T : EntidadBase;
        void UpdateIgnoringProperty<T, W>(T entity, Expression<Func<T, W>> property) where T : class where W : struct;
        void UpdateIgnoringPropertyList<T, W>(T entity, List<Expression<Func<T, W>>> propertyList) where T : class where W : struct;
        int Count<T>(Func<T, bool> predicate) where T : class;
        public void SaveChanges();
        IDbContextTransaction BeginTransaction();
    }
}
