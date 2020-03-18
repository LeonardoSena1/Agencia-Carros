using CadastroDeAgencias.Context;
using CadastroDeAgencias.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CadastroDeAgencias.Controllers
{
    public class CarrosController : Controller
    {
        CarrosDbContext db;
        public CarrosController()
        {
            db = new CarrosDbContext();
        }





        // GET: Carros
        public ActionResult Index(int? pagina, string sortOrder, string searchString, int du = 1)
        {          
            var age = new Agencia();
            age.Carros = db.Carros.ToList();

            //Ordenação
            ViewBag.Agencia = String.IsNullOrEmpty(sortOrder) ? "Nome_Desc" : "";
            var agencia = from s in db.Agencias.ToList()
                          select s;
            switch (sortOrder)
            {
                case "Nome_Desc":
                    agencia = agencia.OrderByDescending(s => s.Nome);
                    break;
                default:
                    agencia = agencia.OrderBy(s => s.Nome);
                    break;
            }

            //Pesquisa por Nome da agencia
            if (!String.IsNullOrEmpty(searchString))
            {
                agencia = agencia.Where(s => s.Nome.ToUpper().Contains(searchString.ToUpper())
                                          || s.Nome.ToUpper().Contains(searchString.ToUpper()));

            }                       

            int paginaTamanho = 10;
            int NumeroPagina = pagina ?? 1;

            //Contador de pagina
            ViewBag.d = pagina == 1 ? ViewBag.d = pagina : ViewBag.d = du;

            return View(agencia.ToPagedList(NumeroPagina, paginaTamanho));
        }

        public ActionResult Create()
        {
            ViewBag.Agencia = db.Agencias;
            var model = new Carro();
            return View(model);
        }

        // POST Carros
        [HttpPost]
        public ActionResult Create(Carro model)
        {
            var carro = new Carro();
            carro.Modelo = model.Modelo;
            carro.Fabricante = model.Fabricante;
            carro.Placa = model.Placa;
            carro.Cor = model.Cor;
            carro.AgenciaId = model.AgenciaId;

            db.Carros.Add(carro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Carros/Edit/5     
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro t = db.Carros.Find(id);
            if (t == null)
            {
                return HttpNotFound();
            }
            ViewBag.Agencia = db.Agencias;
            return View(t);
        }

        // POST Carros
        [HttpPost]
        public ActionResult Edit([Bind(Include = "CarroId,Modelo,Fabricante,Placa,Cor,")] Carro model)
        {
            if (ModelState.IsValid)
            {
                var carro = db.Carros.Find(model.CarroId);
                if (carro == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                carro.Modelo = model.Modelo;
                carro.Fabricante = model.Fabricante;
                carro.Placa = model.Placa;
                carro.Cor = model.Cor;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Agencia = db.Agencias;
            return View(model);
        }

        // GET: Carros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Carro car = db.Carros.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.Agencia = db.Agencias.Find(car.AgenciaId).AgenciaId;
            return View(car);
        }
        // POST: carros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carro car = db.Carros.Find(id);
            db.Carros.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Carros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro car = db.Carros.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.Agencia = db.Agencias.Find(car.AgenciaId).AgenciaId;
            return View(car);
        }



        // Agencias----------------------------------------------------------------

        // GET: Agencias/Create
        public ActionResult AgenciaCreate()
        {
            ViewBag.Agencia = db.Agencias;
            var model = new Agencia();
            return View(model);
        }

        [HttpPost]
        public ActionResult AgenciaCreate(Agencia model)
        {
            var agencia = new Agencia();
            agencia.Nome = model.Nome;
            agencia.Endereco = model.Endereco;
            agencia.HorarioAberto = model.HorarioAberto;
            agencia.HorarioFechado = model.HorarioFechado;         
                       
            db.Agencias.Add(agencia);
            db.SaveChanges();
            return RedirectToAction("Index");            
        }
        
        //lista de carro para não dar erro ao criar agencia no inicio
        public ActionResult ListCarro()
        {
            List<Carro> lista = new List<Carro>();
            Carro car = new Carro();
            car.Modelo = "Kadett";
            car.Fabricante = "Fabricante";
            car.Placa = "HZG-1234";
            car.Cor = "Branco";
            lista.Add(car);


            return View("Index");
        }

        // GET: Agencia/Edit/5     
        public ActionResult AgenciaEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agencia t = db.Agencias.Find(id);
            if (t == null)
            {
                return HttpNotFound();
            }
            ViewBag.Carros = db.Carros;
            return View(t);
        }

        // POST Agencia/Edit/5
        [HttpPost]
        public ActionResult AgenciaEdit([Bind(Include = "AgenciaId,Nome,Endereco,HorarioAberto,HorarioFechado")] Agencia model)
        {
            if (ModelState.IsValid)
            {
                var agencia = db.Agencias.Find(model.AgenciaId);
                if (agencia == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                agencia.Nome = model.Nome;
                agencia.Endereco = model.Endereco;
                agencia.HorarioAberto = model.HorarioAberto;
                agencia.HorarioFechado = model.HorarioFechado;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Carros = db.Carros;
            return View(model);
        }

        // GET: Agencia/AgenciaDetails/5
        public ActionResult AgenciaDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agencia age = db.Agencias.Find(id);
            if (age == null)
            {
                return HttpNotFound();
            }
            ViewBag.Agencia = db.Agencias.Find(age.AgenciaId).AgenciaId;
            return View(age);
        }

        public ActionResult AgenciaDelete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Agencia age = db.Agencias.Find(id);
            if (age == null)
            {
                return HttpNotFound();
            }
            ViewBag.Agencia = db.Agencias.Find(age.AgenciaId).AgenciaId;
            return View(age);
        }
        // POST: carros/Delete/5
        [HttpPost, ActionName("AgenciaDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmado(int id)
        {
            Agencia age = db.Agencias.Find(id);
            db.Agencias.Remove(age);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}