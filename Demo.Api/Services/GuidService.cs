using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Services
{
    public class GuidService : IGuidService
    {
        private readonly string _Guid;

        public GuidService() => _Guid = Guid.NewGuid().ToString();

    
        public string GetGuid() => _Guid;

    }
}
