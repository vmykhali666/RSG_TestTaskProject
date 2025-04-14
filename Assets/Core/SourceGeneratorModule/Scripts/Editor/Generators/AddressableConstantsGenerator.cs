using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.CustomCodeGeneratorModule.Scripts.Editor.Core;
using Core.SourceGeneratorModule.Scripts.Tools;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

namespace Core.SourceGeneratorModule.Scripts.Generators {
    [Generator]
    public class AddressableConstantsGenerator : ICodeGenerator {
        private const string FILE_NAME = "Address";
        private const string BUILT_IN_DATA_GROUP_NAME = "BuiltInData";
        private const string DUPLICATE_ASSET_GROUP_NAME = "DuplicateAssetIsolation";
        private const string ALL_KEYS_FIELD_NAME = "AllKeys";

        public void Execute(GeneratorContext context) {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            
            ClassInstance mainClassInstance = new ClassInstance()
                .AddUsing("System.Collections.Generic")
                .AddNamespace(GeneratorConstants.DefaultNameSpace)
                .SetPublic()
                .SetStatic()
                .SetName(FILE_NAME);

            FieldInstance allKeysFieldInstance = new FieldInstance().SetPublic()
                .SetStatic()
                .SetListType("string")
                .SetName(ALL_KEYS_FIELD_NAME);

            List<string> mainClassAllKeys = new();

            foreach (AddressableAssetGroup addressableAssetGroup in settings.groups) {
                string groupName = GetGroupName(addressableAssetGroup);
                
                if(GroupIsSuitableForGenerating(groupName) is false)
                    return;
                
                List<string> groupClassAllKeys = new();

                ClassInstance addressableClass = new ClassInstance()
                    .SetPublic()
                    .SetStatic()
                    .SetName(groupName);

                foreach (AddressableAssetEntry addressableAssetEntry in addressableAssetGroup.entries.Where(addressableAssetEntry => !addressableAssetEntry.IsSubAsset)) {
                    string assetKey = GetAssetName(addressableAssetEntry);
                    
                    mainClassAllKeys.Add(assetKey);
                    groupClassAllKeys.Add(assetKey);
                    
                    addressableClass.AddField(new FieldInstance()
                        .SetPublic()
                        .SetConst()
                        .SetStringType()
                        .SetName(assetKey)
                        .SetAssignedValue(@$"""{addressableAssetEntry.address}"""));
                }

                mainClassInstance.AddClass(addressableClass);
                if(groupClassAllKeys.Any())
                    addressableClass.AddField(allKeysFieldInstance.Copy()
                        .SetAssignedValue(GetAssignedValueForStringList(groupClassAllKeys, 3)));
            }
            
            if(mainClassAllKeys.Any())
                mainClassInstance.AddField(allKeysFieldInstance.Copy()
                    .SetAssignedValue(GetAssignedValueForStringList(mainClassAllKeys, 2)));
            
            context.OverrideFolderPath(GeneratorConstants.ContentFilePath);
            context.AddFile(FILE_NAME + GeneratorConstants.GeneratedFileEnding, mainClassInstance.GetString());
        }

        private string GetAssignedValueForStringList(List<string> keys, int indentCount) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("new() {");
            
            foreach (string key in keys)
                stringBuilder.AppendLine(IndentGenerator.GetIndent(indentCount + 1) + @$"""{key}"",");

            stringBuilder.Append(IndentGenerator.GetIndent(indentCount) + "}");

            return stringBuilder.ToString();
        }

        private static string GetGroupName(AddressableAssetGroup addressableAssetGroup) =>
            addressableAssetGroup.Name.Replace(" ", string.Empty).Replace("-", string.Empty);

        private static string GetAssetName(AddressableAssetEntry addressableAssetEntry) {
            string assetName = addressableAssetEntry.address.Replace(" ", string.Empty)
                .Replace("-", string.Empty)
                .Replace(@"(", "_")
                .Replace(@")", string.Empty);

            if (assetName.Contains("/") || assetName.Contains(@"\"))
                assetName = assetName.Substring(assetName.LastIndexOf("/", StringComparison.Ordinal) + 1);

            if (assetName.Contains(@"\"))
                assetName = assetName.Substring(assetName.LastIndexOf(@"\", StringComparison.Ordinal) + 1);

            if (assetName.Contains("."))
                assetName = assetName.Substring(0, assetName.LastIndexOf(@".", StringComparison.Ordinal));

            return assetName;
        }

        public bool GroupIsSuitableForGenerating(string groupName) {
            if (string.Equals(groupName, BUILT_IN_DATA_GROUP_NAME, StringComparison.Ordinal))
                return false;
            
            if (string.Equals(groupName, DUPLICATE_ASSET_GROUP_NAME, StringComparison.Ordinal))
                return false;
            
            return !groupName.Any(s => char.IsSymbol(s) || char.IsWhiteSpace(s) || char.IsPunctuation(s));
        }
    }
}