using PostNotes.Models;
using PostNotes.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostNotes.Repository
{
    public class NoteRepository : INotesRepository
    {
        private NotesContext _context;

        public NoteRepository(NotesContext context)
        {
            _context = context;
        }

        public Notes GetNoteById(int id)
        {
            return _context.Notes.FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Notes> GetNotes()
        {
            return _context.Notes.ToList();
        }

        public void CreateNote(Notes note)
        {            
            if(note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }
            _context.Notes.Add(note);
        }           

        public bool SaveChanges()
        {
            if (_context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteNote(Notes note)
        {
            _context.Notes.Remove(note);
        }

        public void UpdateNote(int id, Notes note)
        {
            var noteToUpdate = _context.Notes.Find(id);
            noteToUpdate.Title = note.Title;
            noteToUpdate.Description = note.Description;
        }
    }
}
