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

using System.Collections.Generic;
using System.Linq;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Handles the build dialogue results...
    /// </summary>
    public static class BuildHandler
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Fields
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Stores the handler data for the updaters to use...
        /// </summary>
        private static readonly List<HandlerDialogueData> Data = new List<HandlerDialogueData>();
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        /// <summary>
        /// Registers a class into the handler system when a build is made...
        /// </summary>
        /// <param name="data">The data to add</param>
        public static void Register(HandlerDialogueData data) => Data.Add(data);
        
        
        /// <summary>
        /// Gets a handler data to use...
        /// </summary>
        /// <typeparam name="T">The class</typeparam>
        /// <returns>The result of the get</returns>
        public static HandlerDialogueData Get<T>()
        {
            return Data.FirstOrDefault(t => t.Id.Equals(UtilEditor.GetClassName<T>()));
        }

        
        /// <summary>
        /// Clears the data for a new build...
        /// </summary>
        public static void Clear() => Data.Clear();
    }
}