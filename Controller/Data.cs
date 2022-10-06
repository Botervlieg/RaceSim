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
        public static Competition competitie = default!;

        public static Race CurrentRace = default!; 
        

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
            Track track1 = new Track("track1", new SectionTypes[] 
            {SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Straigth ,SectionTypes.RightCorner, SectionTypes.RightCorner,SectionTypes.LeftCorner,
             SectionTypes.LeftCorner, SectionTypes.Straigth, SectionTypes.LeftCorner, SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.LeftCorner,
             SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.Straigth,
             SectionTypes.LeftCorner,  SectionTypes.Straigth, SectionTypes.LeftCorner, SectionTypes.Finish});

            Track track2 = new Track("track2", new SectionTypes[]
            {SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straigth, SectionTypes.RightCorner, SectionTypes.Straigth,
            SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.RightCorner, SectionTypes.Straigth, SectionTypes.RightCorner, SectionTypes.Finish});
            competitie.Tracks.Enqueue(track1);
            competitie.Tracks.Enqueue(track2);
        }

        public static void NextRace()
        {
            Track? track = competitie.NextTrack();

            if(track is not null)
            {
                CurrentRace = new Race(track, competitie.Participants);
            }

        }





    }
}
