﻿using Insql.MySql;
using System.Data;

namespace Insql
{
    public static partial class DbContextOptionsExtensions
    {
        public static DbContextOptions UseMySql(this DbContextOptions options, string connectionString)
        {
            options.SessionFactory = new MySqlDbSessionFactory(options, connectionString);

            options.SqlResolverEnvironment["DbType"] = "MySql";

            return options;
        }

        public static DbContextOptions UseMySql(this DbContextOptions options, IDbConnection connection)
        {
            options.SessionFactory = new MySqlDbSessionFactory(options, connection);

            options.SqlResolverEnvironment["DbType"] = "MySql";

            return options;
        }
    }
}
