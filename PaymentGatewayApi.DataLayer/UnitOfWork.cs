using System.Threading.Tasks;

namespace PaymentGatewayApi.DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext myDbContext;
        public UnitOfWork(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }
        public async Task CompleteAsync()
        {
            await myDbContext.SaveChangesAsync();
        }
    }
}
