﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{




    public enum SectionTypes {

        Straigth,
        LeftCorner,
        RightCorner,
        StartGrid,
        Finish
    }
    internal class Section
    {
        public SectionTypes SectionType { get; set; }
    }
}