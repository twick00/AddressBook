using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Address_Book.Models;

namespace Address_Book.Controllers
{
    public class HomeController : Controller
    {
        private Dictionary<int, Contact> AllContacts = Contact.GetAllContacts();
        [HttpGet("/")]
        public IActionResult Index()
        {
            AllContacts = Contact.GetAllContacts();
            return View("index", AllContacts);
        }
        [HttpGet("/addcontact")]
        public IActionResult NewContact()
        {
            ViewData["Message"] = "Your application description page.";
            return View("AddContact");
        }
        [HttpPost("/addcontact")]
        public IActionResult CreateNewContact()
        {
            Contact newContact = new Contact(Request.Form["name"],Request.Form["phone"],Request.Form["streetAddress"],Request.Form["city"],Request.Form["state"],Request.Form["zipCode"]);
            AllContacts = Contact.GetAllContacts();
            return RedirectToAction("Index");
        }

        [HttpGet("/test")]
        public IActionResult Test()
        {
            Contact cont = new Contact("Tyler Wickline", "4255555555","1010 Huntsfield Ave Ne", "Everett","Wa","99999");
            AllContacts = Contact.GetAllContacts();
            return View("index");
        }
        [HttpPost("/contact/delete/{id}")]
        public IActionResult DelContact(int id)
        {
            if (Contact.RemoveContact(id))
            {
                Console.WriteLine("Removed");
            }
            else
            {
                Console.WriteLine("Not Removed");
            }
            AllContacts = Contact.GetAllContacts();
            return View("index", AllContacts);
        }
        [HttpGet("/contact/{id}")]
        public IActionResult Contacts(int id)
        {
            return View("Contact", AllContacts[id]);
        }
        [HttpPost("/edit")]
        public IActionResult Edit(int id)
        {   
            AllContacts[id].SetName(Request.Form["name"]);
            AllContacts[id].SetPhone(Request.Form["phone"]);
            AllContacts[id].GetAddress().SetStreetAddress(Request.Form["streetAddress"]);
            AllContacts[id].GetAddress().SetCity(Request.Form["city"]);
            AllContacts[id].GetAddress().SetState(Request.Form["state"]);
            AllContacts[id].GetAddress().SetZipCode(Request.Form["zipCode"]);
            AllContacts = Contact.GetAllContacts();
            return View("index", AllContacts);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
