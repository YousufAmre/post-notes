using PostNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostNotes.Repository.Interface
{
    public interface INotesRepository
    {
        bool SaveChanges();

        IEnumerable<Notes> GetNotes();
        Notes GetNoteById(int id);
        void CreateNote(Notes note);
        void UpdateNote(int id,Notes note);
        void DeleteNote(Notes note);
    }
}
