﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFacturas.Core.Modelos;
using ProyectoFacturas.DataAccess.Repositorios;

namespace ProyectoFacturas.Controllers
{
    public class ClientesController : Controller
    {
        private readonly RepositorioClientes _clientes;

        public ClientesController()
        {
            _clientes = new RepositorioClientes();
        }

        // GET: Clientes
        public ActionResult Index()
        {
            var clientes = _clientes.BuscarTodos();

            return View(clientes);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(string id)
        {
            var cliente = _clientes.BuscarPorId(id);

            if (cliente == null)
                return RedirectToAction("Index");

            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            var cliente = new Cliente();

            cliente.ExentoImpuesto = false;
            cliente.FechaRegistro = DateTime.Now;

            return View(cliente);
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _clientes.Registrar(cliente);
                    _clientes.GuardarCambios();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(string id)
        {
            var cliente = _clientes.BuscarPorId(id);

            if (cliente == null)
                return RedirectToAction("Index");

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cliente.Identificacion = id;

                    _clientes.Editar(cliente);
                    _clientes.GuardarCambios();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(string id)
        {
            var cliente = _clientes.BuscarPorId(id);

            if (cliente != null)
            {
                _clientes.Eliminar(cliente);
                _clientes.GuardarCambios();
            }
            
            return RedirectToAction("Index");
        }
    }
}
