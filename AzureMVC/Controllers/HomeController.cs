using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AzureMVC.Helpers;
using AzureMVC.Models;
using AzureMVC.Repository;

namespace AzureMVC.Controllers
{
    public class HomeController : Controller
    {
        CustomersRepository repository = new CustomersRepository();

        public ActionResult Index()
        {
            List<Customer> customers = repository.GetCustomers();

            return View(customers);
        }

        public ActionResult Details(string rowKey, string partitionKey)
        {
            Customer customer = repository.GetCustomerByKey(partitionKey, rowKey);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public ActionResult Edit(string rowKey = "", string partitionKey = "")
        {
            if (rowKey == String.Empty && rowKey == String.Empty)
            {
                ViewBag.Message = "Add customer";
                return View(new Customer());
            }
                
            
            Customer customer = repository.GetCustomerByKey(partitionKey, rowKey);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Edit customer";
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    repository.InsertCustomer(customer);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", InnerValidationHelper.GetInnerValidationErrors(ex));
                    return View();
                }
                
                return RedirectToAction("Details", new { partitionKey = customer.PartitionKey, rowKey = customer.RowKey });
            }
            return View();
        }

        public ActionResult Remove(string rowKey, string partitionKey)
        {
            repository.DeleteCustomer(rowKey, partitionKey);
            return RedirectToAction("Index");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}