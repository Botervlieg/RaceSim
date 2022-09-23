using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    public class Race
    {
        public Track Track;
        public List<IParticipant> Participants;
        public DateTime StartTime;
        public Random _random;


        private Dictionary<Section, SectionData> _positions;

        public void getSectionData(Section section)
        {
            
        }


    }
}
