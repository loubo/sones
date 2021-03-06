/*
* sones GraphDB - Community Edition - http://www.sones.com
* Copyright (C) 2007-2011 sones GmbH
*
* This file is part of sones GraphDB Community Edition.
*
* sones GraphDB is free software: you can redistribute it and/or modify
* it under the terms of the GNU Affero General Public License as published by
* the Free Software Foundation, version 3 of the License.
* 
* sones GraphDB is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU Affero General Public License for more details.
*
* You should have received a copy of the GNU Affero General Public License
* along with sones GraphDB. If not, see <http://www.gnu.org/licenses/>.
* 
*/

using System;
using System.Collections.Generic;

namespace sones.GraphDB.TypeSystem
{
    public interface IBaseType: IEquatable<IBaseType>
    {
        /// <summary>
        /// The ID of the type.
        /// </summary>
        Int64 ID { get; }

        /// <summary>
        /// The name of the type.
        /// </summary>
        /// <remarks>
        /// The name must be unique for all types in one database.
        /// </remarks>
        String Name { get; }

        /// <summary>
        /// The behaviour for this type.
        /// </summary>
        /// <remarks>
        /// If no behaviour is defined, this property is <c>NULL</c>.
        /// </remarks>
        IBehaviour Behaviour { get; }

        /// <summary>
        /// The comment for this type.
        /// </summary>
        /// <value>A user defined string, never <c>NULL</c>.</value>
        String Comment { get; }

        /// <summary>
        /// Defines whether this type is abstract. 
        /// </summary>
        /// <value>
        /// If true, this type can not have vertices.
        /// </value>
        Boolean IsAbstract { get; }

        /// <summary>
        /// Defines whether this type was not created by the system itself.
        /// </summary>
        Boolean IsUserDefined { get; }

        #region Inheritance 
        
        /// <summary>
        /// Defines whether this type can be used as parent type.
        /// </summary>
        /// <value>
        /// If true, this vertex type must not be used as a parent vertex type.
        /// </value>
        Boolean IsSealed { get; }

        /// <summary>
        /// Has this type a parent type?
        /// </summary>
        /// <returns>True, if this type has a parent type, otherwise false.</returns>
        bool HasParentType { get; }

        /// <summary>
        /// Has this type child types?
        /// </summary>
        /// <returns>False, if this type has no child types, otherwise true.</returns>
        bool HasChildTypes { get; }

        /// <summary>
        /// Returns if the given type is an ancestor of the current type.
        /// </summary>
        /// <param name="myOtherType">The given type.</param>
        /// <returns>True, if the given type is an ancestor of the current type, otherwise false.</returns>
        bool IsAncestor(IBaseType myOtherType);

        /// <summary>
        /// Returns if the given type is an ancestor of or the current itself.
        /// </summary>
        /// <param name="myOtherType">The given type.</param>
        /// <returns>True, if the given type is an ancestor of the current type or the current type itself, otherwise false.</returns>
        bool IsAncestorOrSelf(IBaseType myOtherType);

        /// <summary>
        /// Returns if the given type is a descendant of the current type.
        /// </summary>
        /// <param name="myOtherType">The given type.</param>
        /// <returns>True, if the given type is a descendant of the current type, otherwise false.</returns>
        bool IsDescendant(IBaseType myOtherType);

        /// <summary>
        /// Returns if the given type is a descendant of or the current type itself.
        /// </summary>
        /// <param name="myOtherType">The given type.</param>
        /// <returns>True, if the given type is a descendant of the current type or the current type itself, otherwise false.</returns>
        bool IsDescendantOrSelf(IBaseType myOtherType);

        #endregion

        #region Inheritance

        /// <summary>
        /// Returns the descendantr of this IBaseType.
        /// </summary>
        /// <returns>An enumeration of IBaseTypes that are descendant of this IBaseType.</returns>
        IEnumerable<IBaseType> GetDescendantTypes();

        /// <summary>
        /// Returns the descendantr of this IBaseType and this IBaseType in one enumeration.
        /// </summary>
        /// <returns>An enumeration of IBaseTypes that are descendant of this IBaseType and this IBaseType itself.</returns>
        IEnumerable<IBaseType> GetDescendantTypesAndSelf();

