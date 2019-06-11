using System;
using System.Xml;
using System.Xml.XPath;

namespace Oulanka.Domain
{
    public class Message
    {
        public int MessageId { get; } = -1;
        public string Title { get; set; }
        public string Body { get; set; }

        public Message(IXPathNavigable node)
        {
            if(node == null) throw new ArgumentNullException(nameof(node));

            var xnode = (XmlNode) node;

            MessageId = int.Parse(xnode.Attributes["id"].Value);
            Title = xnode.SelectSingleNode("title").InnerText;
            Body = xnode.SelectSingleNode("body").InnerText;
        }
    }
}