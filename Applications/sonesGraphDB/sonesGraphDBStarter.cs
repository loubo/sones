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
using System.IdentityModel.Selectors;
using sones.GraphDSServer;
using System.IdentityModel.Tokens;
using System.Diagnostics;
using sones.GraphDB;
using sones.Library.VersionedPluginManager;
using sones.GraphDS.PluginManager;
using sones.Library.Commons.Security;
using System.Net;
using System.Threading;
using sones.GraphDB.Manager.Plugin;
using System.IO;
using System.Globalization;
using sones.Library.DiscordianDate;
using System.Security.AccessControl;
#if __MonoCS__
using Mono.Unix.Native;
#endif

namespace sones.sonesGraphDBStarter
{
    #region PassValidator
    public class PassValidator : UserNamePasswordValidator
    {
        public override void Validate(String myUserName, String myPassword)
        {

            Debug.WriteLine(String.Format("Authenticate {0} and {1}", myUserName, myPassword));

            if (!(myUserName == Properties.Settings.Default.Username && myPassword == Properties.Settings.Default.Password))
            {
                throw new SecurityTokenException("Unknown Username or Password");
            }

        }
    }
    #endregion

    #region sones GraphDB Startup
    public class sonesGraphDBStartup
    {
        private bool quiet = false;
        private GraphDS_Server _dsServer;

        public sonesGraphDBStartup(String[] myArgs)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.DatabaseCulture);

            if (myArgs.Count() > 0)
            {
                foreach (String parameter in myArgs)
                {
                    if (parameter.ToUpper() == "--Q")
                        quiet = true;
                }
            }
            #region Start REST, WebDAV and WebAdmin services, send GraphDS notification

            IGraphDB GraphDB;

            if (Properties.Settings.Default.UsePersistence)
            {
                Console.WriteLine("Initializing persistence layer...");
                string configuredLocation = Properties.Settings.Default.PersistenceLocation;
                Uri location = null;

                if (configuredLocation.Contains("file:"))
                {
                    location = new Uri(configuredLocation);
                }
                else
                {
                    string rootPath = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly((typeof(sones.Library.Commons.VertexStore.IVertexStore))).Location);
                    string dataPath = rootPath + Path.DirectorySeparatorChar + configuredLocation;
                    location = new Uri(@dataPath);

                    //commented because if there is any folder inside the path wich contains a whitespace, 
                    //it is misinterpreted by the system and leads to failures
//                    if (!Directory.Exists(location.AbsolutePath))
//                    {
                        
//#if __MonoCS__
//                        Syscall.mkdir(location.AbsolutePath, FilePermissions.ACCESSPERMS);
//#else
//                        Directory.CreateDirectory(location.AbsolutePath);
//                        Directory.SetAccessControl(location.AbsolutePath, new DirectorySecurity(location.AbsolutePath, AccessControlSections.All));
//#endif
                        
//                    }
                }

