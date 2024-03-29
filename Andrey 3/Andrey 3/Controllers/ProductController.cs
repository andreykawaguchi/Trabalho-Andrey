﻿using Andrey_3.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Andrey_3.Controllers
{
    using Andrey_3.Views.Categories;
    using System.Data.Entity;
    using System.Net;

    public class ProductsController : Controller
    {
        private EFContext context = new EFContext();
        // GET: Products

        public ActionResult Index()
        {
            var suppliers = context.Products.Include(c => c.Category).
            Include(f => f.Supplier).OrderBy(n => n.Name);
            return View(suppliers);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(context.Categories.
            OrderBy(b => b.Name), "CategoryId", "Name");
            ViewBag.SupplierId = new SelectList(context.Suppliers.
            OrderBy(b => b.Name), "SupplierId", "Name");
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(product);
            }
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.
                BadRequest);
            }
            Product product = context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(context.Categories.
            OrderBy(b => b.Name), "CategoryId", "Name", product.
            CategoryId);
            ViewBag.SupplierId = new SelectList(context.Suppliers.
            OrderBy(b => b.Name), "SupplierId", "Name", product.
            SupplierId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Entry(product).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }

        // GET: Produtos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.
                BadRequest);
            }
            Product product = context.Products.Where(p => p.ProductId ==
            id).Include(c => c.Category).Include(f => f.Supplier).
            First();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.
                BadRequest);
            }
            Product product = context.Products.Where(p => p.ProductId ==
            id).Include(c => c.Category).Include(f => f.Supplier).
            First();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                Product product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
                TempData["Message"] = "Produto " + product.Name.ToUpper() + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}