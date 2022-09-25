using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller 
{
    public static class Data 
    {
        public static Competition competitie { get; set; }

        public static Race CurrentRace; 
        

        public static void Initialize()
        {
            competitie = new Competition();
            AddParticipants();
            AddTracks();
        }

        public static void AddParticipants()
        {
            Driver driver1 = new Driver("piet", 0, new Car(0, 0, 120, false), TeamColors.Red);
            Driver driver2 = new Driver("henk", 1, new Car(1, 2, 100, false), TeamColors.Blue);
            Driver driver3 = new Driver("jan", 0, new Car(3, 5, 110, true), TeamColors.Green);
            competitie.Participants.Add(driver1);
            competitie.Participants.Add(driver2);
            competitie.Participants.Add(driver3);
        }

        public static void AddTracks()
        {
            Track track1 = new Track("road", new SectionTypes[] 
            {SectionTypes.StartGrid, SectionTypes.LeftCorner, SectionTypes.Straigth, SectionTypes.Finish});

            Track track2 = new Track("weg", new SectionTypes[]
            {SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Finish});
            competitie.Tracks.Enqueue(track1);
            competitie.Tracks.Enqueue(track2);
        }

        public static void NextRace()
        {
            Track track = competitie.NextTrack();

            if(track is not null)
            {
                CurrentRace = new Race(track, competitie.Participants);
            }

        }





    }
}
