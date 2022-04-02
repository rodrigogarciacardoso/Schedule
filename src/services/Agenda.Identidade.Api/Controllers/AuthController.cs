using Agenda.Identidade.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Identidade.Api.Controllers;

[Route("api/identidades")]
public class AuthController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("nova-conta")]
    public async Task<IActionResult> Registrar(UsuarioRegistro usuarioRegistro)
    {
        if (ModelState.IsValid == false) return BadRequest();

        var user = new IdentityUser()
        {
            UserName = usuarioRegistro.Email,
            Email = usuarioRegistro.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost]
    [Route("autenticar")]
    public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
    {
        if (ModelState.IsValid == false) return BadRequest();

        var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, 
            false, true);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest();
    }
}