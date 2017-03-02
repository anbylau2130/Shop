using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP.Models.POCO
{
    public class TreeNode
    {
        List<TreeNode> _children = new List<TreeNode>();
        public long id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public bool @checked { get; set; }
        public object attributes { get; set; }
        public List<TreeNode> children { get { return _children; } set { this._children = value; } }

        public string iconCls
        {
            get;
            set;
        }
    
    }
}