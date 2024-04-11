using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlibliotecaMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace BlibliotecaMVC.Controllers
{

    public class UsuariosController: Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UsuariosController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }



        //nos permite que cualquier usuario pueda invocar esta accion este logueado o no
        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var usuario = new IdentityUser() { Email = modelo.Email, UserName = modelo.Email };


            //Crea el usuario en la BD
            var resultado = await userManager.CreateAsync(usuario, password: modelo.Password);

            if (resultado.Succeeded)
            {
                //Inicia sesión de un usuario y debe existir ya en la BD
                await signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(modelo);
            }
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var resultado = await signInManager.PasswordSignInAsync(modelo.Email, modelo.Password, 
                                    modelo.Recuerdame, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToAction("index", "Home");
            }else
            {
                ModelState.AddModelError(string.Empty, "nombre de usuario o password incorrecto");
                return View(modelo);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //´Pasamos el esquema de autentificacion
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");

        }


    }
}