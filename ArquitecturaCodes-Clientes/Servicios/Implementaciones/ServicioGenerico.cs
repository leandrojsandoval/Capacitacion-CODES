﻿using Microsoft.EntityFrameworkCore.Storage;
using ARQ.Datos.Interfaces;
using ARQ.Entidades;
using ARQ.Servicios.Interfaces;
using ARQ.Servicios.RelationshipValidators;
using Servicios.Implementaciones;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ARQ.Servicios.Implementaciones
{
    public class ServicioGenerico : ServicioBase<IDatosGenerico>, IServicioGenerico
    {
        public ServicioGenerico(IDatosGenerico datos) : base(datos) { }

        public void Add<T>(T p_Entity, bool saveChanges = true) where T : class
        {
            _datos.Add<T>(p_Entity,saveChanges);
        }

        public void AddAsync<T>(T p_Entity) where T : class
        {
            _datos.AddAsync<T>(p_Entity);
        }

        public void AddRange<T>(IEnumerable<T> p_Entity) where T : class
        {
            _datos.AddRange<T>(p_Entity);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _datos.BeginTransaction();
        }

        public void Delete<T>(T p_Entity) where T : class
        {
            _datos.Delete<T>(p_Entity);
        }

        public void Deactivate<T>(T p_Entity) where T : EntidadBase
        {
            var relationShipValidator = RelationshipValidatorFinder<T>.GetValidator(p_Entity);
            if (relationShipValidator != null){
                relationShipValidator.Validate(p_Entity,_datos);
            }
            _datos.Deactivate<T>(p_Entity);
        }

        public void Activate<T>(T p_Entity) where T : EntidadBase
        {
            _datos.Activate<T>(p_Entity);
        }

        public T Get<T>(Func<T, bool> predicate) where T : class
        {
            return _datos.Get<T>(predicate);
        }

        public T Get<T, W>(Func<T, bool> predicate, Expression<Func<T, W>> include)
            where T : class
            where W : class
        {
            return _datos.Get<T, W>(predicate, include);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _datos.GetAll<T>();
        }

        public IEnumerable<T> GetAll<T>(Func<T, bool> predicate) where T : class
        {
            return _datos.GetAll<T>(predicate);
        }

        public T GetById<T>(object id) where T : class
        {
            return _datos.GetById<T>(id);
        }

        public void Update<T>(T p_Entity, bool saveChanges = true) where T : EntidadBase
        {
            if (!p_Entity.Activo) { 
                var relationShipValidator = RelationshipValidatorFinder<T>.GetValidator(p_Entity);
                if (relationShipValidator != null)
                {
                    relationShipValidator.Validate(p_Entity, _datos);
                }
            }
            _datos.Update(p_Entity,saveChanges);
        }

        public void UpdateIgnoringProperty<T, W>(T entity, Expression<Func<T, W>> property)
            where T : class
            where W : struct
        {
            _datos.UpdateIgnoringProperty<T, W>(entity, property);
        }
        public void UpdateIgnoringPropertyList<T, W>(T entity, List<Expression<Func<T, W>>> propertyList)
            where T : class
            where W : struct
        {
            _datos.UpdateIgnoringPropertyList<T, W>(entity, propertyList);
        }

        public int Count<T>(Func<T, bool> predicate) where T : class
        {
            return _datos.Count<T>(predicate);
        }

        public void UpdateRange<T>(IEnumerable<T> p_Entity) where T : class
        {
            _datos.UpdateRange<T>(p_Entity);
        }

        public void SaveChanges()
        {
            _datos.SaveChanges();
        }
    }
}
