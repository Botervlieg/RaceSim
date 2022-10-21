using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Timers;

namespace Controller
{
    public class Race
    {
        public Track Track;
        public List<IParticipant> Participants;
        public DateTime StartTime;
        private Random _random;
        private System.Timers.Timer timer;



        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();


        public void setstartpositie(Track track, List<IParticipant> participants)
        {
            foreach (IParticipant participant in participants)
            {
                foreach (Section section in track.Sections)
                {
                    if (section.SectionType == SectionTypes.StartGrid)
                    {
                        SectionData data = getSectionData(section);
                        if (data.Left is null)
                        {
                            _positions[section].Left = participant;
                            break;
                        }
                        else if (data.Right is null)
                        {
                            _positions[section].Right = participant;
                            break;
                        }
                    }
                }
            }
        }




        public SectionData getSectionData(Section section)
        {

            if (!_positions.ContainsKey(section)) //kijkt of _positions de key section heeft
            {
                _positions.Add(section, new SectionData()); //als de key er niet instaat maakt hij een nieuwe sectiondata aan met section als key
            }
            return _positions[section];  //return de bijbehorende waarde van de key section
        }



        public void RandomizeEquipment()
        {
            foreach (Driver driver in Data.competition.Participants)
            {
                driver.Equipment.Performance = _random.Next(1, 6);
                driver.Equipment.Quality = _random.Next(1, 6);
                driver.Equipment.Performance = _random.Next(5, 11);
            }
        }

        public void startRace()
        {
            timer.Start();
        }

        public void stopRace()
        {
            timer.Stop();
            Data.NextRace();
            
        }

        public event EventHandler<DriversChangedEventArgs> DriversChanged;

        public EventHandler RaceFinished;




        private void OnTimedEvent(object o, ElapsedEventArgs e)
        {
            Drive();
            DriversChanged.Invoke(this, new DriversChangedEventArgs() { Track = this.Track });
            if (CheckIfRaceDone()){
                RaceFinished?.Invoke(this, new EventArgs());
            }
        }


        
        public void Drive()
        {
            LinkedListNode<Section>? currentSectionNode = Track.Sections.Last;
            LinkedListNode<Section>? prevSectionNode;
            while (currentSectionNode != null)
            {
                SectionData currentsectiondata = getSectionData(currentSectionNode.Value);
                SectionData prevsectiondata;
                if (currentSectionNode.Previous is not null)
                {
                    prevSectionNode = currentSectionNode.Previous;
                    prevsectiondata = getSectionData(prevSectionNode.Value);
                } else
                {
                    prevSectionNode = Track.Sections.Last;
                    prevsectiondata = getSectionData(prevSectionNode.Value);
                }
                    if (currentsectiondata.Left is null)
                    {
                        if (prevsectiondata.Left is not null)
                        {
                            prevsectiondata.Left.Location = prevsectiondata.Left.Location + prevsectiondata.Left.Equipment.Performance * prevsectiondata.Left.Equipment.Speed;
                            if (prevsectiondata.Left.Location >= 50)
                            {
                                prevsectiondata.Left.Location = prevsectiondata.Left.Location - 50;
                                _positions[currentSectionNode.Value].Left = prevsectiondata.Left;
                                _positions[prevSectionNode.Value].Left = null;
                            if (currentSectionNode.Value.SectionType == SectionTypes.Finish)
                            {
                                _positions[currentSectionNode.Value].Left.Ronde++;
                                if (_positions[currentSectionNode.Value].Left.Ronde == 3)
                                {
                                    _positions[currentSectionNode.Value].Left = null;
                                    //Finish();
                                }
                            }

                        }
                        }
                        if (prevsectiondata.Right is not null && currentsectiondata.Left is null)
                        {
                            prevsectiondata.Right.Location = prevsectiondata.Right.Location + prevsectiondata.Right.Equipment.Performance * prevsectiondata.Right.Equipment.Speed;
                            if (prevsectiondata.Right.Location >= 50)
                            {
                                prevsectiondata.Right.Location = prevsectiondata.Right.Location - 50;
                                _positions[currentSectionNode.Value].Left = prevsectiondata.Right;
                                _positions[prevSectionNode.Value].Right = null;
                            if (currentSectionNode.Value.SectionType == SectionTypes.Finish)
                            {
                                _positions[currentSectionNode.Value].Left.Ronde++;
                                if (_positions[currentSectionNode.Value].Left.Ronde == 3)
                                {
                                    _positions[currentSectionNode.Value].Left = null;
                                    //Finish();
                                }
                            }
                        }
                        }
                    }

                    if (currentsectiondata.Right is null)
                    {
                        if (prevsectiondata.Left is not null)
                        {
                            prevsectiondata.Left.Location = prevsectiondata.Left.Location + prevsectiondata.Left.Equipment.Performance * prevsectiondata.Left.Equipment.Speed;
                            if (prevsectiondata.Left.Location >= 50)
                            {
                                prevsectiondata.Left.Location = prevsectiondata.Left.Location - 50;
                                _positions[currentSectionNode.Value].Right = prevsectiondata.Left;
                                _positions[prevSectionNode.Value].Left = null;
                            if (currentSectionNode.Value.SectionType == SectionTypes.Finish)
                            {
                                _positions[currentSectionNode.Value].Right.Ronde++;
                                if (_positions[currentSectionNode.Value].Right.Ronde == 3)
                                {
                                    _positions[currentSectionNode.Value].Right = null;
                                    //Finish();
                                }
                            }
                        }
                        }
                        else if (prevsectiondata.Right is not null && currentsectiondata.Right is null)
                        {
                            prevsectiondata.Right.Location = prevsectiondata.Right.Location + prevsectiondata.Right.Equipment.Performance * prevsectiondata.Right.Equipment.Speed;
                            if (prevsectiondata.Right.Location >= 50)
                            {
                                prevsectiondata.Right.Location = prevsectiondata.Right.Location - 50;
                                _positions[currentSectionNode.Value].Right = prevsectiondata.Right;
                                _positions[prevSectionNode.Value].Right = null;
                            if (currentSectionNode.Value.SectionType == SectionTypes.Finish)
                            {
                                _positions[currentSectionNode.Value].Right.Ronde++;
                                if (_positions[currentSectionNode.Value].Right.Ronde == 3)
                                {
                                    _positions[currentSectionNode.Value].Right = null;
                                    //Finish();
                                }
                            }
                        }
                        }
                    }
                currentSectionNode = currentSectionNode.Previous;
            }
        }



        public void Finish()
        {
            {
                int finished = 0;
                foreach (Driver driver in Data.competition.Participants)
                {
                    if (driver.Ronde == 3)
                    {
                        finished++;
                    }
                }
                if (CheckIfRaceDone())
                {
                    stopRace();
                }
            }
        }

        public bool CheckIfRaceDone()
        {
            int finished = 0;
            foreach (Driver driver in Data.competition.Participants)
            {
                if(driver.Ronde == 3)
                {
                    finished++;
                }
            }
            if (finished == Data.competition.Participants.Count)
            {
                foreach (Driver driver in Data.competition.Participants)
                {
                    driver.Ronde = 0;
                }
                return true;
            }
            return false;
        }


        public void cleanup()
        {
            DriversChanged = null;
            RaceFinished = null;
        }
    


        
    



        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            setstartpositie(track, participants);
            timer = new System.Timers.Timer(500);
            timer.Elapsed += OnTimedEvent;
            RandomizeEquipment();
            
            
        }

        
    }
    public class NextRaceEventArgs : EventArgs
    {
        public Race Race { get; set; }

    }
}
