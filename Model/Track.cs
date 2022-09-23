using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Track
    {
        public string Name{ get; set; }
        LinkedList<Section> Sections = new LinkedList<Section>();
        
        public Track(string name, SectionTypes[] sections) { 
            Name = name;
            foreach (SectionTypes i in sections)
            {
                Sections.AddLast(new Section(i));
            }

            
            //SectionTypes to LinkedList help
        }
    }
}
