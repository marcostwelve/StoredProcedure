using System.Diagnostics;
using CrudUsers.Data;
using CrudUsers.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudUsers.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataAccess _dataAccess;

        public HomeController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IActionResult Index()
        {

            try
            {
                var users = _dataAccess.ListAllUsers();
                return View(users);
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = "Ocorreu um erro na listagem de listagem";
                return View();
            }
        }

        public IActionResult GetTimeNow()
        {
            return View();
        }

        public IActionResult Cadastrar() 
        {
            return View();
        }

        public IActionResult Detalhes(int id)
        {
            var user = _dataAccess.GetUserById(id);
            return View(user);
        }


        [HttpPost]
        public IActionResult Cadastrar(User user)
        {
            try
            {
               if (ModelState.IsValid)
               {
                    var result = _dataAccess.InsertUser(user);

                    if (result)
                    {
                        TempData["MessageSuccess"] = "Usuário cadastrado com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["MessageError"] = "Ocorreu um erro no cadastro de usuário";
                        return View(user);
                    }
               }
               else
               {
                    return View(user);
               }
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = "Ocorreu um erro no cadastro de usuário";
                return View();
            }
        }

        public IActionResult Editar(int id)
        {
            var user = _dataAccess.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Editar(int id, User newUser)
        {

            if (ModelState.IsValid)
            {
                var result = _dataAccess.UpdateUser(newUser);
                if (result)
                {
                    TempData["MessageSuccess"] = "Usuário editado com sucesso!";
                    return RedirectToAction("Index");
                }

                else
                {
                    TempData["MessageError"] = "Ocorreu um erro na edição de usuário";
                    return View(newUser);
                }
            }
            else
            {
                return View(newUser);
            }
        }

        public IActionResult Excluir(int id)
        {
            var result = _dataAccess.DeleteUser(id);

            if (result)
            {
                TempData["MessageSuccess"] = "Usuário excluído com sucesso!";
            }
            else
            {
                TempData["MessageError"] = "Ocorreu um erro na exclusão de usuário";
            }

            return RedirectToAction("Index");
        }
    }
}
