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
using System.Linq;
using System.Text;

namespace sones.Plugins.SonesGQL.Aggregates.ErrorHandling
{
    public sealed class InvalidArithmeticAggregationException : ASonesQLAggregateException
    {
        public readonly Type AggregatedType;
        public readonly string Operation;

        public InvalidArithmeticAggregationException(Type myAggregatedType, string myOperation)
        {
            // TODO: Complete member initialization
            AggregatedType = myAggregatedType;
            Operation = myOperation;

            _msg = String.Format("It is not allowed to calculate a {0} aggregate on properties of type {1}.", Operation, AggregatedType);
        }
    }
}
