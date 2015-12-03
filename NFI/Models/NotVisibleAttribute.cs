using System;

namespace NFI.Models
{
    public class NotVisibleAttribute : Attribute
    {

    }

    public class HeaderAttribute : Attribute
    {
        public string Header { get; }
        public HeaderAttribute(string header)
        {
            Header = header;
        }
    }
}