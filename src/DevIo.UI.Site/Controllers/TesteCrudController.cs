using DevIO.UI.Site.Data;
using DevIO.UI.Site.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.UI.Site.Controllers
{
    public class TesteCrudController : Controller
    {
        private readonly MeuDbContext _contexto;

        public TesteCrudController(MeuDbContext contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            var aluno = new Aluno
            {
                Nome = "Felipe",
                DataNascimento = DateTime.Now,
                Email = "felipemoreirasilva97@gmail.com"
            };

            //Metodo Create
            _contexto.Alunos.Add(aluno); //Guarda a alteração na memoria do MeuDbContext(DbContext)
            _contexto.SaveChanges(); //Salva no Banco o que esta em memoria em MeuDbContext(DbContext)

            //Metodos Read
            var aluno2 = _contexto.Alunos.Find(aluno.Id);
            var aluno3 = _contexto.Alunos.FirstOrDefault(a=> a.Email == "felipemoreirasilva97@gmail.com"); //O primeiro ou nenhum caso nao encontre nada
            var aluno4 = _contexto.Alunos.Where(a=> a.Nome == "Felipe"); //Todos os alunos com esse valor no nome

            aluno.Nome = "Amanda";

            //Metodo Update
            _contexto.Alunos.Update(aluno); //Guarda a alteração na memoria do MeuDbContext(DbContext)
            _contexto.SaveChanges(); //Salva no Banco o que esta em memoria em MeuDbContext(DbContext)

            //Metodo Delete
            _contexto.Alunos.Remove(aluno); //Esse metodo tem como parametro uma ENTIDADE, nao por Id, quando tiver apenas Id, é necessario usar o .Find e obter a entidade
            _contexto.SaveChanges(); //Salva no Banco o que esta em memoria em MeuDbContext(DbContext)



            return View();
        }
    }
}
