using CSharpToTypescript.VSIX;

namespace CSharpToTypescript
{
    public class ConvertorEngine
    {
        private CSharpToTypescriptConverter converter;
        private ISettingStore currentSetingStore;
        private CSharpToTypescriptConverter Converter
        {
            get
            {
                if (converter == null)
                    converter = new CSharpToTypescriptConverter();
                return converter;
            }
        }

        private ISettingStore CurrentSetingStore
        {
            get
            {
                if (currentSetingStore == null)
                    currentSetingStore = new MySettings();
                return currentSetingStore;
            }
        }

        /// <summary>
        /// Remove strings like : internal, overwrite
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        private string RemoveParticularStrings(string fileContent)
        {
            string[] removables = { "internal", "overwrite" };
            for (int i = 0; i < removables.Length; i++)
            {
                fileContent = fileContent.Replace(removables[i], "");
            }
            return fileContent;
        }

        private string ReplaceTryCatch(string fileContent)
        {
            fileContent = fileContent.Replace("try", "string TRY;");
            fileContent = fileContent.Replace("catch", "string CATCH;");

            return fileContent;
        }

        public string Convert(string fileContent, string fileName = "")
        {
            if (string.IsNullOrWhiteSpace(fileContent))
                return "// empty input file;";

            fileContent = this.RemoveParticularStrings(fileContent);
            //fileContent = this.ReplaceTryCatch(fileContent);

            string resultTsFile = Converter.ConvertToTypescript(fileContent, CurrentSetingStore);
            if (string.IsNullOrWhiteSpace(resultTsFile))
                resultTsFile = string.Format("/* ERROR: The file {0} could not be converted.\r\n{1}\r\n*/", fileName + ".cs", Converter.ConvertException);

            return resultTsFile;
        }

    }

    class MySettings : ISettingStore
    {
        public bool AddIPrefixInterfaceDeclaration { get => false; set { } }
        public bool IsConvertListToArray { get => true; set { } }
        public bool IsConvertMemberToCamelCase { get => false; set { } }
        public bool IsConvertToInterface { get => false; set { } }
        public bool IsInterfaceOptionalProperties { get => false; set { } }
        public TypeNameReplacementData[] ReplacedTypeNameArray
        {
            get
            {
                return new TypeNameReplacementData[] {
                        new TypeNameReplacementData
                        {
                            NewTypeName = "Date",
                            OldTypeName = "DateTimeOffset"
                        },
                        new TypeNameReplacementData
                        {
                            NewTypeName = "Date",
                            OldTypeName = "DateTime"
                        }
                    };
            }
            set { }
        }
    }
}
