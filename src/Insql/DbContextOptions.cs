﻿using Insql.Resolvers;
using System;
using System.Data;

namespace Insql
{
    public abstract class DbContextOptions
    {
        public abstract Type ContextType { get; }

        public IServiceProvider ServiceProvider { get; set; }

        public ISqlResolver SqlResolver { get; set; }

        public IDbConnection Connection { get; set; }

        public string ConnectionString { get; set; }

        public IDbConnectionFactory ConnectionFactory { get; set; }

        public int? CommandTimeout { get; set; }

        public string ServerName { get; set; }

        public int? ServerVersion { get; set; }
    }
}
