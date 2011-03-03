using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JsonViewer
{
    public partial class JsonViewer : Form
    {
        private Color defaultColor;

        public JsonViewer()
        {
            InitializeComponent();
            defaultColor = txtJson.BackColor;
        }

        private void mnuClear_Click(object sender, EventArgs e)
        {
            txtInput.Text = string.Empty;
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            txtJson.Text = txtInput.Text;
        }

        private void txtJson_TextChanged(object sender, EventArgs e)
        {
            object obj;
            tvJson.Nodes.Clear();
            if (SimpleJson.SimpleJson.TryJsonDecode(txtJson.Text, out obj))
            {
                var rootNode = new TreeNode("JSON");
                tvJson.Nodes.Add(rootNode);
                AddNode(obj, rootNode);
                txtJson.BackColor = this.defaultColor;
            }
            else if (txtJson.Text.Trim().Length != 0)
            {
                txtJson.BackColor = Color.IndianRed;
            }
        }

        #region Node Helpers

        private void AddNode(object obj, TreeNode node)
        {
            if (obj is IList<object>)
            {
                AddNode((IList<object>)obj, node);
            }
            else if (obj is IDictionary<string, object>)
            {
                AddNode((IDictionary<string, object>)obj, node);
            }
            else
            {
                if (obj == null)
                {
                    node.Nodes.Add("null");
                }
                else
                {
                    node.Nodes.Add(obj.ToString());
                }
            }
        }

        private void AddNode(IDictionary<string, object> dictionary, TreeNode node)
        {
            if (dictionary.Count == 0)
            {
                node.Nodes.Add("{}");
            }
            else
            {
                foreach (var pair in dictionary)
                {
                    var key = pair.Key;
                    if (pair.Value is IDictionary<string, object>)
                    {
                        var n = new TreeNode(pair.Key);
                        node.Nodes.Add(n);
                        AddNode(pair.Value, n);
                    }
                    else if (pair.Value is IList<object>)
                    {
                        var n = new TreeNode("[] " + pair.Key);
                        node.Nodes.Add(n);
                        AddNode((IList<object>)pair.Value, n);
                    }
                    else
                    {
                        // todo: differentiate null
                        node.Nodes.Add(string.Format("{0} : {1}", pair.Key, pair.Value));
                    }

                    node.ExpandAll();
                }
            }
        }

        private void AddNode(IList<object> list, TreeNode node)
        {
            foreach (var o in list)
            {
                AddNode(o, node);
            }
        }

        #endregion

    }
}
