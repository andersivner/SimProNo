using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SimProNo.Models
{

    public enum NoteType {Question = 1, Answer = 2, Task = 3, Status = 4}


    public class Note
    {
        public virtual int Id { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual int NoteType { get; set; }
        [NotMapped]
        public NoteType NoteTypeEnum
        {
            get
            {
                if (NoteType < 1 || NoteType > 4)
                    return SimProNo.Models.NoteType.Status;
                return (NoteType)NoteType;
            }
            set { NoteType = (int)value; }
        }
        public virtual DateTime? CompletedDate { get; set; }
        public virtual int? ParentId { get; set; }

        public virtual Note Parent { get; set; }
        [InverseProperty("Parent")]
        public virtual ICollection<Note> Subnotes { get; set; }

        public void Completed()
        {
            CompletedDate = DateTime.Now;
        }

        public void SetText(string text)
        {
            Text = text;

            var noteType = SimProNo.Models.NoteType.Status;
            if (text.EndsWith("!"))
                noteType = SimProNo.Models.NoteType.Task;

            NoteTypeEnum = noteType;
        }
    }
}