using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace RaceSim
{
    public static class visualisatie
    {

        #region graphics
/*
 * de verschillende sections
 * Straigth,
 * LeftCorner,
 * RightCorner,
 * StartGrid,
 * Finish
 */

        private static string[] _finishHorizontal = {"-----",
                                                     "     ",
                                                     "  !  ", 
                                                     "     ", 
                                                     "-----"};

        private static string[] _finishVertical = {"|   |",
                                                   "|   |", 
                                                   "| ! |", 
                                                   "|   |",
                                                   "|   |"};

        private static string[] _startGridHorizontal = {"-----",
                                                        "     ",
                                                        "  *  ",
                                                        "     ",
                                                        "-----"};

        private static string[] _startGridVertical = {"|   |",
                                                      "|   |",
                                                      "| * |",
                                                      "|   |",
                                                      "|   |"};

        private static string[] _straigthHorizontal = {"-----",
                                                       "     ", 
                                                       "     ", 
                                                       "     ", 
                                                       "-----"};

        private static string[] _straigthVertical = {"|   |", 
                                                     "|   |",  
                                                     "|   |",   
                                                     "|   |", 
                                                     "|   |"};

        private static string[] _cornerdownleft = {"----\\",
                                                   "    |",
                                                   "    |",
                                                   "    |",
                                                   "\\   |" };

        private static string[] _cornerupright = {"|   \\",
                                                   "|    ",
                                                   "|    ",
                                                   "|    ",
                                                   "\\----"};

        private static string[] _cornerupleft = {"/   |",
                                                "    |",
                                                "    |",
                                                "    |",
                                                "----/"};

        private static string[] _cornerdownright ={"/----",
                                                  "|    ",
                                                  "|    ",
                                                  "|    ",
                                                  "|   /" };










        #endregion




        static int richting = 1;
        ///noord = 0
        ///oost = 1 
        ///zuid = 2 
        ///west = 3



        public static void drawSection(String[] type, int x , int y) 
        {
            foreach(string row in type)
            {
                foreach (char c in row)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(c);
                    x++;
                }
                y++;
                x = x - 5;
            }


        }





        public static void drawTrack(Track track)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            int X = 50; //Console.CursorLeft;
            int Y = 50; //Console.CursorTop;


           foreach(Section section in track.Sections)
            {   
                if(section.SectionType == SectionTypes.StartGrid)
                {
                    drawSection(_startGridHorizontal, X, Y);
                    if(richting == 1)
                    {
                        X = X + 5;
                    } else
                    {
                        X = X - 10;
                    }
                }


                if(section.SectionType == SectionTypes.Straigth)
                {
                    if(richting == 1 || richting == 3)
                    {
                        drawSection(_straigthHorizontal, X, Y);
                        if(richting == 1)
                        {
                            X = X + 5;
                        } else
                        {
                            X = X - 10;
                        }
                    } else if(richting == 2 || richting == 4)
                    {
                        drawSection(_straigthVertical, X, Y);
                        if(richting == 2)
                        {
                            Y = Y + 5;
                        }
                        else
                        {
                            Y = Y - 10;
                        }
                    }
                }

                if (section.SectionType == SectionTypes.Finish)
                {
                    if (richting == 1 || richting == 3)
                    {
                        drawSection(_finishHorizontal, X, Y);
                        if (richting == 1)
                        {
                            X = X + 5;
                        }
                        else
                        {
                            X = X - 10;
                        }
                    }
                    else if (richting == 2 || richting == 4)
                    {
                        drawSection(_finishVertical, X, Y);
                        if (richting == 2)
                        {
                            Y = Y + 5;
                        }
                        else
                        {
                            Y = Y - 10;
                        }
                    }
                }

                if(section.SectionType == SectionTypes.LeftCorner)
                {
                    if(richting == 0)
                    {
                        drawSection(_cornerdownleft, X, Y);
                        richting = 3;
                        X = X - 5;
                    } else if(richting == 1)
                    {
                        drawSection(_cornerupleft, X, Y);
                        richting = 0;
                        Y = Y + -5;
                    } else if(richting == 2)
                    {
                        drawSection(_cornerupright, X, Y);
                            richting = 1;
                            X = X + 5;
                    } else if(richting == 3)
                    {
                        drawSection(_cornerdownright, X, Y);
                        richting = 2;
                        Y = Y + 5;
                    }
                }

                if(section.SectionType == SectionTypes.RightCorner)
                {
                    if(richting == 0)
                    {
                        drawSection(_cornerdownright, X, Y);
                        X = X + 5;
                        richting = 1;
                    } else if(richting == 1)
                    {
                        drawSection(_cornerdownleft, X, Y);
                        Y = Y + 5;
                        richting = 2;
                    } else if(richting == 2)
                    {
                        drawSection(_cornerupleft, X, Y);
                        X = X - 5;
                        richting = 3;
                    } else if(richting == 3)
                    {
                        drawSection(_cornerupright, X, Y);
                        Y = Y - 5;
                        richting = 0;
                    }


                }









            }









        }
            

            
    }



}

