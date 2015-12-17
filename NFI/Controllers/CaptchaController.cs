using System;
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
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
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
                img = File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }


        [HttpPost]
        public ActionResult Index(CaptachaModel model, string returnUrl)
        {
            //validate captcha 
            var captchaResult = "Captcha" + model.Prefix;
            if (Session[captchaResult] != null && Session[captchaResult].ToString() == model.Captcha)
            {
                Session["IsCaptchaVerfied"] = true;
                if (string.IsNullOrEmpty(returnUrl))
                    throw new Exception("returnUrl must have a value to redirect");
                return Redirect(returnUrl);
            }
            ModelState.Remove("Prefix");
            model.Prefix = Guid.NewGuid().ToString();
            ModelState.AddModelError("Captcha", "Søknaden er sendt inn. Du vil motta en bekreftelse pr epost om kort tid.");
            return View(model);
        }

        public ActionResult Index()
        {
            var captchaModel = new CaptachaModel
            {
                Prefix = Guid.NewGuid().ToString(),
                Captcha = ""
            };

            return View(captchaModel);
        }
    }
}