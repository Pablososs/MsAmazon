using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWorkerService.Service
{
    public interface IAmazonHelper
    {

        public Task saveAsync();

        public Task<List<string>> getId();

        public Task saveOrderItem();

    }
}
