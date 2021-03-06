using System.Threading.Tasks;
using Falcon.Numerics;

namespace MAVN.Service.CrossChainTransfers.Domain.RabbitMq.Handlers
{
    public interface ITransferToExternalFailedEventHandler
    {
        Task HandleAsync(string customerId, Money18 amount);
    }
}
