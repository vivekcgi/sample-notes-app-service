using notes_service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notes_service.Services
{
    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext _context;

        public NoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        // add a note to the database
        public async Task<Note> Add(Note note)
        {
            Note newNote = _context.Add(note).Entity;
            await _context.SaveChangesAsync();
            return newNote;
        }

        // get a note from a database by id
        public async Task<Note> Get(int? id)
        {
            Note note = await _context.Notes.FindAsync(id);
            if(note == null)
            {
                throw new KeyNotFoundException("Note with id not found.");
            }
            else
            {
                return note;
            }            
        }

        // get all the notes from the databse
        public IQueryable<Note> GetAll()
        {
            return _context.Notes.AsQueryable<Note>().OrderBy(a=>a.IsCompleted);
        }

        // delete a specific note from a database by id
        public async Task<int> Delete(int? id)
        {
            Note note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                throw new KeyNotFoundException("Note with id not found.");
            }
            else
            {
                _context.Entry<Note>(note).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                int i = await _context.SaveChangesAsync();
                return i;
            }
        }
        
        // update a note into the databse
        public async Task<Note> Update(Note note)
        {
            _context.Entry<Note>(note).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return note;
        }
    }
}
