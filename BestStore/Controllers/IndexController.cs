using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BestStore.Models;

namespace BestStore.Controllers
{
    public class IndexController : Controller
    {
       
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Register()
        {
            return View();
        }

        // POST: USERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "USERID,EMAIL,ADRESS,PASSWORD")] USER user)
        {
            if (ModelState.IsValid)
            {
                using (BestStoreEntities db = new BestStoreEntities())
                {
                    db.USER.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(USER objUser)
        {
            if (ModelState.IsValid)
            {
                using (BestStoreEntities db = new BestStoreEntities())
                {
                    var obj = db.USER.FirstOrDefault(a => a.EMAIL.Equals(objUser.EMAIL) && a.PASSWORD.Equals(objUser.PASSWORD));
                    if (obj != null)
                    {
                        Session["UserID"] = obj.USERID.ToString();
                        Session["UserName"] = obj.EMAIL.ToString();
                     
                        return RedirectToAction("Index");
                    }
                }
            }
            
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult ProductList()
        {

            BestStoreEntities db = new BestStoreEntities();
            
                
                return View(db.PRODUCT);


            
        }
        public ActionResult CheckOut()
        {

            


            return View();



        }
      
    }
}