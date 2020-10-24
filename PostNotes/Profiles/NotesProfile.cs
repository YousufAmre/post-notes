using AutoMapper;
using PostNotes.DTOs;
using PostNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostNotes.Profiles
{
    public class NotesProfile:Profile
    {
        public NotesProfile()
        {
            //Source -> Target
            CreateMap<Notes, NotesReadDTO>();
            CreateMap<NoteCreateDTO, Notes>();
        }
    }
}
