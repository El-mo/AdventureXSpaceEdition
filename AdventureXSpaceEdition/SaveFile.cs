using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AdventureXSpaceEdition
{
    public class SaveFile
    {
        private readonly string _fileLocation;
        private readonly Dictionary<string, string> _properties;

        public SaveFile(string fileName)
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AdventureX\\";
            Directory.CreateDirectory(directory);
            _fileLocation = directory + fileName;

            if (!File.Exists(_fileLocation))
            {
                using (StreamWriter outputFile = new StreamWriter(_fileLocation, false))
                {
                    outputFile.Write("");
                }
            }

            _properties = ReadFromFile();
        }

        public string GetLocation()
        {
            return _fileLocation;
        }

        private Dictionary<string, string> ReadFromFile()
        {
            var json = File.ReadAllText(_fileLocation);
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return result ?? new Dictionary<string, string>();
        }

        public async void SaveAsync<T>(T saveProperty)
        {
            _properties.Add(typeof(T).Name, JsonConvert.SerializeObject(saveProperty));
            var json = JsonConvert.SerializeObject(_properties);

            using (StreamWriter outputFile = new StreamWriter(_fileLocation, false))
            {
                await outputFile.WriteAsync(json);
            }
        }

        public T Read<T>()
        {
            string propertyJson;
            try
            {
                propertyJson = _properties[typeof(T).Name];
            }
            catch (KeyNotFoundException)
            {
                throw new SavePropertyNotFound();
            }
            
            T result = JsonConvert.DeserializeObject<T>(propertyJson);
            return result;
        }
        
    }

    public class SavePropertyNotFound : KeyNotFoundException
    {
        
    }
}