namespace Smart.Data;

using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

public static class DbBatchExtensions
{
    public static DbBatchCommand Add(this DbBatch batch, string commandText)
    {
        var cmd = batch.CreateBatchCommand();
        cmd.CommandText = commandText;
        batch.BatchCommands.Add(cmd);
        return cmd;
    }

    public static DbBatchCommand Add(this DbBatch batch, string commandText, CommandType commandType)
    {
        var cmd = batch.CreateBatchCommand();
        cmd.CommandText = commandText;
        cmd.CommandType = commandType;
        batch.BatchCommands.Add(cmd);
        return cmd;
    }

    public static DbBatchCommand Add<TState>(this DbBatch batch, string commandText, TState state, Action<DbBatchCommand, TState> configure)
    {
        var cmd = batch.CreateBatchCommand();
        cmd.CommandText = commandText;
        configure(cmd, state);
        batch.BatchCommands.Add(cmd);
        return cmd;
    }

    public static DbBatchCommand Add<TState>(this DbBatch batch, string commandText, CommandType commandType, TState state, Action<DbBatchCommand, TState> configure)
    {
        var cmd = batch.CreateBatchCommand();
        cmd.CommandText = commandText;
        cmd.CommandType = commandType;
        configure(cmd, state);
        batch.BatchCommands.Add(cmd);
        return cmd;
    }

    public static int ExecuteNonQueryWithTransaction(this DbBatch batch, DbTransaction transaction)
    {
        batch.Transaction = transaction;
        return batch.ExecuteNonQuery();
    }

    public static async ValueTask<int> ExecuteNonQueryWithTransactionAsync(this DbBatch batch, DbTransaction transaction, CancellationToken cancellationToken = default)
    {
        batch.Transaction = transaction;
        return await batch.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }
}
