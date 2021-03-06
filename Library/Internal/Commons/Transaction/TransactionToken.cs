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

namespace sones.Library.Commons.Transaction
{
    /// <summary>
    /// A class that contains informations concerning the current Transaction
    /// </summary>
    [Serializable]
    public sealed class TransactionToken
    {
        #region Data

        private TransactionID _iID;
        /// <summary>
        /// The ID of the current transaction token
        /// </summary>
        public TransactionID ID
        {
            set {}
            get {
                return _iID;
                }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new transaction token
        /// </summary>
        /// <param name="myTransactionID">The ID of the token</param>
        public TransactionToken(Int64 myTransactionID)
        {
            _iID = new TransactionID(myTransactionID);
        }

        #endregion

    }
}