using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_api.Data;
using warehouse_api.Model.Domain;

namespace AmazonWorkerService.Service
{
    public class AmazonHelper : IAmazonHelper
    {
        private readonly DataContext _context;
        private readonly IhttpRepository _httpRepository;
        private readonly ILogger _logger;


        public AmazonHelper(DataContext context, IhttpRepository httpRepository)
        {
            _context = context;
            _httpRepository = httpRepository;
           
        }

        public async Task<List<string>> getId()
        {
            return await _context.Order.Select(x => x.AmazonOrderID).ToListAsync();
        }

        public async Task saveAsync()
        {
            var amazonOrders = await _httpRepository.GetAmazonAsync();

            try
            {

                foreach (var amazonOrder in amazonOrders)
                {
                    bool orderExists = await _context.Order
                    .AnyAsync(o => o.AmazonOrderID == amazonOrder.AmazonOrderId);

                    if (!orderExists)
                    {

                        var aus = new Order
                        {
                            AmazonOrderID = amazonOrder.AmazonOrderId,
                            PurchaseDate = amazonOrder.PurchaseDate,
                            OrderStatus = amazonOrder.OrderStatus,
                            NumberOfItemsShipped = amazonOrder.NumberOfItemsUnshipped,
                            MarketplaceID = amazonOrder.MarketplaceId
                        };

                        _context.Order.Add(aus);
                    }

                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

        }

        public async Task saveOrderItem()
        {

            var idList = await getId();
            var orderItems = await _httpRepository.GetSingleOrder(idList);

            foreach (var item in orderItems)
            {
                // Output diagnostico
                Console.WriteLine($"Processing OrderItem: {item.OrderItemId}"); 
                // Controllo nel contesto locale
                if (_context.OrderItem.Local.Any(oi => oi.OrderItemID == item.OrderItemId))
                {
                    Console.WriteLine($"OrderItem {item.OrderItemId} already exists in local context.");
                    continue;
                }

             
               

                      var existingOrderItem = await _context.OrderItem
                    .AsNoTracking()
                    .FirstOrDefaultAsync(oi => oi.OrderItemID == item.OrderItemId && oi.AmazonOrderID == item.AmazonOrderID);


                if (existingOrderItem != null)
                {
                    Console.WriteLine($"OrderItem {item.OrderItemId} already exists in database.");
                    continue;
                }

                // Creazione e aggiunta del nuovo OrderItem
                var aus = new OrderItem
                {
                    OrderItemID = item.OrderItemId,
                    AmazonOrderID = item.AmazonOrderID,
                    ASIN = item.ASIN,
                    Title = item.Title,
                    QuantityOrdered = item.QuantityOrdered,
                    ItemPrice = double.Parse(item.ItemPrice.Amount, CultureInfo.InvariantCulture)
                };

                _context.OrderItem.Add(aus);
                Console.WriteLine($"Added OrderItem {item.OrderItemId} to context.");
            }

            await _context.SaveChangesAsync();
            Console.WriteLine("Saved all OrderItems to database.");


        }
    }
}
