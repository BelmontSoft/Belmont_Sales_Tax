/// <summary>
/// +=================================================+
/// | Author:   Jeremiah Belmont
/// | Class:    ExemptionList
/// | Summary:  List of tax exempt Items designed for 
/// |           cross-checking Item names against.
/// +=================================================+
/// </summary>
using System;
using System.Collections.Generic;
using System.IO;

namespace Belmont_Sales_Tax
{
    public class ExemptionList
    {
        private const String filepath = @"../Belmont_Sales_Tax/Resources/ExemptItems.csv";
        private List<String> exemptItems;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ExemptionList()
        {
            exemptItems = new List<string>();
            InitList();
        }

        /// <summary>
        /// Fills the exemption list using the contents of ExemptItems.csv.
        /// </summary>
        private void InitList()
        {
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string exemptItemsFullFilepath = Path.Combine(projectPath, filepath);

            var reader = new StreamReader(exemptItemsFullFilepath);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] values = line.Split(',');
                    foreach (String value in values)
                    {
                        this.exemptItems.Add(value);
                    }
                }
            }
        }

        /// <summary>
        /// Checks to see whether an Item's name is present in the exemption list.
        /// </summary>
        /// <param name="itemName">Name to check exemption list for.</param>
        /// <returns>Item's basic tax exemption status.</returns>
        public bool Contains(String itemName)
        {
            String[] wordsInName = itemName.Split(null);

            foreach (String word in wordsInName)
            {
                if (this.exemptItems.Contains(word))
                {
                    return true;
                }
                // As a safety net, trim a theoretical 's' off the end of the Item's name and check that too.
                else if (itemName.Length > 2 && itemName[itemName.Length - 1] == 's')
                {
                    String possibleSingularWord = word.Remove(word.Length - 1);
                    if (this.exemptItems.Contains(possibleSingularWord))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
