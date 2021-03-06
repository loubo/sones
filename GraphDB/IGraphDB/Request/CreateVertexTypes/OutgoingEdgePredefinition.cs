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
using sones.GraphDB.TypeSystem;

namespace sones.GraphDB.Request
{
    /// <summary>
    /// The definition for an outgoing edge.
    /// </summary>
    public sealed class OutgoingEdgePredefinition: AttributePredefinition
    {

        #region Constant

        /// <summary>
        /// The name of the predefined edge type that represents a normal edge.
        /// </summary>
        public const string Edge = "Edge";

        /// <summary>
        /// The name of the predefined edge type that represents a edges with an attribute Weight of type double.
        /// </summary>
        public const string WeightedEdge = "Weighted";

        /// <summary>
        /// The name of the predefined edge type that represents a edges with an attribute Order.
        /// </summary>
        public const string OrderedEdge = "Ordered";

        #endregion

        #region Data

        /// <summary>
        /// The edge type of this edge definition
        /// </summary>
        public String EdgeType { get; private set; }


        /// <summary>
        /// The multiplicity of the edge.
        /// </summary>
        public EdgeMultiplicity Multiplicity { get; private set; }

        /// <summary>
        /// The inner edge type of a multi edge.
        /// </summary>
        public string InnerEdgeType { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a definition for an outgoing edge.
        /// </summary>
        /// <param name="myEdgeName">The name of the edge</param>
        public OutgoingEdgePredefinition(String myEdgeName)
            : base(myEdgeName)
        {
            EdgeType = Edge;
            Multiplicity = EdgeMultiplicity.SingleEdge;
        }

        /// <summary>
        /// Sets the edge type of this edge definition.
        /// </summary>
        /// <param name="myEdgeType">
        /// The name of the edge type. 
        /// </param>
        /// <seealso cref="SetAsSingleEdge"/>
        /// <seealso cref="SetMultiplicityAsHyperEdge"/>
        /// <seealso cref="SetEdgeTypeAsWeighted"/>
        /// <returns>The reference of the current object. (fluent interface).</returns>
        public OutgoingEdgePredefinition SetEdgeType(String myEdgeType)
        {
            if (myEdgeType != null)
                EdgeType = myEdgeType;
            return this;
        }

        /// <summary>
        /// Sets the edge multiplicity of this edge to HyperEdge.
        /// </summary>
        /// <returns>The reference of the current object. (fluent interface).</returns>
        public OutgoingEdgePredefinition SetMultiplicityAsHyperEdge()
        {
            InnerEdgeType = null;
            Multiplicity = EdgeMultiplicity.HyperEdge;
            return this;
        }

        /// <summary>
        /// Sets the edge multiplicity of this edge to MultiEdge.
        /// </summary>
        /// <returns>The reference of the current object. (fluent interface).</returns>
        public OutgoingEdgePredefinition SetMultiplicityAsMultiEdge(String myInnerEdgeType = null)
        {
            InnerEdgeType = myInnerEdgeType ?? Edge;
            Multiplicity = EdgeMultiplicity.MultiEdge;
            return this;
        }

        /// <summary>
        /// Sets the edge type of this edge definition to 'Weighted'.
        /// </summary>
        /// <returns>The reference of the current object. (fluent interface).</returns>
        public OutgoingEdgePredefinition SetEdgeTypeAsWeighted()
        {
            EdgeType = WeightedEdge;
            return this;
        }

        /// <summary>
        /// Sets the edge type of this edge definition to 'Ordered'.
        /// </summary>
        /// <returns>The reference of the current object. (fluent interface).</returns>
        public OutgoingEdgePredefinition SetEdgeTypeAsOrdered()
        {
            EdgeType = OrderedEdge;
            return this;
        }

        /// <summary>
        /// Sets the type of this outgoing edge with a VertexTypePredefinition.
        /// </summary>
        /// <param name="myTargetVertexType">The target vertex type predefinition</param>
        /// <returns>The reference of the current object. (fluent interface).</returns>
        public OutgoingEdgePredefinition SetAttributeType(VertexTypePredefinition myTargetVertexType)
        {
            if (myTargetVertexType != null)
                AttributeType = myTargetVertexType.VertexTypeName;

            return this;
        }

        public OutgoingEdgePredefinition SetComment(String myComment)
        {
            Comment = myComment;

            return this;
        }

        public OutgoingEdgePredefinition SetAttributeType(String myTypeName)
        {
            if (myTypeName != null)
                AttributeType = myTypeName;

            return this;
        }


        #endregion



    }
}