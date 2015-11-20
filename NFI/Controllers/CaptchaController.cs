﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Web.Mvc;
using NFI.Models;

namespace NFI.Controllers
{

    public class CaptchaController : Controller
    {
        public ActionResult CaptchaImage(string prefix, bool noisy = false)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question 
            var a = rand.Next(10, 99);
            var b = rand.Next(0, 9);
            var captcha = $"{a} + {b} = ?";

            //store answer 
            Session["Captcha" + prefix] = a + b;

            //image stream 
            FileContentResult img;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(250, 75))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.NavajoWhite, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                            (rand.Next(0, 255)),
                            (rand.Next(0, 255)),
                            (rand.Next(0, 255)));

                        r = rand.Next(0, (400 / 3));
                        x = rand.Next(0, 400);
                        y = rand.Next(0, 75);
                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question 
                gfx.DrawString(captcha, new Font("Tahoma", 25), Brushes.Navy, 25, 25);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }


        [HttpPost]
        public ActionResult Index(CaptachaModel model,string returnUrl)
        {
            //validate captcha 
            if (Session["Captcha"] != null && Session["Captcha"].ToString() == model.Captcha)
            {
                Session["IsCaptchaVerfied"] = true;
                if (string.IsNullOrEmpty(returnUrl))
                    return Redirect("/Home/InputWizard");
                return Redirect(returnUrl);

            }
            ModelState.AddModelError("Captcha", "Wrong value of sum, please try again.");
            //dispay error and generate a new captcha 
            //Session["IsCaptchaVerfied"] = false;
            return View(model);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}