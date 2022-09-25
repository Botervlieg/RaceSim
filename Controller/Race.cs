using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ExchangeAPI.Data;
using Model;

namespace Controller
{
    public class Race
    {
        public Track Track;
        public List<IParticipant> Participants;
        public DateTime StartTime;
        private Random _random;


        private Dictionary<Section, SectionData> _positions;



        public SectionData getSectionData(Section section)
        {

            if (!_positions.ContainsKey(section)) //kijkt of _positions de key section heeft
            {
                _positions.Add(section, new SectionData()); //als de key er niet instaat maakt hij een nieuwe sectiondata aan met section als key
            } 
                return _positions[section]; //return de bijbehorende waarde van de key section
        }



        public void RandomizeEquipment()
        {
            foreach (Driver i in Data.competitie.Participants)
            {
                _random = new Random(DateTime.Now.Millisecond);
                i.Equipment.Performance = _random.Next(1,5);
                i.Equipment.Quality = _random.Next(1, 5);
            }

            

        }


        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
        }


    }
}
