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
using sones.GraphDB.TypeSystem;
using ISonesGQLFunction.Structure;
using sones.GraphDB;
using sones.Library.Commons.Security;
using sones.Library.Commons.Transaction;
using sones.GraphDB.ErrorHandling;
using sones.Library.VersionedPluginManager;
using sones.Library.PropertyHyperGraph;
using sones.Library.LanguageExtensions;

namespace sones.Plugins.SonesGQL.Functions
{
    public sealed class ToUNIXDate : ABaseFunction, IPluginable
    {
        #region constructor

        public ToUNIXDate()
        { }

        #endregion

        public override string GetDescribeOutput()
        {
            return "Convert the datetime value to the unix datetime format.";
        }

        public override bool ValidateWorkingBase(Object myWorkingBase, IGraphDB myGraphDB, SecurityToken mySecurityToken, TransactionToken myTransactionToken)
        {
            if (myWorkingBase == null)
            {
                return false;
            }
            else
            {
                if (myWorkingBase == typeof(UInt64) || myWorkingBase == typeof(DateTime))
                {
                    return true;
                }
                else if (myWorkingBase is Type)
                {
                    return (((Type)myWorkingBase) == typeof(long)) || (((Type)myWorkingBase) == typeof(DateTime));
                }
                else if (myWorkingBase is IPropertyDefinition)
                {
                    return (((myWorkingBase as IPropertyDefinition).BaseType == typeof(long)) || (((myWorkingBase as IPropertyDefinition).BaseType == typeof(DateTime))));
                }
            }

            return false;
        }

        public override FuncParameter ExecFunc(IAttributeDefinition myAttributeDefinition, Object myCallingObject, IVertex myDBObject, IGraphDB myGraphDB, SecurityToken mySecurityToken, TransactionToken myTransactionToken, params FuncParameter[] myParams)
        {
            if (myCallingObject is UInt64)
            {
                var dtValue = Convert.ToDateTime((UInt64)myCallingObject);
                return new FuncParameter((Int64)UNIXTimeConversionExtension.ToUnixTimeStamp(dtValue));
            }
            else if (myCallingObject is DateTime)
            {
                return new FuncParameter(UNIXTimeConversionExtension.ToUnixTimeStamp((DateTime)myCallingObject));
            }
            else
            {
                throw new InvalidTypeException(myCallingObject.GetType().ToString(), "DateTime");
            }
        }

        public override string PluginName
        {
            get { return"sones.tounixdate"; }
        }

        public override PluginParameters<Type> SetableParameters
        {
            get { return new PluginParameters<Type>(); }
        }

        public override IPluginable InitializePlugin(String myUniqueString, Dictionary<string, object> myParameters = null)
        {
            return new ToUNIXDate();
        }

        public void Dispose()
        { }

        public override string FunctionName
        {
            get { return "tounixdate"; }
        }

        public override Type GetReturnType(IAttributeDefinition myWorkingBase, IGraphDB myGraphDB, SecurityToken mySecurityToken, TransactionToken myTransactionToken)
        {
            return typeof(Int64);
        }
    }
}
