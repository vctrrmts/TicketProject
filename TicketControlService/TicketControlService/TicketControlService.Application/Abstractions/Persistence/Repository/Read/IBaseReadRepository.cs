﻿namespace TicketControlService.Application.Abstractions.Persistence.Repository.Read
{
    public interface IBaseReadRepository<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> AsQueryable();

        public IAsyncRead<TEntity> AsAsyncRead();
    }
}
