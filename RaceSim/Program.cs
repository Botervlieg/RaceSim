using Model;
using Controller;
using RaceSim;
using System.Reflection;

Data.Initialize();
Data.NextRace();
visualisatie.Initialize();
Data.CurrentRace.start();



//debuggen zonder timer

/*visualisatie.drawTrack(Data.CurrentRace.Track);
Data.CurrentRace.Drive();
visualisatie.drawTrack(Data.CurrentRace.Track);
Data.CurrentRace.Drive();
visualisatie.drawTrack(Data.CurrentRace.Track);
Data.CurrentRace.Drive();
visualisatie.drawTrack(Data.CurrentRace.Track);
Data.CurrentRace.Drive();
visualisatie.drawTrack(Data.CurrentRace.Track);
Data.CurrentRace.Drive();
visualisatie.drawTrack(Data.CurrentRace.Track);
Data.CurrentRace.Drive();
visualisatie.drawTrack(Data.CurrentRace.Track);
Data.CurrentRace.Drive();
visualisatie.drawTrack(Data.CurrentRace.Track);
Data.CurrentRace.Drive();
visualisatie.drawTrack(Data.CurrentRace.Track);
*/



for (; ; )
{
    Thread.Sleep(100);
}