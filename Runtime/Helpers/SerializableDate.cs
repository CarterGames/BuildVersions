using System;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions
{
    [Serializable]
    public class SerializableDate
    {
        [SerializeField] private int day;
        [SerializeField] private int month;
        [SerializeField] private int year;


        public int Day => day;
        public int Month => month;
        public int Year => year;


        public SerializableDate() { }
        
        
        public SerializableDate(DateTime now)
        {
            day = now.Day;
            month = now.Month;
            year = now.Year;
        }
        

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