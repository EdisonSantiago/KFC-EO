using System;
using System.Collections.Generic;

namespace Oulanka.Domain.Dtos
{
    public class OulankaTreeNode
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string NodeType { get; set; }
        public Guid? ParentId { get; set; }
        public IList<OulankaTreeNode> Nodes { get; private set; }

        public OulankaTreeNode()
        {
            Nodes = new List<OulankaTreeNode>();
        }
    }
}