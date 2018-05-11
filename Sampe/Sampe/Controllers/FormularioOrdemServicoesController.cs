﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sampe;
using Sampe.Models;

namespace Sampe.Controllers
{
    public class FormularioOrdemServicoesController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: FormularioOrdemServicoes
        public ActionResult Index()
        {
            var formularioOrdemServicoes = db.FormularioOrdemServicoes.Include(f => f.Maquina).Include(f => f.Usuario);
            return View(formularioOrdemServicoes.ToList());
        }

        // GET: FormularioOrdemServicoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioOrdemServico formularioOrdemServico = db.FormularioOrdemServicoes.Find(id);
            var busca = from FormularioOrdemServicos in db.FormularioOrdemServicoes
                        where FormularioOrdemServicos.FormularioOrdemServicoId == formularioOrdemServico.FormularioOrdemServicoId
                        join FormularioOSAtividades in db.FormularioOSAtividade
                        on FormularioOrdemServicos.FormularioOrdemServicoId equals FormularioOSAtividades.FormularioOrdemServicoId
                        join AtividadeOS in db.AtividadeOS
                        on FormularioOSAtividades.AtividadeOSId equals AtividadeOS.AtividadeOSId
                        select FormularioOSAtividades;

            var busca2 = from FormularioOrdemServicos in db.FormularioOrdemServicoes
                         where FormularioOrdemServicos.FormularioOrdemServicoId == formularioOrdemServico.FormularioOrdemServicoId
                         join FormularioOSAtividades in db.FormularioOSAtividade
                        on FormularioOrdemServicos.FormularioOrdemServicoId equals FormularioOSAtividades.FormularioOrdemServicoId
                        join AtividadeOS in db.AtividadeOS
                        on FormularioOSAtividades.AtividadeOSId equals AtividadeOS.AtividadeOSId
                        select FormularioOSAtividades.AtividadeOS;

            db.Entry(formularioOrdemServico).Reference(f => f.Maquina).Load();
            db.Entry(formularioOrdemServico).Reference(f => f.Usuario).Load();
            formularioOrdemServico.FormularioOSAtividades = busca.ToArray();
            formularioOrdemServico.AtividadesOs = busca2.ToArray();
            if (formularioOrdemServico == null)
            {
                return HttpNotFound();
            }
            return View(formularioOrdemServico);
        }

        // GET: FormularioOrdemServicoes/Create
        public ActionResult Create()
        {
            ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina");
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario");
            return View();
        }

        // POST: FormularioOrdemServicoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FormularioOrdemServicoId,TipoManutencao,HoraInicio,HoraFinal,Dt,Intervalo,IntervaloInicio,IntervaloFim,ObsIntervalo,Executante,MaquinaId,UsuarioId")] FormularioOrdemServico formularioOrdemServico)
        {
            if (ModelState.IsValid)
            {
                db.FormularioOrdemServicoes.Add(formularioOrdemServico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina", formularioOrdemServico.MaquinaId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario", formularioOrdemServico.UsuarioId);
            return View(formularioOrdemServico);
        }

        // GET: FormularioOrdemServicoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioOrdemServico formularioOrdemServico = db.FormularioOrdemServicoes.Find(id);
            //Para exibir o Status
            var busca = from FormularioOrdemServicos in db.FormularioOrdemServicoes
                        where FormularioOrdemServicos.FormularioOrdemServicoId == formularioOrdemServico.FormularioOrdemServicoId
                        join FormularioOSAtividades in db.FormularioOSAtividade
                        on FormularioOrdemServicos.FormularioOrdemServicoId equals FormularioOSAtividades.FormularioOrdemServicoId
                        join AtividadeOS in db.AtividadeOS
                        on FormularioOSAtividades.AtividadeOSId equals AtividadeOS.AtividadeOSId
                        select FormularioOSAtividades;

            //Para exibir as Atividades
            var busca2 = from FormularioOrdemServicos in db.FormularioOrdemServicoes
                         where FormularioOrdemServicos.FormularioOrdemServicoId == formularioOrdemServico.FormularioOrdemServicoId
                         join FormularioOSAtividades in db.FormularioOSAtividade
                         on FormularioOrdemServicos.FormularioOrdemServicoId equals FormularioOSAtividades.FormularioOrdemServicoId
                         join AtividadeOS in db.AtividadeOS
                         on FormularioOSAtividades.AtividadeOSId equals AtividadeOS.AtividadeOSId
                         select FormularioOSAtividades.AtividadeOS;

            db.Entry(formularioOrdemServico).Reference(f => f.Maquina).Load();
            db.Entry(formularioOrdemServico).Reference(f => f.Usuario).Load();
            formularioOrdemServico.FormularioOSAtividades = busca.ToList();
            formularioOrdemServico.AtividadesOs = busca2.ToArray();
            if (formularioOrdemServico == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina", formularioOrdemServico.MaquinaId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario", formularioOrdemServico.UsuarioId);
            return View(formularioOrdemServico);
        }

        // POST: FormularioOrdemServicoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FormularioOrdemServicoId,TipoManutencao,HoraInicio,HoraFinal,Dt,Intervalo,IntervaloInicio,IntervaloFim,ObsIntervalo,Executante,MaquinaId,UsuarioId")] FormularioOrdemServico formularioOrdemServico, [Bind(Include = "MaquinaId")] Maquina m1, [Bind(Include = "UsuarioId")] Usuario u1, ICollection<int> id)
        {
            List<FormularioOSAtividade> form = new List<FormularioOSAtividade>();

            foreach (var x in id)
            {
                FormularioOSAtividade f1 = new FormularioOSAtividade();
                f1 = db.FormularioOSAtividade.Find(x);
                f1.StatusOS = true;
                form.Add(f1);
            }
            formularioOrdemServico.FormularioOSAtividades = form;
            formularioOrdemServico.MaquinaId = m1.MaquinaId;
            formularioOrdemServico.UsuarioId = u1.UsuarioId;
            //if (ModelState.IsValid)
            //{
                db.Entry(formularioOrdemServico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            //}
            /*
            ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina", formularioOrdemServico.MaquinaId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario", formularioOrdemServico.UsuarioId);
            return View(formularioOrdemServico);*/
        }

        // GET: FormularioOrdemServicoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioOrdemServico formularioOrdemServico = db.FormularioOrdemServicoes.Find(id);
            if (formularioOrdemServico == null)
            {
                return HttpNotFound();
            }
            return View(formularioOrdemServico);
        }

        // POST: FormularioOrdemServicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormularioOrdemServico formularioOrdemServico = db.FormularioOrdemServicoes.Find(id);
            db.FormularioOrdemServicoes.Remove(formularioOrdemServico);
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