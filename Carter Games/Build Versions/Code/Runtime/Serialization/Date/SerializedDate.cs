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

using System;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    /// <summary>
    /// A serializable date to hold the date of the last build made.
    /// </summary>
    [Serializable]
    public class SerializedDate
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Fields
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        [SerializeField] private int day;
        [SerializeField] private int month;
        [SerializeField] private int year;

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Properties
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// The day stored.
        /// </summary>
        public int Day => day;
        
        
        /// <summary>
        /// The month stored.
        /// </summary>
        public int Month => month;
        
        
        /// <summary>
        /// The year stored.
        /// </summary>
        public int Year => year;

        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Constructor
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Creates a blank SerializedDate class.
        /// </summary>
        public SerializedDate() { }
        
        
        /// <summary>
        /// Creates a SerializedDate class with the time entered.
        /// </summary>
        /// <param name="now"></param>
        public SerializedDate(DateTime now)
        {
            day = now.Day;
            month = now.Month;
            year = now.Year;
        }
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Override Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Overrides the ToString() method for this class.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
#if UNITY_EDITOR
            if (day == 0 || month == 0 || year == 0)
            {
                var date = DateTime.Now;
                day = date.Day;
                month = date.Month;
                year = date.Year;
            }
#endif
            
            return new DateTime(year, month, day).ToShortDateString();
        }
    }
}