using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using YTVisionPro.Node;

namespace YTVisionPro.Forms.ProcessNew
{

    [Serializable]
    public class SerializableTreeNode
    {
        [XmlAttribute("Text")]
        public string Text { get; set; }

        [XmlAttribute("ImageIndex")]
        public int ImageIndex { get; set; }

        [XmlAttribute("Tag")]
        public NodeType Tag { get; set; }

        [XmlAttribute("SelectedImageIndex")]
        public int SelectedImageIndex { get; set; }

        [XmlArray("Nodes")]
        [XmlArrayItem("Node", Type = typeof(SerializableTreeNode))]
        public SerializableTreeNode[] Nodes { get; set; }

        public SerializableTreeNode()
        {
        }

        public SerializableTreeNode(TreeNode node)
        {
            Text = node.Text;
            ImageIndex = node.ImageIndex;
            if (node.Level == 1)
                Tag = (NodeType)node.Tag;
            SelectedImageIndex = node.SelectedImageIndex;
            if (node.Nodes.Count > 0)
            {
                Nodes = new SerializableTreeNode[node.Nodes.Count];
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    Nodes[i] = new SerializableTreeNode(node.Nodes[i]);
                }
            }
        }

        public TreeNode ToTreeNode()
        {
            var node = new TreeNode(Text)
            {
                ImageIndex = ImageIndex,
                SelectedImageIndex = SelectedImageIndex,
                Tag = Tag
            };

            if (Nodes != null && Nodes.Length > 0)
            {
                foreach (var child in Nodes)
                {
                    node.Nodes.Add(child.ToTreeNode());
                }
            }

            return node;
        }
    }

    public static class TreeViewSerializer
    {
        public static void SerializeTreeView(TreeView treeView, string filePath = "ToolTreeView.xml")
        {
            // 创建根节点
            var root = new SerializableTreeNode
            {
                Text = "Root",
                Nodes = new SerializableTreeNode[treeView.Nodes.Count]
            };

            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                root.Nodes[i] = new SerializableTreeNode(treeView.Nodes[i]);
            }

            // 创建 XmlSerializer 实例
            var serializer = new XmlSerializer(typeof(SerializableTreeNode));

            // 使用 StreamWriter 写入文件
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, root);
            }
        }

        public static void DeserializeTreeView(TreeView treeView, string filePath)
        {
            // 创建 XmlSerializer 实例
            var serializer = new XmlSerializer(typeof(SerializableTreeNode));

            // 使用 StreamReader 读取文件
            using (var reader = new StreamReader(filePath))
            {
                var root = (SerializableTreeNode)serializer.Deserialize(reader);

                // 清空现有的节点
                treeView.Nodes.Clear();

                // 添加反序列化的节点
                if (root.Nodes != null)
                {
                    foreach (var node in root.Nodes)
                    {
                        treeView.Nodes.Add(node.ToTreeNode());
                    }
                }
            }
        }
    }
}
