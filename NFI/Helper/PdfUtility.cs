using System;
using System.ComponentModel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using NFI.Models;
using Anchor = iTextSharp.text.Anchor;
using Font = iTextSharp.text.Font;

namespace NFI.Helper
{
    public static class PdfUtility
    {
        private static readonly Font Linkfont = FontFactory.GetFont("Arial", 8, Font.UNDERLINE | Font.ITALIC, new BaseColor(0, 0, 255));
        private static readonly Font Headerfont = FontFactory.GetFont("Georgia", 12, Font.BOLD, BaseColor.BLACK);
        private static readonly Font Namefont = FontFactory.GetFont("Courier", 10, Font.BOLD, BaseColor.BLACK);
        private static readonly Font Valuefont = FontFactory.GetFont("Courier", 10, Font.NORMAL, BaseColor.BLACK);

        public static void SavePdfFile(string html, string fullPath,string rootpath)
        {
            var bytes = Encoding.UTF8.GetBytes(html);
            
            using (var input = new MemoryStream(bytes))
            {
                using (var document = new Document(PageSize.A4, 10, 10, 25, 10))
                {
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    using (var writer = PdfWriter.GetInstance(document, fileStream))
                    {
                        var htmlContext = new HtmlPipelineContext(null);
                        htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
                        var cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                        cssResolver.AddCssFile(Path.Combine(rootpath,@"Content/bootstrap.css"), true);
                        cssResolver.AddCss(".table th, .table td { border-top: none !important;}", true);
                        cssResolver.AddCss("body {height: 842px;width: 595px;margin - left: auto;margin - right: auto;}", true);
                        IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(document, writer)));
                        writer.CloseStream = false;
                        document.Open();
                        var worker = new XMLWorker(pipeline, true);
                        var xmlParse = new XMLParser(true, worker);
                        xmlParse.Parse(input);
                        xmlParse.Flush();
                        document.Close();
                    }
                }

            }
        }
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