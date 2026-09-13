using Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderSpecififcation :BaseSpecifications<Order,Guid>
    {
        public OrderSpecififcation(Guid Id) : base(o => o.Id == Id)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
        }
        public OrderSpecififcation(string UserEmail) : base(o => o.UserEmail == UserEmail)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
            AddOrderBy(o => o.dateTime);
        }
    }
}
