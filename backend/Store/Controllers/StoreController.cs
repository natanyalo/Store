using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Models;

namespace Store.Controllers
{
    [Route("store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly DataContext _dataCotext;
        public StoreController(DataContext dataCotext)
        {
            _dataCotext = dataCotext;

        }
        [HttpGet]
        public async Task<IActionResult> get()
        {
            
            var listProduct = _dataCotext.Products.ToList();
            return Ok(listProduct);
        }
        [HttpGet("search")]
        public async Task<IActionResult> getProduct(string name,string department,int price)
        {
            List<Product> listProduct = new List<Product>();
            if (price != 0 && listProduct != null)
                listProduct =  _dataCotext.Products.Where(p=>p.Name.Contains(name)).ToList();
            if (price != 0 && listProduct!=null)
                listProduct = listProduct.Where(item => item.Price == price).ToList();
            if ( listProduct!=null)
                listProduct = listProduct.Where(item => item.Department.Contains(department)).ToList();
            if (listProduct!=null)
           return Ok(listProduct);
            return BadRequest(new { message= " have en error"});
            
        }
        [HttpPost]
        public async Task<IActionResult> addProduct(Product product)
        {
            try
            {
                _dataCotext.Products.Add(product);
                await _dataCotext.SaveChangesAsync();
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
            
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var product = (from pro in _dataCotext.Products
                              where pro.Id == id
                              select pro).FirstOrDefault();
                if (product != null)
                    _dataCotext.Remove(product);
                _dataCotext.SaveChanges();
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

    }
}