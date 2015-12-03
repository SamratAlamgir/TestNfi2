using System;
using System.ComponentModel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Reflection;
using System.Linq;
using NFI.Models;
using Font = iTextSharp.text.Font;

namespace NFI.Helper
{
    public static class PdfUtility
    {
        private static readonly Font Linkfont = FontFactory.GetFont("Arial", 8, Font.UNDERLINE | Font.ITALIC, new BaseColor(0, 0, 255));
        private static readonly Font Headerfont = FontFactory.GetFont("Georgia", 12, Font.BOLD, BaseColor.BLACK);
        private static readonly Font Namefont = FontFactory.GetFont("Courier", 10, Font.BOLD, BaseColor.BLACK);
        private static readonly Font Valuefont = FontFactory.GetFont("Courier", 10, Font.NORMAL, BaseColor.BLACK);
        public static void CreatePdf<T>(T toExport, string fileName, string attachmentLink)
        {


            using (var doc = new Document())

            {
                PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));
                doc.Open();
                var paragraph = new Paragraph();
                var chunk = new Chunk("Application Summary\n\n", Headerfont);
                paragraph.Add(chunk);
                var t = toExport.GetType();
                var fieldInfo = t.GetProperties().Where(
                    prop => Attribute.IsDefined(prop, typeof(NotVisibleAttribute)));
                Phrase p;
                foreach (var col in fieldInfo)
                {
                    p = GetPhrase(toExport, col);
                    paragraph.Add(p);
                }
                p = GetAttachmentLinkPhrase(attachmentLink);
                paragraph.Add(p);
                doc.Add(paragraph);
            }
        }

        private static Phrase GetAttachmentLinkPhrase(string attachmentLink)
        {
            var p = new Phrase();
            var c1 = new Chunk("Attachment Link".PadRight(30), Namefont);
            p.Add(c1);
            var anchor = new Anchor("Download Attachment", Linkfont)
            {
                Reference = attachmentLink,
            };
            p.Add(anchor);
            return p;
        }

        private static Phrase GetPhrase<T>(T toExport, PropertyInfo col)
        {

            var displayAttribute = col.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .Cast<DisplayNameAttribute>().FirstOrDefault();
            var linkAttribute = col.GetCustomAttributes(typeof(HyperLinkAttribute), true)
                .Cast<HyperLinkAttribute>().FirstOrDefault();
            var p = new Phrase();
            var keyName = displayAttribute != null ? displayAttribute.DisplayName : col.Name;
            var value = col.GetValue(toExport, null);
            var propetyValue = value?.ToString() ?? String.Empty;
            var c1 = new Chunk(keyName.PadRight(30), Namefont);
            p.Add(c1);
            if (linkAttribute != null)
            {
                if (linkAttribute is EmailLinkAttribute)
                {
                    var c2 = new Chunk(propetyValue, Linkfont);
                    p.Add(c2);
                }
                else
                {
                    var anchor = new Anchor(keyName, Linkfont)
                    {
                        Reference = propetyValue,
                    };
                    p.Add(anchor);
                }

            }
            else
            {
                var c2 = new Chunk(propetyValue, Valuefont);
                p.Add(c2);
            }
            var c3 = new Chunk(Environment.NewLine);
            p.Add(c3);
            return p;
        }
    }

}