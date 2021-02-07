using System;
using System.Threading.Tasks;
using FoodForYou.Core.Application.ServiceContracts;
using FoodForYou.Core.Models.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodForYou.WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShipperService _shippersService;

        public OrdersController(IOrderService orderService, IShipperService shippersService)
        {
            _orderService = orderService;
            _shippersService = shippersService;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View( await _orderService.GetAll());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetId(id.Value);
            
            if (order == null)
            {
                return NotFound();
            }
            
            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ShipperID"] = new SelectList(await _shippersService.GetAll(), "ShipperID", "CompanyName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,OrderDate,CustomerID,EmployeeID,ShipperID")] OrderDetails order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.Create(order);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShipperID"] = new SelectList(await _shippersService.GetAll(), "ShipperID", "CompanyName");
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetId(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ShipperID"] = new SelectList(await _shippersService.GetAll(), "ShipperID", "CompanyName");
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,OrderDate,CustomerID,EmployeeID,ShipperID")] OrderDetails order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _orderService.Edit(order);
                }
                catch (Exception)
                {
                    if (!await _orderService.Exists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShipperID"] = new SelectList(await _shippersService.GetAll(), "ShipperID", "CompanyName");
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetId(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderService.Delete(await _orderService.GetId(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
