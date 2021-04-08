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
        public static int measuredLuxValue; // accessed in MainActivity class

        private static int measuredLux;            // local measuredLux var (used for consistent calculations) (global measuredLuxValue is bad for consistent calculations(value updated for calculations are done))
        private static string recommendedLuxValue; // recommended lux value. Raw data extracted from files
        private static string plantNameNoSpace;    // plant name without spaces
        private static double averageRecommendedLux;  // since recommended lux values can vary, only its average is used for calculations
        private static double progressTowardRecLux;       // progress toward recommmended lux value.  (measuredLuxValue / averageRecommendedLux)*100
        private static double boxViewHeight;          // progress bar clone's height
        private static string boxViewColour;          // progress bar colour. Red if measured light surpasses rec light, or green if vice versa
        private static string assessmentResult;     // contain messages such as: Below recommendation, above recommendation,..
        private static int lowerLuxLimit;           // lower recommended lux
        private static int upperLuxLimit;           // upper recommended lux
        // declaring properties
        public int MeasuredLux { get { return measuredLux; } } // enabled ReportPage to bind to measuredLuxValue
        public string RecommendedLuxValue { get { return recommendedLuxValue; }  }
        public double BoxViewHeight { get { return boxViewHeight; } }
        public string BoxViewColour { get { return boxViewColour; } }
        public string AssessmentResult { get { return assessmentResult; } }

        // constructor
        public ReportPageData ()
        {
            measuredLux = measuredLuxValue;
            plantNameNoSpace = PlantOPlenty.InfoBoardData.plantName.Replace(" ", "");
            recommendedLuxValue = GetPlantLux();
            averageRecommendedLux = GetAverageRecLux();
            progressTowardRecLux = GetProgressTowardRecLux();
            UpdateProgressBar();
            UpdateMeasuringStatus();
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
            int lower = 0; // lower bar of recommended lux
            int upper = 0; // upper bar of recommended lux
            int separatorIndex; // the index of the separator in text
            string stringHolder; // temp holder used for extracting upper and lower integers

            // extracting numbers from text
            stringHolder = recommendedLuxValue.Replace(" ", "");
            if (stringHolder.Contains("-")) // if recommendation varies
            {
                stringHolder = stringHolder.Replace("lux", "");
                stringHolder = stringHolder.Replace(",", "");
                separatorIndex = stringHolder.IndexOf("-");
                lower = Int32.Parse(stringHolder.Substring(0, separatorIndex));
                upper = Int32.Parse(stringHolder.Substring(separatorIndex + 1));
            }
            else  // recommendation is a single value
            {
                stringHolder = stringHolder.Replace("lux", "");
                stringHolder = stringHolder.Replace(",", "");
                upper = Int32.Parse(stringHolder.Substring(0));
                lower = upper;
            }
            lowerLuxLimit = lower;
            upperLuxLimit = upper;
            // calculating average and return value
            return (lower + upper) / 2;           
        }

        // calculate progress toward recommended lux
        private static double GetProgressTowardRecLux()
        {
            return (measuredLux / averageRecommendedLux);
        }

        // update progress bar (boxview) height
        private static void UpdateProgressBar()
        {
            boxViewHeight = 0;
            if (progressTowardRecLux < 1)
            {
                boxViewHeight = 425 * progressTowardRecLux;
            }
            else // if measured light is larger than recommended
            {
                boxViewHeight = 425;    // max height
            }
                
        }

        // update measuring status: Above rec, Below rec,...
        private static void UpdateMeasuringStatus()
        {
            if (measuredLux < lowerLuxLimit)
            {
                assessmentResult = "Below Recommendation";
                boxViewColour = "#e29c45";  // progress bar colour set to golden falling leaf

            }
            else if (measuredLux > upperLuxLimit)
            {
                assessmentResult = "Above Recommendation";
                boxViewColour = "Red";  // progress bar colour set to red due to overly bright light

            }
            else
            {
                assessmentResult = "Within Recommendation";
                boxViewColour = "#aedd94";  // progress bar colour set to light green

            }
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
