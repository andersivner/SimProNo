using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimProNo.Models;

namespace SimProNo.ViewModels
{
    public class DateNoteGroup
    {
        public DateTime Date {get; set;}
        public IEnumerable<Note> Notes { get; set; }
    }
}