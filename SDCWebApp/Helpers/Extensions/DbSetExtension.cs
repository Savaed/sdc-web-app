//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using SDCWebApp.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SDCWebApp.Helpers.Extensions
//{
//    /// <summary>
//    /// Extend DbSet functionality.
//    /// </summary>
//    public static class DbSetExtension
//    {
//        /// <summary>
//        /// Updates <typeparamref name="TEntity"/> entity with specified limitations.
//        /// </summary>
//        /// <typeparam name="TEntity">Type of entity to be updated.</typeparam>
//        /// <param name="dbSet">Source of <typeparamref name="TEntity"/> entities.</param>
//        /// <param name="entity">The entity to be updated.</param>
//        /// <returns>An <see cref="EntityEntry{TEntity}"/></returns>
//        public static EntityEntry CustomUpdate<TEntity>(this DbSet<TEntity> dbSet, TEntity entity) where TEntity : BasicEntity, new()
//        {            
//            model.Id = entity.Id;
//            var entry = dbSet.Attach(model as SightseeingTariff);
//            var objectProperties = entity.GetType().GetProperties();

//            foreach (var property in objectProperties)
//            {
//                if (!property.Name.Equals("Id") && !property.Name.Equals("CreatedAt"))
//                    property.SetValue(model, property.GetValue(entity));
//            }

//            return entry;
//        }

//    }  
//}
