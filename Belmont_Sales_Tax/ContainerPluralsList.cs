/// <summary>
/// +=================================================+
/// | Author:   Jeremiah Belmont
/// | Class:    ContainerPluralsList
/// | Summary:  Provides the names ofcommon Item 
/// |           containers, featuring both their singular 
/// |           and plural variants.
/// |           Called a List for abstraction purposes,
/// |           but is really a Dictionary.
/// +=================================================+
/// </summary>
using System;
using System.Collections.Generic;
using System.IO;

namespace Belmont_Sales_Tax
{
    public class ContainerPluralsList
    {
        private const String filepath = @"../Resources/ContainerPlurals.csv";
        private Dictionary<String, String> containerPlurals;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ContainerPluralsList()
        {
            containerPlurals = new Dictionary<string, string>();
            InitDictionary();
        }

        /// <summary>
        /// Fills the dictionary using the contents of ContainerPlurals.csv.
        /// </summary>
        private void InitDictionary()
        {
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string exemptItemsFullFilepath = Path.Combine(projectPath, filepath);

            var reader = new StreamReader(exemptItemsFullFilepath);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] values = line.Split(':');
                        containerPlurals.Add(values[0], values[1]);
                }
            }
        }

        /// <summary>
        /// Checks to see if the plural of a container is present in the containers list.
        /// </summary>
        /// <param name="containerName">The name of a container plural (i.e., "boxes").</param>
        /// <returns>Whether the name is present in the containers list.</returns>
        public bool Contains(String containerName)
        {
            return this.containerPlurals.ContainsKey(containerName);
        }

        /// <summary>
        /// Returns the singular form of a container.
        /// </summary>
        /// <param name="containerName">The name of a container plural (i.e., "boxes").</param>
        /// <returns>The name of a singular container (i.e., "box")</returns>
        public String GetSingular(String containerName)
        {
            return this.containerPlurals[containerName];
        }
    }
}