using System;

namespace DisplayActionSheet
{
    public class ActionSheetParameters
    {
        /// <summary>
        /// Action sheet title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Cancel button title
        /// </summary>
        public string Cancel { get; set; }

        /// <summary>
        /// Destructive action title
        /// </summary>
        public string Destruction { get; set; }

        /// <summary>
        /// List of Buttons
        /// </summary>
        public string[] Buttons { get; set; }
    }
}
