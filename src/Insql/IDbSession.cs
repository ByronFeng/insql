﻿using System;
using System.Data;

namespace Insql
{
    public interface IDbSession : IDisposable
    {
        IDbConnection CurrentConnection { get; }

        IDbTransaction CurrentTransaction { get; }

        int? CommandTimeout { get; set; }

        void BeginTransaction();

        void BeginTransaction(IsolationLevel isolationLevel);

        void CommitTransaction();

        void RollbackTransaction();
    }
}
