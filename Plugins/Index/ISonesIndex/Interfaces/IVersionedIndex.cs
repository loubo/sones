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

namespace sones.Plugins.Index.Interfaces
{
    /// <summary>
    /// Root interface for all versioned indices.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TVersion"></typeparam>
    public interface IVersionedIndex<TKey, TValue, TVersion> :
        IIndex<TKey, TValue>
        where TKey : IComparable
        where TVersion : IComparable
    {
        #region Keys

        /// <summary>
        /// Returns all keys of a given version
        /// </summary>
        /// <param name="myVersion">the version</param>
        /// <returns>all keys of the given version</returns>
        ISet<TKey> Keys(TVersion myVersion);

        #endregion

        #region Contains

        /// <summary>
        /// Checks if a key exists in the index
        /// </summary>
        /// <param name="myKey">the key to be checked</param>
        /// <param name="myVersion">the version of the index</param>
        /// <returns>true if the key exists, else false</returns>
        bool ContainsKey(TKey myKey, TVersion myVersion);

        /// <summary>
        /// Checks if a value exists in the index
        /// </summary>
        /// <param name="myValue">the value to be checked</param>
        /// <param name="myVersion">the version of the index</param>
        /// <returns>true if the value exists, else false</returns>
        bool ContainsValue(TValue myValue, TVersion myVersion);

        /// <summary>
        /// Checks if a key and an associated value exists in the index
        /// </summary>
        /// <param name="myKey">the key</param>
        /// <param name="myVersion">the version of the index</param>
        /// <param name="myValue">the associated value</param>
        /// <returns>true if the key and the associated balue exist, else false</returns>
        bool Contains(TKey myKey, TValue myValue, TVersion myVersion);

        #endregion

        #region Remove

        /// <summary>
        /// Removes a given key and its associated value from the index
        /// </summary>
        /// <param name="myKey">the key to be removed</param>
        /// <param name="myVersion">the version of the index</param>
        /// <returns>true if the key has been removed, else false</returns>
        bool Remove(TKey myKey, TVersion myVersion);

        #endregion

        #region Version Counts

        /// <summary>
        /// The total number of versions currently existing in the index
        /// </summary>
        /// <returns>total number of versions of that index</returns>
        Int64 HistoryCount();

        /// <summary>
        /// Returns the number of versions of the given key
        /// </summary>
        /// <param name="myKey">the key to count the versions</param>
        /// <returns>number of versions of that key</returns>
        Int64 VersionCount(TKey myKey);

        #endregion

        #region Clear History

        /// <summary>
        /// Clears all history information
        /// </summary>
        void ClearHistory();

        /// <summary>
        /// Clears the history of a given key
        /// </summary>
        /// <param name="myKey">the key</param>
        void ClearHistory(TKey myKey);

        #endregion
    }
}