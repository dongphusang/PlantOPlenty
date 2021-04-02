using System;
using System.Collections.Generic;
using System.Text;

// This class does 2-way binding with InfoBoard.xaml. Only PlantName variable is involed in this 2-way binding operation. Other variables values are deviced after PlantName is changed

namespace PlantOPlenty
{
    class InfoBoardData
    {
        public static string PlantName { get; set; } // name of selected plant. Assigned after a plant is selected 
        private static string currentPlantGUI;  // decided right after PlantName is changed. Ex: assigned with AlocasiaPolly_Layout if PlantName = AlocasiaPolly. This serves to pull up the appropriate plant GUI
        private static string plantDescription; // description of a plant. decided right after PlantName is changed. Pulled up from an embedded file
        private static int luxValue; // A plant recommeded lux value. decided right after PlantName is changed. Pulled up from an embedded file

        public InfoBoardData ()
        {
            Console.WriteLine($"{PlantName} Source: Infoboard.cs"); //debugging
        }


    }
}
