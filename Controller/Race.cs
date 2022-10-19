﻿using System;
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
                driver.Equipment.Performance = _random.Next(1,5);
                driver.Equipment.Quality = _random.Next(1, 5);
            }
        }

        public void start()
        {
            {
                timer.Enabled = true;
            }
        }

        public event EventHandler DriversChanged;

        

        private void OnTimedEvent(object o, ElapsedEventArgs e)
        {
            
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
}
