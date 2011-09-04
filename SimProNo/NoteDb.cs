using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SimProNo.Models;

namespace SimProNo
{
    public class NoteDb: DbContext, NoteContext
    {
        public NoteDb()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public IDbSet<Note> Notes { get; set; }
    }
}