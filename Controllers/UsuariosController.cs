using Microsoft.AspNetCore.Mvc;
using RpgApi.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RpgApi.Models;
using RpgApi.Utils;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _Context;

        public UsuariosController(DataContext Context)
        {
            _Context = Context;
        }
       
       private async Task<bool> UsuariosExistente (string username)
       {
           if (await _Context.Usuarios.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
           {
               return true;
           }

           return false;
       }

  [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario(Usuario user)
        { 
            try 
        {
            if (await UsuariosExistente(user.Username))
             throw new System.Exception("Nome de usuário já existe");
            
            Criptografia.CriarPasswordHash(user.PasswordString, out byte[] hash, out byte[]salt);
            user.PasswordString = string.Empty;
            user.PasswordHash = salt;
            await _Context.Usuarios.AddAsync(user);
            await _Context.SaveChangesAsync();

            return Ok(user.Id);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
   
   public static bool VerificarPasswordHash(string password, byte[] hash, byte[] salt)
   {
       using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
       {

           var ComputeHash =
           hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           for (int i = 0; i < ComputeHash.Length; i++)
           {
               if (ComputeHash[i] != hash[i])
               {
                   return false;
               }
           }
           return true;
     }
 }

 
       



    }
    
    }
