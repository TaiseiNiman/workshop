using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace MyProject
{
    public class IniFile
    {
        public string Path { get; }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder returnValue, int size, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileSection(string section, byte[] returnValue, int size, string filePath);

        public IniFile(string path)
        {
            Path = path;
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Path);
        }

        public string Read(string section, string key, string defaultValue = "")
        {
            var returnValue = new StringBuilder(255);
            GetPrivateProfileString(section, key, defaultValue, returnValue, 255, Path);
            return returnValue.ToString();
        }

        public Dictionary<string, string> ReadSection(string section)
        {
            var buffer = new byte[2048];
            GetPrivateProfileSection(section, buffer, buffer.Length, Path);
            var sectionData = Encoding.Unicode.GetString(buffer).Trim('\0').Split('\0');
            var result = new Dictionary<string, string>();

            foreach (var entry in sectionData)
            {
                var keyValue = entry.Split(new char[] { '=' }, 2);
                if (keyValue.Length == 2)
                {
                    result[keyValue[0]] = keyValue[1];
                }
            }

            return result;
        }

        public string ReadAll()
        {
            var buffer = new byte[2048];
            var result = new StringBuilder();

            foreach (var section in GetSections())
            {
                result.AppendLine($"[{section}]");
                var sectionData = ReadSection(section);
                foreach (var kvp in sectionData)
                {
                    result.AppendLine($"{kvp.Key}={kvp.Value}");
                }
                result.AppendLine();
            }

            return result.ToString();
        }

        private List<string> GetSections()
        {
            var buffer = new byte[2048];
            GetPrivateProfileSectionNames(buffer, buffer.Length, Path);
            var sections = Encoding.Unicode.GetString(buffer).Trim('\0').Split('\0');
            return new List<string>(sections);
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileSectionNames(byte[] returnValue, int size, string filePath);
    }
}
