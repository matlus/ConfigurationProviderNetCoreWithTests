﻿using System.Text;

namespace ConfigurationProviderNetCore
{
    /// <summary>
    /// This class holds ADO.NET Database connection specific information
    /// </summary>
    internal sealed class DbConnectionInformation
    {
        public string ConnectionStringName { get; }
        public string ConnectionString { get; }
        public string ProviderName { get; }

        public DbConnectionInformation(string connectionStringName, string connectionString, string providerName)
        {
            ConnectionStringName = connectionStringName;
            ConnectionString = connectionString;
            ProviderName = providerName;
        }
    }
}
