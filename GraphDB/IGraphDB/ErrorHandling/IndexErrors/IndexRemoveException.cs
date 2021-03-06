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
    public sealed class IndexRemoveException : AGraphDBIndexException
    {
        public String Name { get; private set; }
        public String Edition { get; private set; }


        public IndexRemoveException(String myName, String myEdition, string myInfo)
        {
            Name = myName;
            Edition = myEdition;
            _msg = String.Format("Could not remove the index with name {0} and edition {1}. {2}", Name, Edition, myInfo);
        }
    }
}

