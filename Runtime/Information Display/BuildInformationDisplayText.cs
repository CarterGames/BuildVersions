using UnityEngine;
using UnityEngine.UI;

namespace CarterGames.Assets.BuildVersions
{
    [AddComponentMenu("Carter Games/Build Versions/Build Info Display (Unity Text)")]
    public class BuildInformationDisplayText : BuildInformationDisplay
    {
        [Tooltip("The text element to show the output on.")]
        [SerializeField] private Text displayText = default;


        /// <summary>
        /// Updates the display with the latest value...
        /// </summary>
        public override void UpdateDisplay()
        {
            displayText.text = Parse(displayFormat);
        }
    }
}