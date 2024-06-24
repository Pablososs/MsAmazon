using BackgroundServiceAmazon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_api.Model.Domain;

namespace AmazonWorkerService.Service
{
    public interface IhttpRepository
    {
        Task<List<Ordersapi.Ordert>> GetAmazonAsync();

        Task<List<SingleOrder.OrderItemt>> GetSingleOrder(List<string> idList);
    }
}
