﻿using Insql.PostgreSql;
using System;
using System.Data;

namespace Insql
{
    public static partial class DbContextOptionsExtensions
    {
        public static DbContextOptions UsePostgreSql(this DbContextOptions options, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            options.SessionFactory = new PostgreSqlDbSessionFactory(options, connectionString);

            return options;
        }

        public static DbContextOptions UsePostgreSql(this DbContextOptions options, IDbConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            options.SessionFactory = new PostgreSqlDbSessionFactory(options, connection);

            return options;
        }
    }
}
