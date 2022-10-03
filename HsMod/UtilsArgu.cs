using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HsMod
{
    public class UtilsArgu
    {
        private readonly Dictionary<string, Collection<string>> dictionary;

        private string argueKey;

        public int Count => dictionary.Count;

        private static UtilsArgu _instance;

        public static UtilsArgu Instance
        {
            get
            {
                return _instance ?? (_instance = new UtilsArgu(
                Environment.GetCommandLineArgs()));
            }
        }

        public Collection<string> this[string parameter]
        {
            get
            {
                if (!dictionary.ContainsKey(parameter))
                {
                    return null;
                }
                return dictionary[parameter];
            }
        }

        private static string[] smethod_0(string string_1)
        {
            StringBuilder stringBuilder = new StringBuilder(string_1);
            bool flag = false;
            for (int i = 0; i < stringBuilder.Length; i++)
            {
                if (stringBuilder[i] == '"')
                {
                    flag = !flag;
                }
                if (stringBuilder[i] == ' ' && !flag)
                {
                    stringBuilder[i] = '\n';
                }
            }
            string[] array = stringBuilder.ToString().Split(
                new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < array.Length; j++)
            {
                array[j] = RemoveQuotes(array[j]);
            }
            return array;
        }

        private static string RemoveQuotes(string argueValue)
        {
            int index1 = argueValue.IndexOf('"');
            int index2 = argueValue.LastIndexOf('"');
            while (index1 != index2)
            {
                argueValue = argueValue.Remove(index1, 1);
                argueValue = argueValue.Remove(index2 - 1, 1);
                index1 = argueValue.IndexOf('"');
                index2 = argueValue.LastIndexOf('"');
            }
            return argueValue;
        }

        public UtilsArgu(IEnumerable<string> arguments)
        {
            dictionary = new Dictionary<string, Collection<string>>();
            Regex regex = new Regex("^-{1,2}|^/|=|:",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            foreach (string argument in arguments)
            {
                string[] argueArray = regex.Split(argument, 3);
                switch (argueArray.Length)
                {
                    case 1:
                        SaveArgueValue(argueArray[0]);
                        break;
                    case 2:
                        SaveArgueKey();
                        argueKey = argueArray[1];
                        break;
                    case 3:
                        {
                            SaveArgueKey();
                            string argueValueStr = RemoveQuotes(argueArray[2]);
                            SaveAllArgueValue(argueArray[1], argueValueStr.Split(','));
                            break;
                        }
                }
            }
            SaveArgueKey();
        }

        private void SaveAllArgueValue(string key, IEnumerable<string> valueList)
        {
            foreach (string value in valueList)
            {
                AddDict(key, value);
            }
        }

        private void SaveArgueKey()
        {
            if (argueKey != null)
            {
                DictAddValue(argueKey, "true");
                argueKey = null;
            }
        }

        private void SaveArgueValue(string argueValue)
        {
            if (argueKey != null)
            {
                argueValue = RemoveQuotes(argueValue);
                AddDict(argueKey, argueValue);
                argueKey = null;
            }
        }

        private void AddDict(string key, string value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new Collection<string>());
            }
            dictionary[key].Add(value);
        }

        private void DictAddValue(string key, string value)
        {
            if (dictionary.ContainsKey(key))
            {
                throw new ArgumentException($"Argument {key} has already been defined");
            }
            dictionary.Add(key, new Collection<string>());
            dictionary[key].Add(value);
        }

        private void method_5(string string_1)
        {
            if (dictionary.ContainsKey(string_1))
            {
                dictionary.Remove(string_1);
            }
        }

        public bool IsTrue(string argueKey)
        {
            CheckSingle(argueKey);
            return this[argueKey]?[0].Equals(
                "true", StringComparison.OrdinalIgnoreCase) ?? false;
        }

        private void CheckSingle(string argueKey)
        {
            if (this[argueKey] != null && this[argueKey].Count > 1)
            {
                throw new ArgumentException(
                    $"{argueKey} has been specified more than once, expecting single value");
            }
        }

        public string Single(string argueKey)
        {
            CheckSingle(argueKey);
            if (this[argueKey] != null && !IsTrue(argueKey))
            {
                return this[argueKey][0];
            }
            return null;
        }

        public bool Exists(string argueKey)
        {
            if (this[argueKey] != null)
            {
                return this[argueKey].Count > 0;
            }
            return false;
        }
    }
}
