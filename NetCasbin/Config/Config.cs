﻿using System;
using System.Collections.Generic;
using System.IO;

namespace NetCasbin.Config
{
    public class Config
    {
        private static readonly string DEFAULT_SECTION = "default";
        private static readonly string DEFAULT_COMMENT = "#";
        private static readonly string DEFAULT_COMMENT_SEM = ";";

        // Section:key=value
        private readonly IDictionary<string, IDictionary<string, string>> _data;

        private Config()
        {
            _data = new Dictionary<string, IDictionary<string, string>>();
        }

        /// <summary>
        /// newConfig create an empty configuration representation from file.
        /// </summary>
        /// <param name="configFilePath">the path of the model file.</param>
        /// <returns>the constructor of Config.</returns>
        public static Config NewConfig(string configFilePath)
        {
            var c = new Config();
            c.Parse(configFilePath);
            return c;
        }

        /// <summary>
        /// newConfigFromText create an empty configuration representation from text.
        /// </summary>
        /// <param name="text">the model text.</param>
        /// <returns> the constructor of Config.</returns>
        public static Config NewConfigFromText(string text)
        {
            var c = new Config();
            c.ParseBuffer(new StringReader(text));
            return c;
        }

        /// <summary>
        /// addConfig adds a new section->key:value to the configuration.
        /// </summary>
        /// <param name="section"></param>
        /// <param name="option"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool AddConfig(string section, string option, string value)
        {
            if (string.IsNullOrEmpty(section))
            {
                section = DEFAULT_SECTION;
            }

            if (!_data.ContainsKey(section))
            {
                _data.Add(section, new Dictionary<string, string>());
            }

            var ok = _data[section].ContainsKey(option);
            _data[section].Add(option, value);
            return !ok;
        }

        private void Parse(string configFilePath)
        {
            using (var sr = new StreamReader(configFilePath))
            {
                ParseBuffer(sr);
            }
        }

        private void ParseBuffer(TextReader reader)
        {
            var section = "";
            var lineNum = 0;
            string line;
            while (true)
            {
                lineNum++;
                try
                {
                    if ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                catch (IOException e)
                {
                    throw new Exception("IO error occurred");
                }

                line = line.Trim();

                if (line.StartsWith(DEFAULT_COMMENT))
                {
                    continue;
                }
                else if (line.StartsWith(DEFAULT_COMMENT_SEM))
                {
                    continue;
                }
                else if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    section = line.Substring(1, line.Length - 2);
                }
                else
                {
                    var optionVal = line.Split("=".ToCharArray(), 2);
                    if (optionVal.Length != 2)
                    {
                        throw new Exception(
                                string.Format("parse the content error : line {0} , {1} = ? ", lineNum, optionVal[0]));
                    }
                    var option = optionVal[0].Trim();
                    var value = optionVal[1].Trim();
                    AddConfig(section, option, value);
                }
            }
        }

        public bool GetBool(string key)
        {
            return bool.Parse(Get(key));
        }

        public int GetInt(string key)
        {
            return int.Parse(Get(key));
        }

        public float GetFloat(string key)
        {
            return float.Parse(Get(key));
        }

        public string GetString(string key)
        {
            return Get(key);
        }

        public string[] GetStrings(string key)
        {
            var v = Get(key);
            if (string.IsNullOrEmpty(v))
            {
                return null;
            }
            return v.Split(',');
        }

        public void Set(string key, string value)
        {

            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("key is empty");
            }

            var section = "";
            string option;

            var keys = key.ToLower().Split(new string[] { "::" }, StringSplitOptions.None);
            if (keys.Length >= 2)
            {
                section = keys[0];
                option = keys[1];
            }
            else
            {
                option = keys[0];
            }
            AddConfig(section, option, value);
        }

        public string Get(string key)
        {
            string section;
            string option;

            var keys = key.ToLower().Split(new string[] { "::" }, StringSplitOptions.None);
            if (keys.Length >= 2)
            {
                section = keys[0];
                option = keys[1];
            }
            else
            {
                section = DEFAULT_SECTION;
                option = keys[0];
            }

            var ok = _data.ContainsKey(section) && _data[section].ContainsKey(option);
            if (ok)
            {
                return _data[section][option];
            }
            else
            {
                return "";
            }
        }
    }
}
