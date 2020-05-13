﻿using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Job.EthereumBridge.Contract;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Numerics;
using MAVN.Service.CrossChainTransfers.Domain.RabbitMq.Handlers;

namespace MAVN.Service.CrossChainTransfers.DomainServices.RabbitMq.Subscribers
{
    public class TransferToInternalDetectedSubscriber : JsonRabbitSubscriber<TransferToInternalDetectedEvent>
    {
        private readonly ITransferToInternalDetectedEventHandler _handler;
        private readonly ILog _log;

        public TransferToInternalDetectedSubscriber(
            ITransferToInternalDetectedEventHandler handler,
            string connectionString,
            string exchangeName,
            string queueName,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, queueName, logFactory)
        {
            _handler = handler;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(TransferToInternalDetectedEvent message)
        {
            await _handler.HandleAsync(message.OperationId, message.PrivateAddress, message.PublicAddress,
                message.Amount, message.PublicTransferId);
            _log.Info("Processed TransferToInternalDetectedEvent", message);
        }
    }
}
