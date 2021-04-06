using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using System;
using API.Domain.Entities;

namespace API.Controllers
{
    [Route("api/todo")]
    public class TodoController : Controller
    {

        [HttpGet]
        //[Route("api/todo/Listar")]
        public IActionResult ListarTarefa()
        {
            List<Tarefa> tarefas;
            using (var db = new LiteDatabase("banco.db"))
            {
                var taskCollection = db.GetCollection<Tarefa>("tarefas");

                tarefas = taskCollection.FindAll().ToList();
            }

            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddTaskRequest request)
        {
            var task = new Tarefa(request.Nome);

            using (var db = new LiteDatabase("banco.db"))
            {
                var taskCollection = db.GetCollection<Tarefa>("tarefas");
                taskCollection.Insert(task);
            }

            return Ok(new { Tarefa = task, Mensagem = "Operação realizada com sucesso!" });
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] UpdateTaskRequest request)
        {

            using (var db = new LiteDatabase("banco.db"))
            {
                var taskCollection = db.GetCollection<Tarefa>("tarefas");

                var taskUpdate = taskCollection.FindOne(x => x.Id == request.Id);

                taskUpdate.Nome = request.Nome;
                taskUpdate.Done = request.Done;

                taskCollection.Update(taskUpdate);
            }

            return Ok(new { Mensagem = "Operação realizada com sucesso!" });
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            using (var db = new LiteDatabase("banco.db"))
            {
                var taskCollection = db.GetCollection<Tarefa>("tarefas");

                taskCollection.Delete(id);
            }

            return Ok(new { Mensagem = "Operação realizada com sucesso!" });

        }
    }
}
