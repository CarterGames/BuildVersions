/*
* Copyright (c) 2018-Present Carter Games
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
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System.Collections.Generic;
using CarterGames.Common;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    public class AssetIndex : BuildVersionsAsset
    {
        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Fields
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        [SerializeField] private SerializableDictionary<string, List<BuildVersionsAsset>> assets;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Properties
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */

        /// <summary>
        /// A lookup of all the assets in the project that can be used at runtime.
        /// </summary>
        public SerializableDictionary<string, List<BuildVersionsAsset>> Lookup => assets;

        /* ─────────────────────────────────────────────────────────────────────────────────────────────────────────────
        |   Methods
        ───────────────────────────────────────────────────────────────────────────────────────────────────────────── */
        
        /// <summary>
        /// Sets the lookup to the value entered.
        /// </summary>
        /// <param name="value">The data to insert.</param>
        public void SetLookup(List<BuildVersionsAsset> value)
        {
            assets = new SerializableDictionary<string, List<BuildVersionsAsset>>();

            foreach (var foundAsset in value)
            {
                var key = foundAsset.GetType().ToString();
                
                if (assets.ContainsKey(key))
                {
                    if (assets[key].Contains(foundAsset)) continue;
                    assets[key].Add(foundAsset);
                }
                else
                {
                    assets.Add(key, new List<BuildVersionsAsset>()
                    {
                        foundAsset
                    });
                }
            }
        }
    }
}