using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ATM_DAL
{
    /// <summary>
    /// Base data access layer
    /// </summary>
    public class Base_DAL
    {
        /// <summary>
        /// read Data From File
        /// </summary>
        /// <param name="filePath">string</param>
        /// <returns>List<string></returns>
        public List<string> readDataFromFile(string filePath)
        {
            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(filePath);
            string line = String.Empty;
            List<String> accounts = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }
            sr.Close();
            return list;
        }

        /// <summary>
        ///  write Data To File
        /// </summary>
        /// <param name="filePath">string</param>
        /// <param name="list">List<string></param>
        public void  writeDataToFile(string filePath, List<string> list)
        {
            StreamWriter sw = new StreamWriter(new FileStream(filePath, FileMode.Truncate, FileAccess.Write));
            for (int i = 0; i < list.Count; i++)
            {
                sw.WriteLine(list[i]);
            }
            sw.Close();
        }
    }
}
