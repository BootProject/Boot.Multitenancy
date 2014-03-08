using FluentNHibernate.Automapping;
using System;

namespace Boot.Multitenancy.Filters.Extensions
{
    /// <summary>
    /// Extension to ignore sertain attributes.
    /// </summary>
    public static class FluentExtensions
    {
        /// <summary>
        /// Ignore a single property.
        /// Property marked with this attributes will not allow the Propery to be persisted to table.
        /// </summary>
        /// <param name="p">IPropertyIgnorer</param>
        /// <param name="propertyType">The type to ignore.</param>
        /// <returns>The property to ignore.</returns>
        public static IPropertyIgnorer SkipProperty(this IPropertyIgnorer p, Type propertyType)
        {
            return p.IgnoreProperties(x => x.MemberInfo.GetCustomAttributes(propertyType, false).Length > 0);
        }

        /// <summary>
        /// Ignore a single property.
        /// Property marked with this attributes will not allow the class to be persisted to table.
        /// </summary>
        /// <param name="p">IPropertyIgnorer</param>
        /// <param name="propertyType">The type to ignore.</param>
        /// <returns>The class to ignore.</returns>
        public static IPropertyIgnorer SkipEntity(this IPropertyIgnorer p, Type propertyType)
        {
            return p.IgnoreProperties(x => x.MemberInfo.GetCustomAttributes(propertyType, false).Length > 0);
        }
    }

}
