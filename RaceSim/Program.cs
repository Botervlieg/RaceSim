using Model;
using Controller;
using RaceSim;
using System.Reflection;

Data.Initialize();
Data.NextRaceEventHandler += visualisatie.OnNextRaceEvent;
Data.NextRace();


//visualisatie.drawTrack(Data.CurrentRace.Track);
//Data.CurrentRace.startRace();





//debuggen zonder timer
/*
while (true)
{
    visualisatie.drawTrack(Data.CurrentRace.Track);
    Data.CurrentRace.Drive();
}
*/




for (; ; )
{
    Thread.Sleep(100);
}