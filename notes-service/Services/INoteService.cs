using notes_service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notes_service.Services
{
    public interface INoteService
    {
        Task<Note> Add(Note note);
        Task<Note> Get(int? id);
        IQueryable<Note> GetAll();
        Task<Note> Update(Note note);
        Task<int> Delete(int? id);
    }
}
