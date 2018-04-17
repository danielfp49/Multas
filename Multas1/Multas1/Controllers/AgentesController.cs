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
        /// <summary>
        /// lista todos os agentes
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //obter a lista de todos os agentes
            // em SQL: select * From Agentes ORDER BY Nome;
            var listaDeAgentes = db.Agentes.ToList().OrderBy(a => a.Nome);

            return View(listaDeAgentes);

        }

        // GET: Agentes/Details/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest); instrução original predefenida por defeito, pois o sistema nao sabe o detalhe do agente quer ver 

                //redirecionar para uma pagina que nos controlamos
                return RedirectToAction("Index");
            }
            Agentes agente = db.Agentes.Find(id);
            if (agente == null)
            {
                //o agente nao foi encontrado logo, gera-se uma msg de erro
                //return HttpNotFound();
                //redirecionar para uma pagina que nos controlamos
                return RedirectToAction("Index");


            }
            return View(agente);
        }

        // GET: Agentes/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agentes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Esquadra")] Agentes agente, HttpPostedFileBase uploadFotografia)
        {
            //escrever os dados de um novo agente na BD

            //especificar o ID do novo Agente
            //testar se há registos na tabela dos agentes
            //  if (db.Agentes.Count() != 0) {     } 


            // ou entao, usar a instrução TRY/CATCH
            int idNovoAgente = 0;
            try {
                idNovoAgente = db.Agentes.Max(a => a.ID) + 1;


            }
            catch (Exception)
            {
                idNovoAgente = 1;
            }

            //guardar o ID do novo Agente
            agente.ID = idNovoAgente;
            //especificar (escolher) o nome do ficheiro
            string nomeImagem = "Agente_" + idNovoAgente + ".jpg";
            //var. auxiliar
            string Path = "";
            //escrever o ficheiro com a fotografia no disco rigido, na pasta 'imagens'
            if (uploadFotografia != null) {
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
                try
                {
                    //adiciona o novo agente à BD
                    db.Agentes.Add(agente);
                    
                    // faz 'commit' às alteraçoes
                    db.SaveChanges();
                   
                    //escrever o ficheiro com a fotografia no disco rigido
                    uploadFotografia.SaveAs(Path);
                    
                    // se tudo correr be,, redireciona para a página do Index
                    return RedirectToAction("Index");
                }

                catch (Exception)
                {
                    ModelState.AddModelError("", "Houve um erro com a criação do novo agente");
                }

            }

            return View(agente);
        }



        // GET: Agentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest); instrução original predefenida por defeito, pois o sistema nao sabe o detalhe do agente quer ver 

                //redirecionar para uma pagina que nos controlamos
                return RedirectToAction("Index");
            }
            Agentes agente = db.Agentes.Find(id);
            if (agente == null)
            {
                //o agente nao foi encontrado logo, gera-se uma msg de erro
                //return HttpNotFound();
                //redirecionar para uma pagina que nos controlamos
                return RedirectToAction("Index");


            }
            return View(agente);
        }

        // POST: Agentes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentes"></param>
        /// <returns></returns>
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
        /// <summary>
        /// apresenta na view os dados de um agente, com vista à sua, eventual, eliminação
        /// </summary>
        /// <param name="id">identificador do agente</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            //pesquisar pelo agentente, cujo ID foi fornecido
            Agentes agente = db.Agentes.Find(id);

            //verificar se o agente foi encontrado
            if (agente == null)
            {
                //o agente nao existe
                // redirecionar para a pagina tual
                return RedirectToAction("Index");


            }
            return View(agente);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            try {

                Agentes agente = db.Agentes.Find(id);
                db.Agentes.Remove(agente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                ModelState.AddModelError.Format("", string.Format("Não é possivel apagar agente nº {0} -{1}, porque há multas associadas a ele", id, agente.Nome)
             ///se existir uma classe chamada 'Erro.cs'
             ///iremos nela registas os dados do erro
             /// - criar um objeto desta classe
             /// - atribuir a esse objeto od dados de erro
             ///     - nome do controller
             ///     - nome do método
             ///     - data + hora do erro
             ///     - mensagem do erro (ex)
             ///     - dados que se tentavam inserir
             ///     - outros dados considerados relevante
             /// - guardar o objeto na BD
             /// - notificar um gestor do sistema, por email, ou por outro meio, da ocorrencia do erro e dos seus dados



             );

            }
        
            //se cheguei aqui é porque houve um problema
            //devolvo os dados do agente à view
            return View(agente);
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
