//-----------------------------------------------------------------------
// <copyright file="JsonViewer.cs" company="The Outercurve Foundation">
//    Copyright (c) 2011, The Outercurve Foundation.
//
//    Licensed under the MIT License (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.opensource.org/licenses/mit-license.php
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
// <author>Nathan Totten (ntotten.com), Jim Zimmerman (jimzimmerman.com) and Prabir Shrestha (prabir.me)</author>
// <website>https://github.com/facebook-csharp-sdk/simple-json</website>
//-----------------------------------------------------------------------

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
            if (SimpleJson.SimpleJson.TryDeserializeObject(txtJson.Text, out obj))
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

                node.ExpandAll();
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
