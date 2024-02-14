/*
 * Copyright (c) 2024 Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Text;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    /// <summary>
    /// A display script to show the build numbers in text form...
    /// </summary>
    public abstract class BuildInformationDisplay : MonoBehaviour
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Fields
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        // The strings to look out for when parsing the input for the display..
        private const string TypeString = "bv_type";
        private const string NumberString = "bv_number";
        private const string DateString = "bv_date";
        private const string DayString = "bv_day";
        private const string MonthString = "bv_month";
        private const string YearString = "bv_year";
        private const string SemanticString = "bv_semantic";
        private const string SemanticPatchString = "bv_semantic_patch";
        private const string SemanticMinorString = "bv_semantic_minor";
        private const string SemanticMajorString = "bv_semantic_major";
        private const string NewLine = "newline";
        
        
        /// <summary>
        /// The string that is used to format the display...
        /// </summary>
        [Tooltip("The string to send to the display text. Use { } to define variables in the string.")]
        [SerializeField, TextArea(1, 5)] protected string displayFormat = "#{bv_number}";

        
        // The information to read...
        private BuildInformation buildInformation;
        
        
        // the string builder...
        private readonly StringBuilder builder = new StringBuilder();

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Unity Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        private void OnEnable() => Initialize();

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Initializes the display script...
        /// </summary>
        protected virtual void Initialize()
        {
            if (buildInformation == null)
                buildInformation = AssetAccessor.GetAsset<BuildInformation>();
            
            UpdateDisplay();
        }


        /// <summary>
        /// Updates the display with the latest value...
        /// </summary>
        public abstract void UpdateDisplay();


        /// <summary>
        /// Parses the {} text to be the elements requested...
        /// </summary>
        /// <param name="toParse">The string to parse</param>
        /// <returns>The formatted string</returns>
        protected string Parse(string toParse)
        {
            var splitString = toParse.Split('{', '}');
            string[] split;

            builder.Clear();
           
            foreach (var s in splitString)
            {
                switch (s.ToLower())
                {
                    case TypeString:
                        builder.Append(buildInformation.BuildType);
                        break;
                    case NumberString:
                        builder.Append(buildInformation.BuildNumber);
                        break;
                    case DateString:
                        builder.Append(buildInformation.BuildDate);
                        break;
                    case DayString:
                        builder.Append(buildInformation.BuildDate.Day);
                        break;
                    case MonthString:
                        builder.Append(buildInformation.BuildDate.Month);
                        break;
                    case YearString:
                        builder.Append(buildInformation.BuildDate.Year);
                        break;
                    case SemanticString:
                        builder.Append(buildInformation.SemanticVersionNumber);
                        break;
                    case SemanticPatchString:
                        
                        split = buildInformation.SemanticVersionNumber.Split('.');

                        if (split.Length < 3)
                        {
                            BvLog.Warning("Unable to display patch of the semantic versioning number as it does not exist.");
                            break;
                        }

                        builder.Append(buildInformation.SemanticVersionNumber.Split('.')[2]);
                        break;
                    case SemanticMinorString:
                        
                        split = buildInformation.SemanticVersionNumber.Split('.');
                        if (split.Length < 2)
                        {
                            BvLog.Warning("Unable to display minor of the semantic versioning number as it does not exist.");
                            break;
                        }
                        
                        builder.Append(buildInformation.SemanticVersionNumber.Split('.')[1]);
                        break;
                    case SemanticMajorString:
                        
                        split = buildInformation.SemanticVersionNumber.Split('.');
                        if (split.Equals(string.Empty))
                        {
                            BvLog.Warning("Unable to display major of the semantic versioning number as it does not exist.");
                            break;
                        }
                        
                        builder.Append(buildInformation.SemanticVersionNumber.Split('.')[0]);
                        break;
                    case NewLine:
                        builder.AppendLine();
                        break;
                    default:
                        builder.Append(s);
                        break;
                }
            }
            
            return builder.ToString();
        }
    }
}