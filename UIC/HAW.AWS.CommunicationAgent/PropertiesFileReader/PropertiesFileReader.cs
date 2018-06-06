using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace HAW.AWS.CommunicationAgent.PropertiesFileReaders
{
    class PropertiesFileReader
    {
        private Dictionary<string, string> data;
        // match rows of a property row if they are seperated by a '=' or ':'
        // Note: a space as seperator is not recognized. Neither is a continuation 
        // on the next line with '/' possible.
        private Regex matchPropertyRow = new Regex(@"^(?!!|#).+(=|:).+$");
        private string filepath { get; set; }

        public PropertiesFileReader(string filepath)
        {
            data = new Dictionary<string, string>();
            this.filepath = filepath;
            try{
                writePropertiesFromFileToDictonary();
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: Configfile config.properties was not found or could not be read. Using DEFAULT VALUES: \n port_uic=8081, port_uas=8081");
                data.Add("port_uic", "8081");
                data.Add("port_uas", "8080");
            }
           

        }

        private void writePropertiesFromFileToDictonary()
        {
            string[] fileLines = File.ReadAllLines(filepath);
            
            foreach (var row in fileLines)
            {
                if (matchPropertyRow.IsMatch(row))
                {
                    string[] splittedRow = row.Split(new Char[] { '=', ':' }, 2);
                    string key = splittedRow[0].Trim();
                    string value = splittedRow[1].Trim();
                    data.Add(key, value);
                }
            }
        }

        public bool containsProperty(string key)
        {
            return data.ContainsKey(key);
        }

        public string getValue(string key)
        {
            return data[key];
        }
    }
}
