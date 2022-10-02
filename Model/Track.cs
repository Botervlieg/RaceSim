using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Model
{
    public class Track
    {
        public string Name{ get; set; }
        public LinkedList<Section> Sections { get; set; } 



        public LinkedList<Section> ConvertSections(SectionTypes[] sections) 
        {
            LinkedList<Section> result = new LinkedList<Section>();

            foreach (SectionTypes sectiontype in sections)
            {
                result.AddLast(new Section(sectiontype)); 
            }
            
            return result;

        }

        
        public Track(string name, SectionTypes[] sections) { 
            Name = name;
            Sections = ConvertSections(sections);
        }


    }
}
