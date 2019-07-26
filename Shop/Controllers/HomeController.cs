﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly AppDbContext context;


        public HomeController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var items = _itemRepository.GetAllItems().OrderBy(p => p.Name);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Title = "Items overview";
            homeViewModel.Items = items.ToList();
            
            

            return View(homeViewModel);
        }

        public IActionResult Details(int id)
        {
            var item = _itemRepository.GetItemById(id);
            if(item == null)
            {
                return NotFound();
            }
            return View(item);
        }


        public IActionResult AddNewItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewItem(Item item)
        {
            _itemRepository.Add(item);
            _itemRepository.Commit();
            return View();
        }

        
    }
}
