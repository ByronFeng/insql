﻿using Insql.MySql;
using System.Data;

namespace Insql
{
    public static partial class DbContextOptionsExtensions
    {
        public static DbContextOptions UseMySql(this DbContextOptions options)
        {
            options.ConnectionFactory = MySqlDbConnectionFactory.Instance;

            options.SqlResolverEnvironment["DbType"] = "MySql";

            return options;
        }

        public static DbContextOptions UseMySql(this DbContextOptions options, string connectionString)
        {
            options.ConnectionFactory = MySqlDbConnectionFactory.Instance;
            options.ConnectionString = connectionString;

            options.SqlResolverEnvironment["DbType"] = "MySql";

            return options;
        }

        public static DbContextOptions UseMySql(this DbContextOptions options, IDbConnection connection)
        {
            options.ConnectionFactory = MySqlDbConnectionFactory.Instance;
            options.ConnectionString = connection.ConnectionString;
            options.Connection = connection;

            options.SqlResolverEnvironment["DbType"] = "MySql";

            return options;
        }
    }
}
