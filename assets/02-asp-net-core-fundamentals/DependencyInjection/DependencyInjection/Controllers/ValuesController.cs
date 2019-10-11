using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IOperationScoped _scopedOperation;
        private readonly IOperationSingleton _singletonOperation;

        public ValuesController(IOperationScoped scopedOperation, IOperationSingleton singletonOperation)
        {
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
        }

        [HttpGet]
        public ActionResult<Dictionary<string, IOperation>> Get()
        {
            return new Dictionary<string, IOperation>()
            {
                { "Scoped", _scopedOperation },
                { "Singleton", _singletonOperation }
            };
        }
    }

    public class Operation : IOperationScoped, IOperationSingleton
    {
        public Operation()
        {
            OperationId = Guid.NewGuid();
        }

        public Guid OperationId { get; private set; }
    }
}