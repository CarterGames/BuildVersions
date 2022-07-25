using TMPro;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    /// <summary>
    /// Handles a standard build information display for the text mesh pro text component...
    /// </summary>
    [AddComponentMenu("Carter Games/Build Versions/Build Info Display (Text Mesh Pro)")]
    public class BuildInformationDisplayTmp : BuildInformationDisplay
    {
        //
        //
        //  Fields
        //
        //
        
        [Tooltip("The text element to show the output on.")]
        [SerializeField] private TMP_Text displayText = default;

        
        //
        //
        //  Method Overrides
        //
        //

        /// <summary>
        /// Updates the display with the latest value...
        /// </summary>
        public override void UpdateDisplay()
        {
            displayText.text = Parse(displayFormat);
        }
    }
}