using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServicesManger
    {
        public IProductService Product { get; }
        public IBasketServices Basket { get; }
        public ICacheServices Cache { get; }
        public IAuthService AuthService { get; }
        public IOrderServices orderService { get; }
        public IPaymentServices Payment { get; }
    }
}
