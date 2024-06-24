using BackgroundServiceAmazon.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWorkerService.Service
{
    public class httpRepository : IhttpRepository
    {
        private readonly HttpClient _httpClient;

        public httpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Ordersapi.Ordert>> GetAmazonAsync()
        {
            var response = await _httpClient.GetAsync("https://testbobphp2.altervista.org/projectwork_its_vi/orders.php");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var root = JsonConvert.DeserializeObject<Ordersapi.Root>(json);


            return root?.payload?.Orders ?? new List<Ordersapi.Ordert>();
        }

                public async Task<List<SingleOrder.OrderItemt>> GetSingleOrder(List<string> idList)
                {
                  
                    var orders = new List<SingleOrder.OrderItemt>();


                    foreach (var id in idList)
                    {
                        var response = await _httpClient.GetAsync($"https://testbobphp2.altervista.org/projectwork_its_vi/orderitems.php?AmazonOrderID={id}");
                        response.EnsureSuccessStatusCode();
                        var json = await response.Content.ReadAsStringAsync();
                        var order = JsonConvert.DeserializeObject<SingleOrder.Root>(json);
                        if (order != null && order.payload != null && order.payload.OrderItems != null)
                        {
                            foreach (var orderItem in order.payload.OrderItems)
                            {
                                orderItem.AmazonOrderID = order.payload.AmazonOrderId;  // Assegna AmazonOrderID
                            }
                            orders.AddRange(order.payload.OrderItems);
                        }
                    }
                    return orders;
                }
    }
}
