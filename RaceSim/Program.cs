using Model;
using Controller;
using RaceSim;

Data.Initialize();
Data.NextRace();
visualisatie.Initialize();
visualisatie.drawTrack(Data.CurrentRace.Track);





for (; ; )
{
    Thread.Sleep(100);
}