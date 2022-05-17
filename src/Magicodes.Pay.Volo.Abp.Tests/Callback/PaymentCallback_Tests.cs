﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Magicodes.Pay.Abp;
using Magicodes.Pay.Abp.Callbacks;
using Magicodes.Pay.Abp.TransactionLogs;
using Magicodes.Pay.Volo.Abp.Tests;

namespace Magicodes.Pay.Volo.Abp.Tests.Callback
{
    public class PaymentCallback_Tests : TestBase
    {
        private IPaymentManager paymentManager;
        private string outTradeNo = "AAAAAAAAAAAAAAAAAAAAAAA";

        public PaymentCallback_Tests()
        {
            paymentManager = Resolve<IPaymentManager>();
            UsingDbContext(context => context.TransactionLogs.Add(new TransactionLog()
            {
                ClientIpAddress = "192.168.1.1",
                ClientName = "OS",
                CustomData = new
                {
                    Name = "佩奇",
                    IdCard = "430122200010016014",
                    Phone = "18812340001",
                    RecommendCode = "00001",
                    CreationTime = new DateTime(2019, 10, 1),
                    OpenId = "owWF25zT2BnOeQ68myWuQian7qHq"
                }.ToJsonString(),
                OutTradeNo = outTradeNo,
                Currency = new Currency(100),
                Name = "学费",
                PayChannel = PayChannels.AliAppPay,
                Terminal = Terminals.Ipad,
                TransactionState = TransactionStates.NotPay,
            }));
        }

        [Fact()]
        public async Task ExecuteCallback_Tests()
        {
            await paymentManager.ExecuteCallback("缴费支付", outTradeNo, "aaaa", 100);

            UsingDbContext(context =>
            {
                var log = context.TransactionLogs.First(p => p.OutTradeNo == outTradeNo);
                log.TransactionState.ShouldBe(TransactionStates.Success);
                log.PayTime.HasValue.ShouldBeTrue();
                log.Exception.ShouldBeNull();
            });
        }

        [Fact()]
        public async Task ExecuteCallbackError_Tests()
        {
            await Assert.ThrowsAsync<UserFriendlyException>(async () => await paymentManager.ExecuteCallback("缴费支付异常测试", outTradeNo, "aaaa", 100));

            UsingDbContext(context =>
            {
                //验证状态
                context.TransactionLogs.First(p => p.OutTradeNo == outTradeNo).TransactionState.ShouldBe(TransactionStates.PayError);

                //验证异常日志
                context.TransactionLogs.First(p => p.OutTradeNo == outTradeNo).Exception.ShouldNotBeNullOrEmpty();
                context.TransactionLogs.First(p => p.OutTradeNo == outTradeNo).Exception.ShouldContain("支付报错");
            });
        }

    }
}
