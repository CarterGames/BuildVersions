using System;
using System.Text;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    /// <summary>
    /// A display script to show the build numbers in text form...
    /// </summary>
    public abstract class BuildInformationDisplay : MonoBehaviour
    {
        //
        //
        //  Fields
        //
        //
        
        // The strings to look out for when parsing the input for the display...
        private const string TypeString = "bv_type";
        private const string NumberString = "bv_number";
        private const string DateString = "bv_date";
        private const string DayString = "bv_day";
        private const string MonthString = "bv_month";
        private const string YearString = "bv_year";
        private const string SystematicString = "bv_systematic";
        private const string SystematicPatchString = "bv_systematic_patch";
        private const string SystematicMinorString = "bv_systematic_minor";
        private const string SystematicMajorString = "bv_systematic_major";
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

        
        //
        //
        //  Unity Methods
        //
        //
        
        
        private void OnEnable() => Initialise();

        
        //
        //
        //  Methods
        //
        //

        
        /// <summary>
        /// Initialises the display script...
        /// </summary>
        public virtual void Initialise()
        {
            if (buildInformation == null)
                buildInformation = AssetAccessor.GetAsset<BuildInformation>();
            
            UpdateDisplay();
        }
        

        /// <summary>
        /// Updates the display with the latest value...
        /// </summary>
        public virtual void UpdateDisplay() { }


        /// <summary>
        /// Parses the {} text to be the elements requested...
        /// </summary>
        /// <param name="toParse">The string to parse</param>
        /// <returns>The formatted string</returns>
        protected string Parse(string toParse)
        {
            var splitString = toParse.Split('{', '}');
            var split = Array.Empty<string>();

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

                        split = buildInformation.BuildDate.Split('/');
                        if (split.Equals(string.Empty))
                        {
                            BvLog.Warning("Unable to display the day element of the date, please make sure the date field is populated correctly.");
                            break;
                        }
                        
                        builder.Append(buildInformation.BuildDate.Split('/')[0]);
                        break;
                    case MonthString:
                        
                        split = buildInformation.BuildDate.Split('/');
                        if (split.Length < 2)
                        {
                            BvLog.Warning("Unable to display the month element of the date, please make sure the date field is populated correctly.");
                            break;
                        }
                        
                        builder.Append(buildInformation.BuildDate.Split('/')[1]);
                        break;
                    case YearString:
                        
                        split = buildInformation.BuildDate.Split('/');
                        if (split.Length < 3)
                        {
                            BvLog.Warning("Unable to display the year element of the date, please make sure the date field is populated correctly.");
                            break;
                        }
                        
                        builder.Append(buildInformation.BuildDate.Split('/')[2]);
                        break;
                    case SystematicString:
                        builder.Append(buildInformation.SystematicVersionNumber);
                        break;
                    case SystematicPatchString:
                        
                        split = buildInformation.SystematicVersionNumber.Split('.');

                        if (split.Length < 3)
                        {
                            BvLog.Warning("Unable to display patch of the systematic versioning number as it does not exist.");
                            break;
                        }

                        builder.Append(buildInformation.SystematicVersionNumber.Split('.')[2]);
                        break;
                    case SystematicMinorString:
                        
                        split = buildInformation.SystematicVersionNumber.Split('.');
                        if (split.Length < 2)
                        {
                            BvLog.Warning("Unable to display minor of the systematic versioning number as it does not exist.");
                            break;
                        }
                        
                        builder.Append(buildInformation.SystematicVersionNumber.Split('.')[1]);
                        break;
                    case SystematicMajorString:
                        
                        split = buildInformation.SystematicVersionNumber.Split('.');
                        if (split.Equals(string.Empty))
                        {
                            BvLog.Warning("Unable to display major of the systematic versioning number as it does not exist.");
                            break;
                        }
                        
                        builder.Append(buildInformation.SystematicVersionNumber.Split('.')[0]);
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