﻿using System.Threading.Tasks;

namespace Magicodes.Pay.Volo.Abp.TransactionLogs
{
    /// <summary>
    /// 交易日志仓储
    /// </summary>
    public interface ITransactionLogStore
    {
        Task SaveAsync(TransactionLog transactionLog);
    }
}