using System.IO.IsolatedStorage;
using Controller;
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
                                                     "  1  ",
                                                     "  !  ",
                                                     "  2  ",
                                                     "-----"};

        private static string[] _finishVertical = {"|   |",
                                                   "|   |",
                                                   "|1!2|",
                                                   "|   |",
                                                   "|   |"};

        private static string[] _startGridHorizontal = {"-----",
                                                        "  1  ",
                                                        "  *  ",
                                                        "  2  ",
                                                        "-----"};

        private static string[] _startGridVertical = {"|   |",
                                                      "|   |",
                                                      "|1*2|",
                                                      "|   |",
                                                      "|   |"};

        private static string[] _straigthHorizontal = {"-----",
                                                       "  1  ",
                                                       "     ",
                                                       "  2  ",
                                                       "-----"};

        private static string[] _straigthVertical = {"|   |",
                                                     "|   |",
                                                     "|1 2|",
                                                     "|   |",
                                                     "|   |"};

        private static string[] _cornerdownleft = {"----\\",
                                                   "   1|",
                                                   "    |",
                                                   " 2  |",
                                                   "\\   |" };

        private static string[] _cornerupright = {"|   \\",
                                                   "|  1 ",
                                                   "|    ",
                                                   "|2   ",
                                                   "\\----"};

        private static string[] _cornerupleft = {"/   |",
                                                "2   |",
                                                "    |",
                                                "   1|",
                                                "----/"};

        private static string[] _cornerdownright ={"/----",
                                                  "|1   ",
                                                  "|    ",
                                                  "|  2 ",
                                                  "|   /" };










        #endregion




        static int richting;
        ///noord = 0
        ///oost = 1
        ///zuid = 2
        ///west = 3


        public static void Initialize()
        {
            Console.Clear();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            drawTrack(Data.CurrentRace.Track);
        }

        public static void OnNextRaceEvent(object sender, NextRaceEventArgs e)
        {
            drawTrack(Data.CurrentRace.Track);
            Initialize();
        }

        private static void OnDriversChanged(object? o, Model.DriversChangedEventArgs args)
        {
            drawTrack(args.Track);
        }
        


        public static string[] DrawParticipant(IParticipant participant, string[] section)
        {
            string[] sectiontemp = new string[section.Length];

            for (int i = 0; i < section.Length; i++)
            {
                sectiontemp[i] = section[i];
            }
            for (int i = 0; i < section.Length; i++)
            {
                if (section[i].Contains("1"))
                {
                    sectiontemp[i] = section[i].Replace('1', participant.Name[0]);
                    if (participant.Equipment.IsBroken)
                    {
                        sectiontemp[i] = sectiontemp[i].Replace(participant.Name[0], '#');
                    }
                    return sectiontemp;
                } else 
                if (section[i].Contains("2"))
                {
                    sectiontemp[i] = section[i].Replace('2', participant.Name[0]);
                    if (participant.Equipment.IsBroken)
                    {
                        sectiontemp[i] = sectiontemp[i].Replace(participant.Name[0], '#');
                    }
                    return sectiontemp;
                }
            }
            return sectiontemp;
        }





        public static void drawSection(String[] type, int x, int y, Section section)
        {
            string[] sectiontemp = new string[type.Length];
            for (int i = 0; i < type.Length; i++)
            {
                sectiontemp[i] = type[i];
            }

            SectionData data = Data.CurrentRace.getSectionData(section);
            if (data.Left is not null)
            {
                sectiontemp = DrawParticipant(data.Left, sectiontemp);
            }  
            if (data.Right is not null) 
            {
                sectiontemp = DrawParticipant(data.Right, sectiontemp);
            } 
            
                for (int i = 0; i < type.Length; i++)
                {
                   sectiontemp[i] = sectiontemp[i].Replace('1', ' ');
                   sectiontemp[i] = sectiontemp[i].Replace('2', ' ');
                }
                
            
            foreach (string row in sectiontemp)
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
            //Console.Clear();
            
            int X = 50; //Console.CursorLeft;
            int Y = 50; //Console.CursorTop;
            richting = 1;


            foreach (Section section in track.Sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    drawSection(_startGridHorizontal, X, Y, section);
                    if (richting == 1)
                    {
                        X = X + 5;
                    }
                    else
                    {
                        X = X - 5;
                    }
                }


                if (section.SectionType == SectionTypes.Straigth)
                {
                    if (richting == 1 || richting == 3)
                    {
                        drawSection(_straigthHorizontal, X, Y, section);
                        if (richting == 1)
                        {
                            X = X + 5;
                        }
                        else
                        {
                            X = X - 5;
                        }
                    }
                    else if (richting == 2 || richting == 0)
                    {
                        drawSection(_straigthVertical, X, Y, section);
                        if (richting == 2)
                        {
                            Y = Y + 5;
                        }
                        else
                        {
                            Y = Y - 5;
                        }
                    }
                }

                if (section.SectionType == SectionTypes.Finish)
                {
                    if (richting == 1 || richting == 3)
                    {
                        drawSection(_finishHorizontal, X, Y, section);
                        if (richting == 1)
                        {
                            X = X + 5;
                        }
                        else
                        {
                            X = X - 5;
                        }
                    }
                    else if (richting == 2 || richting == 4)
                    {
                        drawSection(_finishVertical, X, Y, section);
                        if (richting == 2)
                        {
                            Y = Y + 5;
                        }
                        else
                        {
                            Y = Y - 5;
                        }
                    }
                }

                if (section.SectionType == SectionTypes.LeftCorner)
                {
                    if (richting == 0)
                    {
                        drawSection(_cornerdownleft, X, Y, section);
                        richting = 3;
                        X = X - 5;
                    }
                    else if (richting == 1)
                    {
                        drawSection(_cornerupleft, X, Y, section);
                        richting = 0;
                        Y = Y + -5;
                    }
                    else if (richting == 2)
                    {
                        drawSection(_cornerupright, X, Y, section);
                        richting = 1;
                        X = X + 5;
                    }
                    else if (richting == 3)
                    {
                        drawSection(_cornerdownright, X, Y, section);
                        richting = 2;
                        Y = Y + 5;
                    }
                }

                if (section.SectionType == SectionTypes.RightCorner)
                {
                    if (richting == 0)
                    {
                        drawSection(_cornerdownright, X, Y, section);
                        X = X + 5;
                        richting = 1;
                    }
                    else if (richting == 1)
                    {
                        drawSection(_cornerdownleft, X, Y, section);
                        Y = Y + 5;
                        richting = 2;
                    }
                    else if (richting == 2)
                    {
                        drawSection(_cornerupleft, X, Y, section);
                        X = X - 5;
                        richting = 3;
                    }
                    else if (richting == 3)
                    {
                        drawSection(_cornerupright, X, Y, section);
                        Y = Y - 5;
                        richting = 0;
                    }
                }
            }
        }
    }
}

