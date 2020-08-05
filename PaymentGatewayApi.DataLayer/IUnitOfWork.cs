using System.Threading.Tasks;

namespace PaymentGatewayApi.DataLayer
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
