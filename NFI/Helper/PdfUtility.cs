using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Text;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;

namespace NFI.Helper
{
    public static class PdfUtility
    {
        public static void SavePdfFile(string html, string fullPath, string rootpath)
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
                        cssResolver.AddCssFile(Path.Combine(rootpath, @"Content/bootstrap.css"), true);
                        cssResolver.AddCssFile(Path.Combine(rootpath, @"Content/pdfreport.css"), true);
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


        public static void ParseHtmlToPdf(string html, string fullPath, string rootpath)
        {

            var bytes = Encoding.UTF8.GetBytes(html);

            using (var input = new MemoryStream(bytes))
            {
                using (var document = new Document(PageSize.A4, 10, 10, 25, 10))
                {
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    using (var writer = PdfWriter.GetInstance(document, fileStream))
                    {
                        XMLWorkerFontProvider fontProvider = new XMLWorkerFontProvider();
                        fontProvider.Register(Path.Combine(rootpath, @"fonts/GOTHIC.TTF"), "Default_CenturyGothic");
                        CssAppliers ca = new CssAppliersImpl(fontProvider);
                        var htmlContext = new HtmlPipelineContext(ca);
                        htmlContext.SetAcceptUnknown(true);
                        htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
                        var cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                        cssResolver.AddCssFile(Path.Combine(rootpath, @"Content/pdfreport.css"), true);
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
    }
}