        /// <summary>
        /// Returns the ancestor of this IBaseType.
        /// </summary>
        /// <returns>An enumeration of IBaseTypes that are ancestors of this IBaseType.</returns>
        IEnumerable<IBaseType> GetAncestorTypes();

        /// <summary>
        /// Returns the ancestor of this IBaseType and this IBaseType in one enumeration.
        /// </summary>
        /// <returns>An enumeration of IBaseTypes that are ancestors of this IBaseType and this IBaseType itself.</returns>
        IEnumerable<IBaseType> GetAncestorTypesAndSelf();

        /// <summary>
        /// Returns all descendant and ancestors of this IBaseType.
        /// </summary>
        /// <returns>An enumeration of all IBaseType that are ancestors or descendant of this IBaseType.</returns>
        IEnumerable<IBaseType> GetKinsmenTypes();

        /// <summary>
        /// Returns all descendant and ancestors of this IBaseType and this IBaseType in one enumeration. 
        /// </summary>
        /// <returns>An enumeration of all IBaseType that are ancestors or descendant of this IBaseType and this IBaseType itself.</returns>
        IEnumerable<IBaseType> GetKinsmenTypesAndSelf();

        /// <summary>
        /// Returns the direct children of this IBaseType.
        /// </summary>
        /// <returns>An enumeration of all direct children of this IBaseType.</returns>
        IEnumerable<IBaseType> ChildrenTypes { get; }

        /// <summary>
        /// Gets the parent of this IBaseType.
        /// </summary>
        IBaseType ParentType { get; }

        #endregion

        #region Attributes

        /// <summary>
        /// Has this vertex type a certain attribute?
        /// </summary>
        /// <returns>True or false</returns>
        bool HasAttribute(String myAttributeName);

        /// <summary>
        /// Gets a certain attribute definition
        /// </summary>
        /// <param name="myAttributeName">The name of the interesting attribute</param>
        /// <returns>A attribute definition</returns>
        IAttributeDefinition GetAttributeDefinition(String myAttributeName);

        /// <summary>
        /// Gets a certain attribute definition
        /// </summary>
        /// <param name="myAttributeID">The id of the interesting attribute</param>
        /// <returns>A attribute definition</returns>
        IAttributeDefinition GetAttributeDefinition(Int64 myAttributeID);

        /// <summary>
        /// Has this vertex type any attributes?
        /// </summary>
        /// <returns>True or false</returns>
        bool HasAttributes(bool myIncludeAncestorDefinitions);

        /// <summary>
        /// Gets all attributes defined on this vertex type.
        /// </summary>
        /// <param name="myIncludeParents">Include the properties of the parent vertex type(s)</param>
        /// <returns>An enumerable of attribute definitions</returns>
        IEnumerable<IAttributeDefinition> GetAttributeDefinitions(bool myIncludeAncestorDefinitions);

        #endregion

        #region Properties

        /// <summary>
        /// Has this vertex type a certain property?
        /// </summary>
        /// <returns>True or false</returns>
        bool HasProperty(String myAttributeName);

        /// <summary>
        /// Gets a certain attribute definition
        /// </summary>
        /// <param name="myPropertyName">The name of the property</param>
        /// <returns>A property definition</returns>
        IPropertyDefinition GetPropertyDefinition(String myPropertyName);

        /// <summary>
        /// Gets a certain attribute definition
        /// </summary>
        /// <param name="myPropertyID">The id of the property</param>
        /// <returns>A property definition</returns>
        IPropertyDefinition GetPropertyDefinition(Int64 myPropertyID);

        /// <summary>
        /// Has this vertex type any properties?
        /// </summary>
        /// <returns>True or false</returns>
        bool HasProperties(bool myIncludeAncestorDefinitions);

        /// <summary>
        /// Gets all properties defined on this vertex type.
        /// </summary>
        /// <param name="myIncludeParents">Include the properties of the parent vertex type(s)</param>
        /// <returns>An enumerable of property definitions</returns>
        IEnumerable<IPropertyDefinition> GetPropertyDefinitions(bool myIncludeAncestorDefinitions);

        /// <summary>
        /// Gets the properties with the given name.
        /// </summary>
        /// <param name="myPropertyNames">A list of peroperty names.</param>
        /// <returns>An enumerable of property definitions</returns>
        IEnumerable<IPropertyDefinition> GetPropertyDefinitions(IEnumerable<string> myPropertyNames);

        #endregion


    }
}
