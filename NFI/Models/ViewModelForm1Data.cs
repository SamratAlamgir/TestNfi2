using System.Web;

namespace NFI.Models
{
    public class ViewModelForm1Data
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Company { get; set; }
        public HttpPostedFileBase file1 { get; set; }
        public HttpPostedFileBase file2 { get; set; }
    }
}