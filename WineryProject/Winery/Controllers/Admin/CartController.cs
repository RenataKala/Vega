﻿using BL.Interfaces;
using BL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winery.Models;

namespace Winery.Controllers.Admin
{
    
    public class CartController : Controller
    {
        private readonly IWineRepository _wineRepository;

        public CartController()
        {
            _wineRepository = new WineRepository();
        }
        // GET: Cart
        public ActionResult Index()
        {
            // Init the cart list
            var cart = Session["cart"] as List<CartVM> ?? new List<CartVM>();
            // Check if cart is empty
            if (cart.Count == 0 || Session["cart"] == null)
            {
                ViewBag.Message = "Your cart is empty.";
                return View();
            }
            decimal total = 0m;
            //loop tru cart items and append to total
            foreach (var item in cart)
            {
                total += item.Total;
            }

            ViewBag.GrandTotal = total;
            return View(cart);
        }

        public ActionResult CartPartial()
        {
          CartVM model = new CartVM();
           
            int qty = 0;           
            decimal price = 0m;         
            if (Session["cart"] != null)
            {
                // Get total qty and price
                var list = (List<CartVM>)Session["cart"];

                foreach (var item in list)
                {
                    qty += item.Quantity;
                    price += item.Quantity * item.Price;
                }
                model.Quantity = qty;
                model.Price = price;
            }
            else
            {               
                model.Quantity = 0;
                model.Price = 0m;
            }
            
            return PartialView(model);
        }

        public ActionResult AddToCartPartial(int id)
        {
            // Init CartVM list
            List<CartVM> cart = Session["cart"] as List<CartVM> ?? new List<CartVM>();

            // Init CartVM
            CartVM model = new CartVM();

            // Get the product
            var wine = _wineRepository.GetByID(id);

           // Check if the product is already in cart
            var productInCart = cart.FirstOrDefault(x => x.ProductId == id);

                // If not, add new
                if (productInCart == null)
                {
                    cart.Add(new CartVM()
                    {
                        ProductId = wine.WineID,
                        ProductName = wine.Name,
                        Quantity = 1,
                        Price = wine.Price,
                        Image = wine.ImagePath
                    });
                }
                else
                {
                    productInCart.Quantity++;
                }         

            // Get total qty and price and add to model

            int qty = 0;
            decimal price = 0m;

            foreach (var item in cart)
            {
                qty += item.Quantity;
                price += item.Quantity * item.Price;
            }

            model.Quantity = qty;
            model.Price = price;

            // Save cart back to session
            Session["cart"] = cart;

            // Return partial view with model
            return PartialView(model);
        }

        public JsonResult IncrementProduct(int productId)
        {
            // Init cart list
            List<CartVM> cart = Session["cart"] as List<CartVM>;

            // Get cartVM from list
                CartVM model = cart.FirstOrDefault(x => x.ProductId == productId);

                // Increment qty
                model.Quantity++;

                // Store needed data
                var result = new { qty = model.Quantity, price = model.Price };

                // Return json with data
                return Json(result, JsonRequestBehavior.AllowGet);
           }

        public ActionResult DecrementProduct(int productId)
        {
            // Init cart
            List<CartVM> cart = Session["cart"] as List<CartVM>;
                            
                CartVM model = cart.FirstOrDefault(x => x.ProductId == productId);
                          
                if (model.Quantity > 1)
                {
                    model.Quantity--;
                }
                else
                {
                    model.Quantity = 0;
                    cart.Remove(model);
                }

                // Store needed data
                var result = new { qty = model.Quantity, price = model.Price };
                       
                return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void RemoveProduct(int productId)
        {
            // Init cart list
            List<CartVM> cart = Session["cart"] as List<CartVM>;
                    
                // Get model from list
                CartVM model = cart.FirstOrDefault(x => x.ProductId == productId);

                // Remove model from list
                cart.Remove(model);
        }
    }
}