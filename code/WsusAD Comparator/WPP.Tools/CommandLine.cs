using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPP.Tools
{
    public class CommandLine
    {
        private string _optionPrefix = "-/";  // List of first characters that can prefix an option (-Option or /Option…)
        private Dictionary<string, string> _options = new Dictionary<string, string>();

        /// <summary>
        /// Returns an instace of the CommandLine class initialized with the provided command line.
        /// </summary>
        /// <param name="commandLine">The command line to parse.</param>
        /// <param name="removePrefix">A facultative parameter. True if the parser must remove the character that prefix each option (like -Option or /Option)</param>
        public  CommandLine(string[] commandLine, bool removePrefix=true)
        {
            this.Prefix = this._optionPrefix;

            foreach (string command in commandLine)
            {
                KeyValuePair<string, string> option = ParseToken(command, removePrefix);
                if (String.IsNullOrEmpty(option.Key))
                {
                    throw new ArgumentException("The option [" + command + "] have an empty Option Name.");
                }
                else
                { this._options.Add(option.Key, option.Value); }
            }
        }

        #region (Properties)

        /// <summary>
        /// Gets or Sets the list of characters used to prefix options in a CommandLine (Like -Option or /Option). By default, this property contains "-/".
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets the count of options found in the CommandLine.
        /// </summary>
        public int OptionCount { get { return this._options.Count; } }

        #endregion (Properties)


        #region (Methods)

        /// <summary>
        /// Determine, wether or not, an option is present in the CommandLine
        /// </summary>
        /// <param name="name">Name of the option to look for.</param>
        /// <returns>True if the option have been found.</returns>
        public bool OptionExists(string name)
        {
            try
            {
                GetOptionByName(name);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the value of an option identified by his name.
        /// </summary>
        /// <typeparam name="T">Type of the returned value.</typeparam>
        /// <param name="name">Name of the option to found.</param>
        /// <param name="defaultValue">Value that will be return if the option can't be found are the value can't be convert into the requested Type.</param>
        /// <returns>The value found in the CommandLine or the default value.</returns>
        public T GetOptionValue<T>(string name, object defaultValue)
        {
            try
            {
                KeyValuePair<string, string> option = this.GetOptionByName(name);

                return (T)Convert.ChangeType(option.Value, typeof(T));
            }
            catch (Exception)
            {
                return (T)Convert.ChangeType(defaultValue, typeof(T));
            }
        }

        /// <summary>
        /// Search the provided name into the CommandLine and return the Key/Value pair for this option. The search is not case sensitive.
        /// </summary>
        /// <param name="name">Name of the option to look for.</param>
        /// <returns>A Key/Value pair for this option.</returns>
        private KeyValuePair<string, string> GetOptionByName(string name)
        {
            foreach (KeyValuePair<string, string> pair in this._options)
            {
                if(String.Compare(pair.Key, name, true) == 0)
                {
                    return pair;
                }
            }
            throw new ArgumentException("Unable to find [" + name + "] in the CommandLine");
        }

        private KeyValuePair<string, string> ParseToken(string token, bool removePrefix)
        {
            if (removePrefix && this.Prefix.Contains(token.Substring(0, 1)))
            {
                token = token.Substring(1, token.Length - 1);   // Remove the prefix
            }
            int index = token.IndexOf('=');                     // search Key/Value separator
            string key = token.Substring(0, index);             // Get Key
            string value = token.Substring(index + 1);          // Get Value

            return new KeyValuePair<string, string>(key, value);
        }

        #endregion (Methods)
    }
}
