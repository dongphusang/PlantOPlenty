using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// This class does 1-way binding with InfoBoard.xaml. PlantDescription and LuxValue are involved in this 1-way binding op
// All variables values are decided after PlantName is changed

namespace PlantOPlenty
{
    public class InfoBoardData
    {

        public static string plantName; // contains space. Ex: Alocasia Polly
        public static int averageLux;
        private static string plantNameNoSpace; // removed all white space from plantName var. Used within this class instead of plantName var (since file names don't have space -> easier file manipulation). Ex: AlocasiaPolly
        private string embeddedPicturePath;
        //private static string averageLux; // the lux value that is to be displayed to the user. The system will attempt to measure the surrounding light 10 times, sum them up, then divided by 10
        
        public string EmbeddedPicturePath { get { return embeddedPicturePath; } } // provides access to the variable for InfoBoard.xaml 

        private static string currentPlantGUI;  // decided right after PlantName is changed. Ex: assigned with AlocasiaPolly_Layout if PlantName = AlocasiaPolly. This serves to pull up the appropriate plant GUI
        public string PlantName { get { return plantName; } } // provides access to the variable for InfoBoard.xaml 
        public string PlantDescription { get; set; } // description of a plant. decided right after PlantName is changed. Pulled up from an embedded file
        public string RecommededLuxValue { get; set; }// A plant recommeded lux value. decided right after PlantName is changed. Pulled up from an embedded file
        public int AverageLux { get { return averageLux; } } // property of averageLux

        // initializing variables
        public InfoBoardData()
        {
            plantNameNoSpace = plantName.Replace(" ", "");
            embeddedPicturePath = $"{plantNameNoSpace}.jpg";
            currentPlantGUI = $"{plantNameNoSpace}_Layout";
            PlantDescription = GetPlantDescription();
            RecommededLuxValue = GetPlantLux();

            //debugging
            Console.WriteLine($"DEBUG: InfoBoardData.InfoBoardData().plantDescription.Debug => {PlantDescription}");
            Console.WriteLine($"DEBUG: InfoBoardData.InfoBoardData().luxValue.Debug => {RecommededLuxValue}");
            Console.WriteLine($"DEUBG: InfoBoardData.InfoBoardData().averageLux.Debug => {AverageLux}");
            

        }

        // serves to find and get the description of a plant
        private static string GetPlantDescription()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream($"PlantOPlenty.PlantsDescription.{plantNameNoSpace}_Desc.txt")) 
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
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
