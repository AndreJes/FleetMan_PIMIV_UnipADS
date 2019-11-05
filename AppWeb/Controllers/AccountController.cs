﻿using AppWeb.Models;
using AppWeb.Models.ClienteUsuario;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Modelo.Classes.Clientes;
using Modelo.Classes.Usuarios;
using Persistencia.DAL.Usuarios;
using Persistencia.DAL.Usuarios.Web;
using Servicos.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AppWeb.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private GerenciadorUsuarios Gerenciador
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuarios>();
            }
        }

        private ClienteService ClienteService = new ClienteService();

        private UsuarioClienteDAL UsuarioClienteDAL
        {
            get
            {
                return new UsuarioClienteDAL();
            }
        }

        // GET: Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [Authorize]
        public ActionResult Edit(string id)
        {
            UsuarioCliente usuarioCliente = null;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                usuarioCliente = Gerenciador.FindById(id);
                if (usuarioCliente.Email != HttpContext.User.Identity.Name)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                }
            }

            if (usuarioCliente == null)
            {
                return new HttpNotFoundResult();
            }

            UsuarioEditViewModel usuarioClienteView = new UsuarioEditViewModel();
            usuarioClienteView.UsuarioId = usuarioCliente.Id;
            usuarioClienteView.EmailAtual = usuarioCliente.Email;

            return View(usuarioClienteView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UsuarioCliente usuario = Gerenciador.Find(model.Email, model.Senha);
                if (usuario == null)
                {
                    @ViewBag.Error = "Usuário não encontrado ou senha inválida!";
                }
                else
                {
                    ClaimsIdentity identity = Gerenciador.CreateIdentity(usuario, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
                    if (returnUrl == null)
                    {
                        returnUrl = "/Home";
                    }
                    return Redirect(returnUrl);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioEditViewModel usuarioEditView)
        {
            if (ModelState.IsValid)
            {
                var user = Gerenciador.Find(usuarioEditView.EmailAtual, usuarioEditView.SenhaAtual);

                if (user == null)
                {
                    return View(usuarioEditView);
                }

                #region Editar dados do cliente e Email do Usuario
                Cliente cliente =  ClienteService.ObterClientePFPorId(long.Parse(usuarioEditView.UsuarioId));
                if(cliente == null)
                {
                    cliente = ClienteService.ObterClientePJPorId(long.Parse(usuarioEditView.UsuarioId));
                }

                if (!string.IsNullOrEmpty(usuarioEditView.EmailAtual) || !string.IsNullOrWhiteSpace(usuarioEditView.EmailAtual))
                {
                    cliente.Email = usuarioEditView.EmailAtual;
                    ClienteService.GravarCliente(cliente);
                    UsuarioClienteDAL.AlterarUsuarioCliente(usuarioEditView.UsuarioId, novoEmail: usuarioEditView.EmailAtual);
                }
                #endregion
                if (!string.IsNullOrEmpty(usuarioEditView.NovaSenha) || !string.IsNullOrEmpty(usuarioEditView.NovaSenha))
                {
                    UsuarioClienteDAL.AlterarUsuarioCliente(usuarioEditView.UsuarioId, novaSenha: usuarioEditView.NovaSenha);
                }

                return RedirectToAction("Details", "Cliente");
            }

            return View(usuarioEditView);
        }

        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}