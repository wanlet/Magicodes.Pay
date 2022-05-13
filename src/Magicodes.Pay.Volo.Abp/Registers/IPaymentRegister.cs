﻿using System;
using System.Threading.Tasks;
using Magicodes.Pay.Notify.Models;
using Magicodes.Pay.Volo.Abp.Dto;

namespace Magicodes.Pay.Volo.Abp.Registers
{
    /// <summary>
    /// 支付注册器
    /// </summary>
    public interface IPaymentRegister
    {
        /// <summary>
        /// 关键字
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// 构建支付配置
        /// </summary>
        /// <param name="logAction"></param>
        /// <returns></returns>
        Task Build(Action<string, string> logAction);

        /// <summary>
        /// 支付回调处理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ExecPayNotifyOutputDto> ExecPayNotifyAsync(PayNotifyWithGuidInput input);
    }
}