using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using TDJS_Vision.Node;

namespace TDJS_Vision.Forms.ProcessNew
{
    [Serializable]
    public class SerializableTreeNode
    {
        [XmlAttribute("Text")]
        public string Text { get; set; }

        [XmlAttribute("ImageName")]
        public string ImageName { get; set; }

        [XmlAttribute("Tag")]
        public NodeType Tag { get; set; }

        [XmlAttribute("SelectedImageName")]
        public string SelectedImageName { get; set; }

        [XmlArray("Nodes")]
        [XmlArrayItem("Node", Type = typeof(SerializableTreeNode))]
        public SerializableTreeNode[] Nodes { get; set; }

        public SerializableTreeNode()
        {
        }

        public SerializableTreeNode(TreeNode node, Dictionary<int, string> imageNames, Dictionary<int, string> selectedImageNames)
        {
            Text = node.Text;
            ImageName = GetImageName(node.ImageIndex, imageNames);
            if (node.Level == 1)
                Tag = (NodeType)node.Tag;
            SelectedImageName = GetImageName(node.SelectedImageIndex, selectedImageNames);
            if (node.Nodes.Count > 0)
            {
                Nodes = new SerializableTreeNode[node.Nodes.Count];
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    Nodes[i] = new SerializableTreeNode(node.Nodes[i], imageNames, selectedImageNames);
                }
            }
        }

        public TreeNode ToTreeNode(Dictionary<string, int> imageNameToIndex, Dictionary<string, int> selectedImageNameToIndex)
        {
            var node = new TreeNode(Text)
            {
                ImageIndex = GetImageIndex(ImageName, imageNameToIndex),
                SelectedImageIndex = GetImageIndex(SelectedImageName, selectedImageNameToIndex),
                Tag = Tag
            };

            if (Nodes != null && Nodes.Length > 0)
            {
                foreach (var child in Nodes)
                {
                    node.Nodes.Add(child.ToTreeNode(imageNameToIndex, selectedImageNameToIndex));
                }
            }

            return node;
        }

        private string GetImageName(int index, Dictionary<int, string> names)
        {
            if (names.TryGetValue(index, out var name))
            {
                return name;
            }
            return null;
        }

        private int GetImageIndex(string name, Dictionary<string, int> names)
        {
            if (names.TryGetValue(name, out var index))
            {
                return index;
            }
            return -1;
        }
    }

    public static class TreeViewSerializer
    {
        public static void SerializeTreeView(TreeView treeView, string filePath = "ToolTreeView.xml")
        {
            // 创建映射表
            var imageNames = new Dictionary<int, string>();
            var selectedImageNames = new Dictionary<int, string>();

            // 填充映射表
            for (int i = 0; i < treeView.ImageList.Images.Count; i++)
            {
                imageNames[i] = treeView.ImageList.Images.Keys[i].ToString();
                selectedImageNames[i] = treeView.ImageList.Images.Keys[i].ToString();
            }

            // 创建根节点
            var root = new SerializableTreeNode
            {
                Text = "Root",
                Nodes = new SerializableTreeNode[treeView.Nodes.Count]
            };

            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                root.Nodes[i] = new SerializableTreeNode(treeView.Nodes[i], imageNames, selectedImageNames);
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
            // 创建映射表
            var imageNameToIndex = new Dictionary<string, int>();
            var selectedImageNameToIndex = new Dictionary<string, int>();

            // 填充映射表
            for (int i = 0; i < treeView.ImageList.Images.Count; i++)
            {
                imageNameToIndex[treeView.ImageList.Images.Keys[i].ToString()] = i;
                selectedImageNameToIndex[treeView.ImageList.Images.Keys[i].ToString()] = i;
            }

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
                        treeView.Nodes.Add(node.ToTreeNode(imageNameToIndex, selectedImageNameToIndex));
                    }
                }
            }
        }
    }
}