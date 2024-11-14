using UnityEngine;
using SFB; // StandaloneFileBrowser‚Ì–¼‘O‹óŠÔ
using System.IO;
using MyProject;

namespace MyProject
{
    public static class FileSelector
    {
        public static string OpenFileDialog()
        {
            var extensions = new[] {
            new ExtensionFilter("All Files", "*"),
            new ExtensionFilter("Text Files", "txt"),
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg")
        };

            string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);
            if (paths.Length > 0)
            {
                string filePath = paths[0];
                Debug.Log("Selected file with full path: " + filePath);
                return filePath;
            }
            return "";
        }
    }
}
