﻿using System;
using sones.Library.Internal.Security;
using sones.Library.Internal.Token;
using sones.GraphDB.Request;
using sones.GraphDB.Transaction;

namespace sones.GraphDB
{
    /// <summary>
    /// The CCC implemention of the graphdb interface
    /// </summary>
    public sealed class CubeConnectedCycles : IGraphDB
    {
        #region IGraphDB Members

        /// <summary>
        /// Creates a new type of vertex
        /// </summary>
        /// <typeparam name="TResult">The type of the result of this request</typeparam>
        /// <param name="mySessionToken">The current session token</param>
        /// <param name="myTransactionToken">The current transaction token (null, if there is no transaction)</param>
        /// <param name="myRequestCreateVertexType">The create vertex type request</param>
        /// <returns>A generic result</returns>
        public TResult CreateVertexType<TResult>(SessionToken mySessionToken, TransactionToken myTransactionToken, RequestCreateVertexType<TResult> myRequestCreateVertexType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears the graphdb entirely
        /// </summary>
        /// <param name="mySessionToken">The current session token</param>
        /// <param name="myTransactionToken">The current transaction token (null, if there is no transaction)</param>
        /// <param name="myRequestClear">The clear request
        /// <returns>A generic result</returns>
        public TResult Clear<TResult>(SessionToken mySessionToken, TransactionToken myTransactionToken, RequestClear<TResult> myRequestClear)
        {
            throw new NotImplementedException();
        }

        public TransactionToken BeginTransaction(SessionToken mySessionToken, bool myLongrunning = false, IsolationLevel myIsolationLevel = IsolationLevel.Serializable)
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction(SessionToken mySessionToken, TransactionToken myTransactionToken)
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction(SessionToken mySessionToken, TransactionToken myTransactionToken)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}