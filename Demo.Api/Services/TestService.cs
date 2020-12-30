using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Services
{
    public class TestService
    {
        private readonly IGuidService _guidService;

        public TestService(IGuidService service)
        {
            _guidService = service;
        }


        public string getId() => _guidService.GetGuid();

    }
}
