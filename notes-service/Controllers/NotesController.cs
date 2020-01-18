using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using notes_service.Models;
using notes_service.Services;

namespace notes_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var allNotes = _noteService.GetAll();
            var response = new Success<List<Note>>()
            {
                Message = "Notes fetched",
                Data = allNotes.ToList(),
                Code = 200
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id?}")]
        public async Task<IActionResult> Get(int? id)
        {
            if(id == null)
            {
                return BadRequest(new Error()
                {
                    Message = "Note id cannot be null",
                    Code = 400
                });
            }

            try
            {
                Note note = await _noteService.Get(id);
                var response = new Success<Note>()
                {
                    Message = "Note fetched",
                    Data = note,
                    Code = 200
                };
                return Ok(response);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new Error()
                {
                    Message = ex.Message,
                    Code = 400
                });
            }
            catch
            {
                return StatusCode(500, new Error()
                {
                    Message = "Something went wrong",
                    Code = 500
                });
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Add(Note note)
        {
            try
            {
                Note newNote = await _noteService.Add(note);
                var response = new Success<Note>()
                {
                    Message = "Note created",
                    Data = note,
                    Code = 200
                };
                return Ok(response);
            }            
            catch
            {
                return StatusCode(500, new Error()
                {
                    Message = "Something went wrong",
                    Code = 500
                });
            }
        }

        [HttpPut]
        [Route("{id?}")]
        public async Task<IActionResult> Update([FromRoute]int? id, Note note)
        {
            if(id == null)
            {
                return BadRequest(new Error()
                {
                    Message = "Note id cannot be null",
                    Code = 400
                });
            }
            if(id != note.Id)
            {
                return BadRequest(new Error()
                {
                    Message = "Note id is not valid",
                    Code = 400
                });
            }
            try
            {
                Note newNote = await _noteService.Update(note);
                var response = new Success<Note>()
                {
                    Message = "Note updated",
                    Data = note,
                    Code = 200
                };
                return Ok(response);
            }
            catch
            {
                return StatusCode(500, new Error()
                {
                    Message = "Something went wrong",
                    Code = 500
                });
            }
        }

        [HttpDelete]
        [Route("{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest(new Error()
                {
                    Message = "Note id cannot be null",
                    Code = 400
                });
            }

            try
            {
                int i = await _noteService.Delete(id);
                var response = new Success<int?>()
                {
                    Message = "Note deleted",
                    Code = 200
                };
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new Error()
                {
                    Message = ex.Message,
                    Code = 400
                });
            }
            catch
            {
                return StatusCode(500, new Error()
                {
                    Message = "Something went wrong",
                    Code = 500
                });
            }
        }
    }
}