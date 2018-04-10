using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multas1.Models;

namespace Multas1.Controllers
{
    public class AgentesController : Controller
    {
        private MultasDb db = new MultasDb();

        // GET: Agentes
        public ActionResult Index()
        {
            return View(db.Agentes.ToList());
        }

        // GET: Agentes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            return View(agentes);
        }

        // GET: Agentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agentes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Esquadra")] Agentes agente,HttpPostedFileBase uploadFotografia )
        {
            //escrever os dados de um novo agente na BD

            //especificar o ID do novo Agente
            int idNovoAgente = db.Agentes.Max (a=>a.ID)+1;
            //guardar o ID do novo Agente
            agente.ID = idNovoAgente;
            //especificar (escolher) o nome do ficheiro
            string nomeImagem = "Agente_"+idNovoAgente+".jpg";
            //var. auxiliar
            string Path = "";
            //escrever o ficheiro com a fotografia no disco rigido, na pasta 'imagens'
            if(uploadFotografia != null){
                //o ficheiro foi fornecido 
                //validar se o que foi fornecido é uma imagem --->TPC
                //formatar o tamanho das imagens

            
                //criar o caminho completo ate ao sitio onde o ficheiro será guardado
                Path = System.IO.Path.Combine(Server.MapPath("~/imagens/"), nomeImagem);
                
                //gurdar o nome do ficheiro escolhido na BD
                agente.Fotografia = nomeImagem;

            }
            else
            {
                //nao foi fornecido qualquer ficheiro
                //gerar uma mensagem de erro
                ModelState.AddModelError("", "Nao foi fornecida uma imagem...");

                //devolver o controlo à View
                return View(agente);
            }
            //gurdar o nome escolhido na BD


            if (ModelState.IsValid)    
            {
                db.Agentes.Add(agente);
                db.SaveChanges();
                uploadFotografia.SaveAs(Path);
                return RedirectToAction("Index");
            }

            return View(agente);
        }

        // GET: Agentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            return View(agentes);
        }

        // POST: Agentes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Esquadra,Fotografia")] Agentes agentes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agentes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // GET: Agentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            return View(agentes);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agentes agentes = db.Agentes.Find(id);
            db.Agentes.Remove(agentes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
