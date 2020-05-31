using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BlazorTodoApp.Server.Models;
using BlazorTodoApp.Server.Repositories;
using BlazorTodoApp.Shared.Models;

namespace BlazorTodoApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodosController(
            ITodoRepository todoRepository,
            IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoDto>> Get(bool? done)
        {
            var items = _todoRepository.GetAll(done);
            return Ok(items.Select(x => _mapper.Map<TodoDto>(x)));
        }

        [HttpGet]
        [Route("{id}", Name = nameof(GetSingle))]
        public ActionResult<TodoDto> GetSingle(Guid id)
        {
            TodoEntity entity = _todoRepository.GetSingle(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TodoDto>(entity));
        }

        [HttpPost(Name = nameof(AddTodo))]
        public ActionResult AddTodo([FromBody] TodoCreateDto todoCreateDto)
        {
            if (todoCreateDto == null)
            {
                return BadRequest();
            }

            TodoEntity item = _mapper.Map<TodoEntity>(todoCreateDto);
            item.Created = DateTime.UtcNow;
            TodoEntity newTodoEntity = _todoRepository.Add(item);

            if (!_todoRepository.Save())
            {
                throw new Exception("Adding an item failed on save.");
            }


            return CreatedAtRoute(
                nameof(GetSingle),
                new { id = newTodoEntity.Id },
                _mapper.Map<TodoDto>(newTodoEntity));
        }

        [HttpPut]
        [Route("{id}", Name = nameof(UpdateTodo))]
        public ActionResult<TodoDto> UpdateTodo(Guid id, [FromBody] TodoUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest();
            }

            TodoEntity singleById = _todoRepository.GetSingle(id);

            if (singleById == null)
            {
                return NotFound();
            }

            _mapper.Map(updateDto, singleById);

            TodoEntity updatedTodo = _todoRepository.Update(id, singleById);

            if (!_todoRepository.Save())
            {
                throw new Exception("Updating an item failed on save.");
            }

            var updatedDto = _mapper.Map<TodoDto>(updatedTodo);


            return Ok(updatedDto);
        }

        [HttpDelete]
        [Route("{id}", Name = nameof(DeleteTodo))]
        public ActionResult DeleteTodo(Guid id)
        {
            TodoEntity singleById = _todoRepository.GetSingle(id);

            if (singleById == null)
            {
                return NotFound();
            }

            _todoRepository.Delete(singleById);

            if (!_todoRepository.Save())
            {
                throw new Exception("Deleting an item failed on save.");
            }


            return NoContent();
        }
    }
}