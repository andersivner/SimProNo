using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects;
using System.Data.Entity;
using SimProNo.Models;
using System.Configuration;
using SimProNo.ViewModels;

namespace SimProNo.Controllers
{
    public class NoteController : Controller
    {

        private NoteContext context;

        public NoteController(NoteContext context)
        {
            this.context = context;
        }

        //
        // GET: /Note/

        public ActionResult Index()
        {
            var notes = context.Notes.OrderByDescending(x => x.CreateDate).Take(100).ToList();

            var notegroups = notes.GroupBy(x => x.CreateDate.Date)
                                .Select(x => new DateNoteGroup()
                                {
                                    Date = x.Key,
                                    Notes = x.OrderByDescending(n => n.CreateDate)
                                });

            return View(notegroups);
        }

        [HttpPost]
        public ActionResult Index(string text)
        {
            var noteType = NoteType.Status;
            if (text.EndsWith("!"))
                noteType = NoteType.Task;

            Note note = context.Notes.Add(new Models.Note() { CreateDate = DateTime.Now, Text = text, NoteTypeEnum = noteType });
            context.SaveChanges();

            ModelState.Clear();

            return Index();
        }

        public ActionResult ByParent(int id)
        {
            return View(context.Notes.Include(x => x.Subnotes).Where(x => x.Id == id).FirstOrDefault()); 
        }

        [HttpPost]
        public ActionResult ByParent(int id, string text)
        {
            var noteType = NoteType.Status;
            if (text.EndsWith("!"))
                noteType = NoteType.Task;

            context.Notes.Add(new Models.Note() { CreateDate = DateTime.Now, Text = text, ParentId = id, NoteTypeEnum = noteType });
            context.SaveChanges();

            return ByParent(id);
        }

        [HttpPost]
        public ActionResult Completed(int id)
        {
            var note = context.Notes.Find(id);
            note.Completed();
            context.SaveChanges();
            return PartialView("Note", note);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Edit(int id)
        {
            var note = context.Notes.Find(id);

            return PartialView(note);
        }

        [HttpPost]
        public ActionResult Edit(int id, string text)
        {
            var note = context.Notes.Find(id);
            note.Text = text;
            context.SaveChanges();

            return PartialView("Note", note);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Note(int id)
        {
            var note = context.Notes.Find(id);

            return PartialView(note);
        }
    }
}
