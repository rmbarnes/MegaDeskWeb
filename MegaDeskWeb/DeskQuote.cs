using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDeskWeb
{
    public class DeskQuote
    { 
        public Desk desk { get; set; }
        public int rushDays { get; set; }
        public string custName { get; set; }
        public DateTime quoteDate { get; set; }
        public int totalCost { get; set; }
        public int[,] rushOrderOptions = new int[3, 3];// { get; set; }
        public string[] quotes;// { get; set; }

        //Non-Default Constructor
        public DeskQuote(Desk desk, int rushDays, string custName, DateTime quoteDate)
        {
            this.desk = desk;
            this.rushDays = rushDays;
            this.custName = custName;
            this.quoteDate = quoteDate;
            totalCost = (getQuote());
        }

        //Default Constructor
        public DeskQuote()
        {
            desk = new Desk();
            rushDays = 0;
            custName = "NoNameJenkins";
            quoteDate = DateTime.Now;
            totalCost = 0;
        }

        public int getQuote()
        {
            int totalCost = 0;
            int basePrice = 200;
            int area = desk.Width * desk.Depth;

            int extraAreaCost = 0;
            //If area over 1000, do something about it!
            if (area > 1000)
            {
                int extraInches = area - 1000;
                extraAreaCost = extraInches * 1; //The cost per square inch
                //I know this is redundant, but it's show why it's times 1
            }

            //Find out extra pricing for rush options based on .txt file input
            ReadRushOrderPrices();
            int rushDaysCost = 0;
            if (rushDays == 3)
            {
                if (area < 1000)
                {
                    rushDaysCost = rushOrderOptions[0, 0];
                } else if (area >= 1000 && area <= 2000)
                {
                    rushDaysCost = rushOrderOptions[0, 1];
                } else //The area is > 2000
                {
                    rushDaysCost = rushOrderOptions[0, 2];
                }
            } else if (rushDays == 5)
            {
                if (area < 1000)
                {
                    rushDaysCost = rushOrderOptions[1, 0];
                }
                else if (area >= 1000 && area <= 2000)
                {
                    rushDaysCost = rushOrderOptions[1, 1];
                }
                else //The area is > 2000
                {
                    rushDaysCost = rushOrderOptions[1, 2];
                }
            } else if (rushDays == 7)
            {
                if (area < 1000)
                {
                    rushDaysCost = rushOrderOptions[2, 0]; //array[1][2][2].value
                }
                else if (area >= 1000 && area <= 2000)
                {
                    rushDaysCost = rushOrderOptions[2, 1];
                }
                else //The area is > 2000
                {
                    rushDaysCost = rushOrderOptions[2, 2];
                }
            } else //standard shipping
            {
                rushDaysCost = 0; //This line really isn't needed
            }

            //Find out how much the surface material costs
            int surfaceMaterialCost;
            switch (desk.SurfaceMaterial)
            {
                case Desk.Surface.Oak:
                    surfaceMaterialCost = 200;
                    break;
                case Desk.Surface.Laminate:
                    surfaceMaterialCost = 100;
                    break;
                case Desk.Surface.Pine:
                    surfaceMaterialCost = 50;
                    break;
                case Desk.Surface.Rosewood:
                    surfaceMaterialCost = 300;
                    break;
                case Desk.Surface.Veneer:
                    surfaceMaterialCost = 125;
                    break;
                default:
                    surfaceMaterialCost = 0;
                    break;
            }

            //Number of Drawers
            int numDrawersCost = desk.NumDrawers * 50; //$50 per drawer.

            //UPDATE THIS WITH ADDITIONAL COST VARIABLES IF NEEDED
            totalCost = basePrice + surfaceMaterialCost + extraAreaCost + rushDaysCost + numDrawersCost;
            this.totalCost = totalCost;
            return totalCost;
        }

        public void ReadRushOrderPrices()
        {
            //Read the quotes into a 1-dimentional array
            try
            {
                using (StreamReader sr = new StreamReader("rushOrderPrices.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        quotes = sr.ReadToEnd().Split('\n');
                    }
                }
            }
            catch
            {
                //You don messed up
            }

            //format dem quotes into a 3x3 array
            rushOrderOptions = new int[3,3];
            int quoteIndex = 0;
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    rushOrderOptions[i,j] = int.Parse(quotes[quoteIndex]);
                    quoteIndex++;
                }
            }
        }
    }
    
}