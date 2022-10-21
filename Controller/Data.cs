using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller 
{
    public static class Data 
    {
        public static Competition competition;

        public static Race CurrentRace;


        public static event EventHandler<NextRaceEventArgs> NextRaceEventHandler;



        public static void Initialize()
        {
            competition = new Competition();
            AddParticipants();
            AddTracks();
        }

        public static void AddParticipants()
        {
            Driver driver1 = new Driver("piet", 0, new Car(5, 5, 10, false), TeamColors.Red);
            Driver driver2 = new Driver("henk", 1, new Car(4, 2, 9, false), TeamColors.Blue);
            Driver driver3 = new Driver("jan", 0, new Car(3, 4, 6, true), TeamColors.Green);
            Driver driver4 = new Driver("willem ", 1, new Car(5, 3, 5, false), TeamColors.Yellow);
            
            competition.Participants.Add(driver1);
            competition.Participants.Add(driver2);
            competition.Participants.Add(driver3);
            competition.Participants.Add(driver4);
        }

        public static void AddTracks()
        {
            Track track1 = new Track("track1", new SectionTypes[] 
            {SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish ,SectionTypes.RightCorner, SectionTypes.RightCorner,SectionTypes.LeftCorner,
             SectionTypes.LeftCorner, SectionTypes.Straigth, SectionTypes.LeftCorner, SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.LeftCorner,
             SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.Straigth,
             SectionTypes.LeftCorner,  SectionTypes.Straigth, SectionTypes.LeftCorner, SectionTypes.Straigth});

            Track track2 = new Track("track2", new SectionTypes[]
            {SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.Straigth, SectionTypes.RightCorner, SectionTypes.Straigth,
             SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.RightCorner, SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.Straigth,
             SectionTypes.Straigth, SectionTypes.RightCorner, SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.RightCorner});

            Track track3 = new Track("track3", new SectionTypes[] 
            {SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straigth,
             SectionTypes.Straigth, SectionTypes.Straigth, SectionTypes.RightCorner, SectionTypes.RightCorner});



            competition.Tracks.Enqueue(track1);
            competition.Tracks.Enqueue(track2);
            competition.Tracks.Enqueue(track3);
        }
        


        public static void NextRace()
        {
            if (CurrentRace is not null) { 
            CurrentRace.cleanup();
        }
            Track? track = competition.NextTrack();

            if(track is not null)
            {
                CurrentRace = new Race(track, competition.Participants);
                CurrentRace.RaceFinished += OnRaceFinished;
                NextRaceEventHandler?.Invoke(null, new NextRaceEventArgs() { Race = CurrentRace });
                CurrentRace.startRace();
            } 
        }

        public static void OnRaceFinished(object o, EventArgs e)
        {
            NextRace();
        }

        
    }
}
