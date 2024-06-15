﻿using Microsoft.EntityFrameworkCore.Storage;
using TicketBuying.Application.Abstractions.Persistence;

namespace TicketBuying.Infrastructure.Persistence;

internal class ContextTransaction : IContextTransaction
{
    private readonly IDbContextTransaction _contextTransaction;

    public ContextTransaction(IDbContextTransaction contextTransaction)
    {
        _contextTransaction = contextTransaction;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _contextTransaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        await _contextTransaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        _contextTransaction.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _contextTransaction.DisposeAsync();
    }
}