                try
                {
                    //Make a new GraphDB instance
                    GraphDB = new SonesGraphDB(new GraphDBPlugins(new PluginDefinition("sones.pagedfsnonrevisionedplugin", new Dictionary<string, object>() { { "location", location } })));
                    Console.WriteLine("Persistence layer initialized...");
                }
                catch (Exception a)
                {
                    Console.WriteLine(a.Message);
                    Console.WriteLine(a.StackTrace);

                    Console.Error.WriteLine("Could not access the data directory " + location.AbsoluteUri + ". Please make sure you that you have the right file access permissions!");
                    Console.Error.WriteLine("Using in memory storage instead.");

                    GraphDB = new SonesGraphDB(null,true,new CultureInfo(Properties.Settings.Default.DatabaseCulture));
                }
            }
            else
            {
                GraphDB = new SonesGraphDB(null, true, new CultureInfo(Properties.Settings.Default.DatabaseCulture));
            }

            #region Configure PlugIns
            // Plugins are loaded by the GraphDS with their according PluginDefinition and only if they are listed
            // below - there is no auto-discovery for plugin types in GraphDS (!)

                #region Query Languages
                // the GQL Query Language Plugin needs the GraphDB instance as a parameter
                List<PluginDefinition> QueryLanguages = new List<PluginDefinition>();
                Dictionary<string, object> GQL_Parameters = new Dictionary<string, object>();
                GQL_Parameters.Add("GraphDB", GraphDB);

                QueryLanguages.Add(new PluginDefinition("sones.gql", GQL_Parameters));
                #endregion

                #region REST Service Plugins
                List<PluginDefinition> SonesRESTServices = new List<PluginDefinition>();
                // not yet used
                #endregion

                #region Drain Pipes            
                
                //// QueryLog DrainPipe
                //Dictionary<string, object> QueryLog_Parameters = new Dictionary<string, object>();
                //QueryLog_Parameters.Add("AsynchronousMode", true);  // do the work in a separate thread to not slow down queries
                //QueryLog_Parameters.Add("MaximumAsyncBufferSize", (Int32)1024 * 1024 * 10); // 10 Mbytes of maximum async queue size
                //QueryLog_Parameters.Add("AppendLogPathAndName", "sones.drainpipelog");
                //QueryLog_Parameters.Add("CreateNew", false); // always create a new file on start-up
                //QueryLog_Parameters.Add("FlushOnWrite", true);  // always flush on each write
            
                //// the DrainPipe Log expects several parameters
                //Dictionary<string, object> DrainPipeLog_Parameters = new Dictionary<string, object>();
                //DrainPipeLog_Parameters.Add("AsynchronousMode", true);  // do the work in a separate thread to not slow down queries
                //DrainPipeLog_Parameters.Add("MaximumAsyncBufferSize", (Int32)1024 * 1024 * 10); // 10 Mbytes of maximum async queue size
                //DrainPipeLog_Parameters.Add("AppendLogPathAndName", "sones.drainpipelog");
                //DrainPipeLog_Parameters.Add("CreateNew", false); // always create a new file on start-up
                //DrainPipeLog_Parameters.Add("FlushOnWrite", true);  // always flush on each write

                //Dictionary<string, object> DrainPipeLog2_Parameters = new Dictionary<string, object>();
                //DrainPipeLog2_Parameters.Add("AsynchronousMode", true);  // do the work in a separate thread to not slow down queries
                //DrainPipeLog2_Parameters.Add("MaximumAsyncBufferSize", (Int32)1024 * 1024 * 10); // 10 Mbytes of maximum async queue size
                //DrainPipeLog2_Parameters.Add("AppendLogPathAndName", "sones.drainpipelog2");
                //DrainPipeLog2_Parameters.Add("CreateNew", false); // always create a new file on start-up
                //DrainPipeLog2_Parameters.Add("FlushOnWrite", true);  // always flush on each write


                List<PluginDefinition> DrainPipes = new List<PluginDefinition>();
                //DrainPipes.Add(new PluginDefinition("sones.querylog", QueryLog_Parameters));
                //DrainPipes.Add(new PluginDefinition("sones.drainpipelog", DrainPipeLog_Parameters));
                //DrainPipes.Add(new PluginDefinition("sones.drainpipelog", DrainPipeLog2_Parameters));
                #endregion

            #endregion

            GraphDSPlugins PluginsAndParameters = new GraphDSPlugins(SonesRESTServices,QueryLanguages,DrainPipes);

            _dsServer = new GraphDS_Server(GraphDB, Properties.Settings.Default.ListeningPort,Properties.Settings.Default.Username,Properties.Settings.Default.Password, IPAddress.Any, PluginsAndParameters);
            _dsServer.LogOn(new UserPasswordCredentials(Properties.Settings.Default.Username,Properties.Settings.Default.Password));
            _dsServer.StartRESTService("", Properties.Settings.Default.ListeningPort, IPAddress.Any);

            #endregion

            #region Some helping lines...
            if (!quiet)
            {
                DiscordianDate ddate = new DiscordianDate();

                Console.WriteLine("sones GraphDB version 2.0 - "+ddate.ToString());
                Console.WriteLine("(C) sones GmbH 2007-2011 - http://www.sones.com");
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("This GraphDB Instance offers the following options:");
                Console.WriteLine("   * If you want to suppress console output add --Q as a");
                Console.WriteLine("     parameter.");
                Console.WriteLine();
                Console.WriteLine("   * REST Service is started at http://localhost:"+Properties.Settings.Default.ListeningPort);
                Console.WriteLine("      * access it directly like in this example: ");
                Console.WriteLine("           http://localhost:"+Properties.Settings.Default.ListeningPort+"/gql?DESCRIBE%20VERTEX%20TYPES");
                Console.WriteLine("      * if you want JSON Output add ACCEPT: application/json ");
                Console.WriteLine("        to the client request header (or application/xml or");
                Console.WriteLine("        application/text)");
                Console.WriteLine();
                Console.WriteLine("   * we recommend to use the AJAX WebShell. ");
                Console.WriteLine("        Browse to http://localhost:"+Properties.Settings.Default.ListeningPort+"/WebShell and use");
                Console.WriteLine("        the username \""+Properties.Settings.Default.Username+"\" and password \""+Properties.Settings.Default.Password+"\"");
                Console.WriteLine();

                Console.WriteLine("Enter 'shutdown' to initiate the shutdown of this instance.");
            }

            bool shutdown = false;
            while (!shutdown)
            {
                String command = Console.ReadLine();

                if (command.ToUpper() == "SHUTDOWN")
                    shutdown = true;
            }

            _dsServer.Shutdown(null);

            //GraphDB.Shutdown(null);

            #endregion
        }
    }
    #endregion

    public class sonesGraphDBStarter
    {
        static void Main(string[] args)
        {
            var sonesGraphDBStartup = new sonesGraphDBStartup(args);
        }
    }
}
