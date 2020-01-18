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
        public async Task<Note> Add(Note note)
        {
            Note newNote = _context.Add(note).Entity;
            await _context.SaveChangesAsync();
            return newNote;
        }
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

        public IQueryable<Note> GetAll()
        {
            return _context.Notes.AsQueryable<Note>().OrderBy(a=>a.IsCompleted);
        }

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
        
        public async Task<Note> Update(Note note)
        {
            _context.Entry<Note>(note).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return note;
        }
    }
}
