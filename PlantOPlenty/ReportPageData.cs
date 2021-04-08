using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace PlantOPlenty
{
    public class ReportPageData
    {
        // declaring variables
        public static int measuredLuxValue;

        private static int measuredLux;            // local measuredLux var (used for consistent calculations) (global measuredLuxValue is bad for consistent calculations(value updated for calculations are done))
        private static string recommendedLuxValue; // recommended lux value 
        private static string plantNameNoSpace;    // plant name without spaces
        private static int averageRecommendedLux;  // since recommended lux values can vary, only its average is used for calculations
        private static double progressTowardRecLux;       // progress toward recommmended lux value.  (measuredLuxValue / averageRecommendedLux)*100
        // declaring properties
        public int MeasuredLux { get { return measuredLux; } } // enabled ReportPage to bind to measuredLuxValue
        public string RecommendedLuxValue { get { return recommendedLuxValue; }  }

        // constructor
        public ReportPageData ()
        {
            measuredLux = measuredLuxValue;
            plantNameNoSpace = PlantOPlenty.InfoBoardData.plantName.Replace(" ", "");
            recommendedLuxValue = GetPlantLux();
            averageRecommendedLux = GetAverageRecLux();
            //progressTOwardRecLux = GetProgressTowardRecLux();
            //UpdateProgress();
        }
        
        // averaging recommended lux values
        /*
         * Get value in string 
         * Separate the string into two int values 
         * Find the average of the values
         */
        private static int GetAverageRecLux()
        {
            // declaring local vars
            int lower; // lower bar of recommended lux
            int upper; // upper bar of recommended lux
            int separatorIndex; // the index of the separator in text
            string stringHolder; // temp holder used for extracting upper and lower integers

            // extracting numbers from text
            stringHolder = recommendedLuxValue.Replace(" ", "");
            stringHolder = stringHolder.Replace("lux", "");
            stringHolder = stringHolder.Replace(",", "");
            separatorIndex = stringHolder.IndexOf("-");
            lower = Int32.Parse(stringHolder.Substring(0, separatorIndex));
            upper = Int32.Parse(stringHolder.Substring(separatorIndex+1));
            // calculating average
            return (lower + upper) / 2;           
        }

        // calculate progress toward recommended lux
        private static double GetProgressTowardRecLux()
        {
            return 3.3;
        }

        // update progress bar (boxview) attribute
        private static void UpdateProgressBar()
        {

        }

        // serves to find and get plant lux level
        private static string GetPlantLux()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream($"PlantOPlenty.PlantsDescription.{plantNameNoSpace}_Lux.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
