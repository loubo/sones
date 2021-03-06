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

namespace sones.GraphDB.ErrorHandling
{
    /// <summary>
    /// The vertex type is invalid
    /// </summary>
    public sealed class InvalidVertexTypeException : AGraphDBVertexTypeException
    {
        public String InvalidVertexType { get; private set; }
        public String Info { get; private set; }

        /// <summary>
        /// Creates a new InvalidVertexTypeException exception
        /// </summary>
        /// <param name="myInvalidVertexType">The name of the invalid vertex type</param>
        /// <param name="myInfo"></param>
        public InvalidVertexTypeException(String myInvalidVertexType, String myInfo) : base()
        {
            Info = myInfo;
            InvalidVertexType = myInvalidVertexType;
            _msg = String.Format("The type {0} is not valid. {1}.", InvalidVertexType, Info);
        }

    }
}
