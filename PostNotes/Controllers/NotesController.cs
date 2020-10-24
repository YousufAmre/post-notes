using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostNotes.DTOs;
using PostNotes.Models;
using PostNotes.Repository.Interface;

namespace PostNotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INotesRepository _notesRepository;
        private readonly IMapper _mapper;

        public NotesController(INotesRepository repository, IMapper mapper)
        {
            _notesRepository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<NotesReadDTO>> GetAllNotes()
        {
            var notes = _notesRepository.GetNotes();
            return Ok(_mapper.Map<IEnumerable<NotesReadDTO>>(notes));
        }

        [HttpGet("{id}", Name = "GetNoteById")]
        public ActionResult<NotesReadDTO> GetNoteById(int id)
        {
            var note = _notesRepository.GetNoteById(id);
            if (note != null)
            {
                return Ok(_mapper.Map<NotesReadDTO>(note));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<NotesReadDTO> CreateNote([FromBody] NoteCreateDTO noteCreateDTO)
        {
            var noteModel = _mapper.Map<Notes>(noteCreateDTO);
            noteModel.PostedOn = DateTime.Now;
            _notesRepository.CreateNote(noteModel);
            _notesRepository.SaveChanges();

            var noteReadDTO = _mapper.Map<NotesReadDTO>(noteModel);

            return CreatedAtRoute(nameof(GetNoteById), new { Id = noteReadDTO.Id }, noteReadDTO);
        }

        [HttpPut("{id}")]
        public ActionResult<bool> UpdateNote(int id, [FromBody] NoteCreateDTO noteUpdate)
        {             
            if (_notesRepository.GetNoteById(id) == null)
                return NotFound();
            var noteModel = _mapper.Map<Notes>(noteUpdate);
            _notesRepository.UpdateNote(id, noteModel);
            return _notesRepository.SaveChanges();
        }

        [HttpDelete("{id}")]
        public ActionResult<Notes> DeleteNote(int id)
        {
            var note = _notesRepository.GetNoteById(id);
            if (note == null)
                return NotFound();
            _notesRepository.DeleteNote(note);
            _notesRepository.SaveChanges();
            return note;
        }
    }
}
