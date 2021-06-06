using MediatR;
using Order.Application.Ordering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Add
{
    public record AddOrder(AddOrderDto Model) : IRequest<bool>;
    public class AddOrderHandler : IRequestHandler<AddOrder, bool>
    {
        public Task<bool> Handle(AddOrder request, CancellationToken cancellationToken)
        {
            // the order login does not implement :)
            return Task.FromResult(true);
        }
    }
}
