using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SimProNo.Models;

namespace SimProNo
{
    public interface NoteContext 
    {
        IDbSet<Note> Notes { get; set; }
        int SaveChanges();
    }
}