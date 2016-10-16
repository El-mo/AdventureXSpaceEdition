using System;
using System.IO;
using Xunit;

namespace AdventureXSpaceEdition
{
    public class SaveFileTests
    {
        private readonly string _fileName = "TestAdventureData.txt";

        private readonly string _fileLoc = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                           "\\AdventureX\\";


        [Fact]
        public void SaveFile_WhenSaveFileNotExists_CanCreateFile()
        {
            var filename = _fileName;
            var file = new FileInfo(_fileLoc + filename);

            var saveFile = new SaveFile(_fileName);
            //var loc = saveFile.GetLocation();
            //saveFile = null;

            Assert.True(file.Exists);
            file = null;
            DeleteFile(saveFile);
        }

        [Fact]
        public void SaveProperty()
        {
            var saveFile = GenerateSaveFile();
            var testObj = TestSaveProperty();

            saveFile.SaveAsync(testObj);

            DeleteFile(saveFile);
        }

        [Fact]
        public void SaveDifferentProperties()
        {
            var saveFile = GenerateSaveFile();
            var testObj = TestSaveProperty();
            var testObj2 = TestSaveProperty2();

            saveFile.SaveAsync(testObj);
            saveFile.SaveAsync(testObj2);

            DeleteFile(saveFile);
        }

        [Fact]
        public void GetCachedSaveProperty()
        {
            var saveFile = GenerateSaveFile();
            var testObj = TestSaveProperty();
            saveFile.SaveAsync(testObj);

            var resultTestObj = saveFile.Read<SaveObj>();

            DeleteFile(saveFile);

            Assert.Equal(testObj.name, resultTestObj.name);
            Assert.Equal(testObj.number, resultTestObj.number);
        }

        [Fact]
        public void GetSavePropertyFromFile()
        {
            var saveFile = GenerateSaveFile();
            var testObj = TestSaveProperty();
            saveFile.SaveAsync(testObj);
            var fileName = Path.GetFileName(saveFile.GetLocation());
            saveFile = null;
            var readSaveFile = new SaveFile(fileName);

            var resultTestObj = readSaveFile.Read<SaveObj>();

            DeleteFile(readSaveFile);

            Assert.Equal(testObj.name, resultTestObj.name);
            Assert.Equal(testObj.number, resultTestObj.number);
        }

        [Fact]
        public void GetNullSavePropertyFile()
        {
            var saveFile = GenerateSaveFile();
            Assert.Throws<SavePropertyNotFound>(() => saveFile.Read<SaveObj>());

            DeleteFile(saveFile);
        }

        private SaveFile GenerateSaveFile()
        {
            return new SaveFile(Guid.NewGuid() + _fileName);
        }

        private void DeleteFile(SaveFile saveFile)
        {
            var location = saveFile.GetLocation();
            saveFile = null;
            var file = new FileInfo(location);
            if (file.Exists)
                file.Delete();
        }
        private static SaveObj TestSaveProperty()
        {
            return new SaveObj
            {
                name = "name",
                number = 2
            };
        }
        
        private static SaveObj2 TestSaveProperty2()
        {
            return new SaveObj2
            {
                name = "name",
                number = 2
            };
        }
    }



    public class SaveObj 
    {
        public string name;
        public int number;
    }
    public class SaveObj2 
    {
        public string name;
        public int number;
    }
}