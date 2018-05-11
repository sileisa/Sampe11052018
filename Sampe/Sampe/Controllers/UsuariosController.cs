﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sampe;
using Sampe.Models;

namespace Sampe.Controllers
{
    public class UsuariosController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Cargo);
            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "NomeCargo");
            db.Cargoes.Load();
            return View(db.Usuarios.ToList().OrderBy(u => u.NomeUsuario));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UsuarioId,NomeUsuario,SobrenomeUsuario,Login,Senha,Hierarquia,CargoId")] Usuario usuario)
        {
            //    , [Bind(Include = "CargoId")]Cargo cargo
            //usuario.CargoId = cargo.CargoId;
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "NomeCargo", usuario.CargoId);
            return View(usuario);
        }

        public ActionResult AlterarSenha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
           
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarSenha([Bind(Include = "UsuarioId,NomeUsuario,SobrenomeUsuario,Login,Senha,Hierarquia,CargoId")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(usuario);
        }


        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            db.Entry(usuario).Reference(u => u.Cargo).Load();
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "NomeCargo");
            return View();
        }

        // POST: Usuarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioId,NomeUsuario,SobrenomeUsuario,Login,Senha,Hierarquia,CargoId")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "NomeCargo", usuario.CargoId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "NomeCargo", usuario.CargoId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioId,NomeUsuario,SobrenomeUsuario,Login,Senha,Hierarquia,CargoId")] Usuario usuario)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CargoId = new SelectList(db.Cargoes, "CargoId", "NomeCargo", usuario.CargoId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            db.Entry(usuario).Reference(u => u.Cargo).Load();
            
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            
            db.Usuarios.Remove(usuario);
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