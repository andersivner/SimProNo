using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SimProNo.Controllers;
using Moq;
using SimProNo.Models;
using SimProNo;
using System.Data.Entity;
using System.Web.Mvc;

namespace ClassLibrary1
{
    [TestFixture]
    public class Class1
    {
        private Mock<NoteContext> db;

        [SetUp]
        public void Setup()
        {
            db = new Mock<NoteContext>();
        }

        [Test]
        public void Given_OneNote_When_ViewingIndex_Then_OneNoteIsDisplayed()
        {
            db.Setup(x => x.Notes).Returns(new MockDbSet<Note>()
                { new Note()
                });
            var noteController = new NoteController(db.Object);

            var viewResult = (ViewResult)noteController.Index();

            var model = (IEnumerable<Note>)viewResult.Model;
            Assert.AreEqual(1, model.Count());
        }

        [Test]
        public void Given_TwoNotes_When_ViewingIndex_Then_NotesAreOrderedByDateDescending()
        {
            db.Setup(x => x.Notes).Returns(new MockDbSet<Note>()
                { new Note() { CreateDate = new DateTime(2011, 01, 01) },
                  new Note() { CreateDate = new DateTime(2011, 02, 02) }
                });
            var noteController = new NoteController(db.Object);

            var viewResult = (ViewResult)noteController.Index();

            var model = (IEnumerable<Note>)viewResult.Model;
            Assert.AreEqual(2, model.Count());
            Assert.AreEqual(new DateTime(2011, 02, 02), model.First().CreateDate);
            Assert.AreEqual(new DateTime(2011, 01, 01), model.ElementAt(1).CreateDate);
        }

        [Test]
        public void Given_NoNotes_When_CreatingANote_Then_ThatNoteIsDiplayed()
        {
            db.Setup(x => x.Notes).Returns(new MockDbSet<Note>()
            {
            });
            var noteController = new NoteController(db.Object);

            var viewResult = (ViewResult)noteController.Index("test note");

            var model = (IEnumerable<Note>)viewResult.Model;
            Assert.AreEqual(1, model.Count());
            Assert.AreEqual("test note", model.First().Text);
        }
        
        /*
        [Test]
        public void Given__When__Then_()
        {
            db.Setup(x => x.Notes).Returns(new MockDbSet<Note>()
                { 
                });
            var noteController = new NoteController(db.Object);

            var viewResult = (ViewResult)noteController.Index();

            var model = (IEnumerable<Note>)viewResult.Model;

        }
        */
    }
}
