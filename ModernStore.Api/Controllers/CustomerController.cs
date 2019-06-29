using Microsoft.AspNetCore.Mvc;
using ModernStore.Domain.Commands.Handlers;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Infra.Transactions;
using System.Threading.Tasks;

namespace ModernStore.Api.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly CustomerHandler _handler;

        public CustomerController(IUow uow, CustomerHandler handler)
            :base(uow)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("v1/customers")]
        public async Task<IActionResult> Post([FromBody] RegisterCustomerCommand command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }
    }
}