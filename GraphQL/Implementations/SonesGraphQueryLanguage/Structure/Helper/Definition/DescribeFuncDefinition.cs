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
using sones.GraphDB;
using sones.GraphQL.GQL.ErrorHandling;
using sones.GraphQL.GQL.Manager.Plugin;
using sones.GraphQL.Result;
using sones.Library.Commons.Security;
using sones.Library.Commons.Transaction;
using sones.Library.ErrorHandling;
using sones.Library.VersionedPluginManager;
using sones.Plugins.SonesGQL.Functions;

namespace sones.GraphQL.GQL.Structure.Helper.Definition
{
    public sealed class DescribeFuncDefinition : ADescribeDefinition
    {
        #region Data

        /// <summary>
        /// The function name
        /// </summary>
        private String _FuncName;

        #endregion

        #region Ctor

        public DescribeFuncDefinition(string myFuncName = null)
        {
            _FuncName = myFuncName;
        }

        #endregion

        public override QueryResult GetResult(
                                                GQLPluginManager myPluginManager, 
                                                IGraphDB myGraphDB, 
                                                SecurityToken mySecurityToken, 
                                                TransactionToken myTransactionToken)
        {
            var resultingVertices = new List<IVertexView>();
            ASonesException error = null;

            if (!String.IsNullOrEmpty(_FuncName))
            {

                #region Specific function

                //aggregate is user defined z.b. sones.exist or another.exist
                if (_FuncName.Contains("."))
                {
                    try
                    {
                        //get plugin
                        var func = myPluginManager.GetAndInitializePlugin<IGQLFunction>(_FuncName);

                        if (func != null)
                        {
                            resultingVertices = new List<IVertexView>() { GenerateOutput(func, _FuncName) };
                        }
                        else
                        {
                            error = new AggregateOrFunctionDoesNotExistException(typeof(IGQLFunction), _FuncName, "");
                        }
                    }
                    catch (ASonesException e)
                    {
                        error = new AggregateOrFunctionDoesNotExistException(typeof(IGQLFunction), _FuncName, "", e);
                    }
                }
                //try get function
                else
                {
                    try
                    {
                        //get plugin
                        var func = myPluginManager.GetAndInitializePlugin<IGQLFunction>(_FuncName);

                        if (func != null)
                        {
                            resultingVertices = new List<IVertexView>() { GenerateOutput(func, _FuncName) };
                        }
                        else
                        {
                            error = new AggregateOrFunctionDoesNotExistException(typeof(IGQLFunction), _FuncName, "");
                        }
                    }
                    catch (ASonesException e)
                    {
                        //maybe user forgot prefix 'sones.'
                        try
                        {
                            //get plugin
                            var func = myPluginManager.GetAndInitializePlugin<IGQLFunction>("SONES." + _FuncName);

                            if (func != null)
                            {
                                resultingVertices = new List<IVertexView>() { GenerateOutput(func, "SONES." + _FuncName) };
                            }
                            else
                            {
                                error = new AggregateOrFunctionDoesNotExistException(typeof(IGQLFunction), _FuncName, "");
                            }
                        }
                        catch (ASonesException ee)
                        {
                            error = new AggregateOrFunctionDoesNotExistException(typeof(IGQLFunction), _FuncName, "", e);
                        }
                    }
                }

                #endregion

            }

            else
            {

                #region All functions

                myPluginManager.GetPluginsForType<IGQLFunction>();
                foreach (var funcName in myPluginManager.GetPluginsForType<IGQLFunction>())
                {
                    try
                    {
                        //get plugin
                        var func = myPluginManager.GetAndInitializePlugin<IGQLFunction>(funcName);

                        if (func != null)
                        {
                            resultingVertices.Add(GenerateOutput(func, funcName));
                        }
                        else
                        {
                            error = new AggregateOrFunctionDoesNotExistException(typeof(IGQLFunction), funcName, "");
                        }
                    }
                    catch (ASonesException e)
                    {
                        error = new AggregateOrFunctionDoesNotExistException(typeof(IGQLFunction), funcName, "", e);
                    }
                }

                #endregion

            }

            if(error != null)
                return new QueryResult("", SonesGQLConstants.GQL, 0L, ResultType.Failed, resultingVertices, error);
            else
                return new QueryResult("", SonesGQLConstants.GQL, 0L, ResultType.Successful, resultingVertices);
        }

        #region Output

        /// <summary>
        /// Generate an output for an function description.
        /// </summary>
        /// <param name="myAggregate">The function.</param>
        /// <param name="myAggrName">The function name.</param>
        /// <returns>List of readouts with the information.</returns>
        private IVertexView GenerateOutput(IGQLFunction myFunc, String myFuncName)
        {
            var _Function = new Dictionary<String, Object>();
            var temp = new Dictionary<String, object>();
            var temp2 = new Dictionary<String, object>();
            var edges = new Dictionary<String, IEdgeView>();

            _Function.Add("Function", myFunc.FunctionName);
            _Function.Add("Type", myFuncName);
            _Function.Add("Description", myFunc.GetDescribeOutput());

            int count = 1;
            foreach (var parameter in ((IPluginable)myFunc).SetableParameters)
            {
                temp.Add("Parameter " + count.ToString() + " Key: ", parameter.Key);

                count++;
            }

            count = 1;
            foreach (var parameter in myFunc.GetParameters())
            {
                temp2.Add("Parameter " + count.ToString() + " Name: ", parameter.Name);

                count++;
            }

            edges.Add("SetableParameters", new SingleEdgeView(null, new VertexView(temp, null)));
            edges.Add("Parameters", new SingleEdgeView(null, new VertexView(temp2, null)));

            return new VertexView(_Function, edges);
        }

        #endregion
    }
}
