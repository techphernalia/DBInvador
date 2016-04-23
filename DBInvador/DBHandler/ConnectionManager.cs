using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace Techphernalia.DBInvader.DBHandler
{
    static class ConnectionManager
    {
        private static List<IDBHandler> IDBHandlers = new List<IDBHandler>();

        internal static List<IDBHandler> DBHandlers
        {
            get
            {
                return IDBHandlers;
            }
            private set
            {
                throw new InvalidOperationException("You can not explicitly set DBHandlers.");
            }
        }

        static ConnectionManager()
        {
            if (File.Exists("DBConfig.xml"))
            {
                XmlDocument XDoc = new XmlDocument();
                XDoc.Load("DBConfig.xml");
                foreach (XmlNode DBs in XDoc.SelectNodes("DBHandlers"))
                {
                    foreach (XmlNode DB in DBs.SelectNodes("DBHandler"))
                    {
                        DBHandlers.Add(GetDBHandler(DB.SelectSingleNode("displayname").InnerText, DB.SelectSingleNode("type").InnerText, DB.SelectSingleNode("connectionString").InnerText));
                    }
                }
            }
        }

        internal static IDBHandler GetDBHandler(string displayName, string dbServerType, string connectionString)
        {
            var dbServer = (DBServer)Enum.Parse(typeof(DBServer), dbServerType, true);
            IDBHandler handler = null;
            switch (dbServer)
            {
                case DBServer.MssqlServer:
                    handler = new MssqlServerHandler(connectionString);
                    handler.DisplayName = displayName;
                    handler.Name = handler.DisplayName;
                    break;
            }
            return handler;
        }

        public static void AddServer(IDBHandler dbHandler)
        {
            DBHandlers.Add(dbHandler);
            SaveConfig();
        }

        internal static void DeleteServer(IDBHandler dbHandler)
        {
            DBHandlers.Remove(dbHandler);
            SaveConfig();
        }

        private static void SaveConfig()
        {
            StringBuilder bldr = new StringBuilder();
            bldr.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            bldr.AppendLine("<DBHandlers>");
            bldr.AppendLine(string.Join(Environment.NewLine, (from h in DBHandlers select string.Format("<DBHandler><type>{0}</type><displayname>{1}</displayname><connectionString>{2}</connectionString></DBHandler>", h.DBServer.ToString(), h.DisplayName, h.ConnectionString))));
            bldr.Append("</DBHandlers>");
            using (StreamWriter rtr = new StreamWriter("DBConfig.xml", false))
            {
                rtr.Write(bldr.ToString());
                rtr.Flush();
            }
        }
    }
}
