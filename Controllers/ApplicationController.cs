using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskManagementSystem.Models;
using TaskManagementSystem.Repository;

namespace TaskManagementSystem.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly ApplicationRepository _applicationRepository;
        private readonly IHttpContextAccessor _contxt;

        public ApplicationController(ApplicationRepository applicationRepository, IHttpContextAccessor contxt)
        {
            _applicationRepository = applicationRepository;
            _contxt = contxt;
        }


        #region Login and Logout
        // GET: ApplicationController/Login
        public IActionResult Login()
        {
            if (_contxt.HttpContext.Session.GetString("UserInfo") == null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("EnterTask");
            }

        }

        // POST: ApplicationController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Employee employee)
        {
            try
            {
              

                Employee employeeInfo = _applicationRepository.Login(employee);
                if (employeeInfo.EmpID!= 0) 
                {

                    _contxt.HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(employeeInfo));
                    //return RedirectToAction();
                    return RedirectToAction("EnterTask");
                }
                else
                {
                    ViewBag.LoginError = "Invalid Credentials.";
                    return View();
                }
                
               
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _contxt.HttpContext.Session.Remove("UserInfo");
            return RedirectToAction("Login");
        }
        #endregion

        #region TaskEnter
        public IActionResult EnterTask()
        {

            TempData["LoggedIn"] = 1;
            TempData["EmpName"] = JsonConvert.DeserializeObject<Employee>(_contxt.HttpContext.Session.GetString("UserInfo")).EmpName;

            if (_contxt.HttpContext.Session.GetString("UserInfo")!=null)
            {
                ViewBag.Client = _applicationRepository.ClientList().ToList();
                ViewBag.Category = _applicationRepository.CategoryList().ToList();

                return View();
            }
            return RedirectToAction("Login");
           
        }

        // POST: ApplicationController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EnterTask(TaskManagementSystem.Models.Task task)
        {
            if (task.DataBaseID != null && task.TaskTitle != null && task.CategoryID != null)
            {
                task.EmpID = JsonConvert.DeserializeObject<Employee>(_contxt.HttpContext.Session.GetString("UserInfo")).EmpID;
                int success=_applicationRepository.InsertTask(task);


                if (success == 1)
                {
                    return RedirectToAction("SuccessMessage");
                }
                else
                {
                    return RedirectToAction("EnterTask");
                }

            }
            else
            {
                return RedirectToAction("EnterTask");
            }


        }
        #endregion

        #region List Task

        public IActionResult ListTask()
        {
            TempData["LoggedIn"] = 1;
            TempData["EmpName"] = JsonConvert.DeserializeObject<Employee>(_contxt.HttpContext.Session.GetString("UserInfo")).EmpName;

            if (_contxt.HttpContext.Session.GetString("UserInfo") != null)
            {
                List<TaskManagementSystem.Models.Task> listTask = _applicationRepository.ListTasks().ToList();
                return View(listTask);
            }
            return RedirectToAction("Login");
        }
        #endregion

        #region AddNotes

        [HttpGet]
        public IActionResult AddingTaskNotes(int id)
        {
            if (_contxt.HttpContext.Session.GetString("UserInfo") != null)
            {
                TempData["LoggedIn"] = 1;
                TempData["EmpName"] = JsonConvert.DeserializeObject<Employee>(_contxt.HttpContext.Session.GetString("UserInfo")).EmpName;
                ViewBag.taskInfo = _applicationRepository.ListTasksNotes(id);
                Notes task = new Notes();

                task.TaskId = id;
                task.EmpID = JsonConvert.DeserializeObject<Employee>(_contxt.HttpContext.Session.GetString("UserInfo")).EmpID;

                return View(task);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddingTaskNotes(Notes note)
        {
            try
            {
                _applicationRepository.InsertTaskNotes(note);
                return RedirectToAction("AddingTaskNotes", new { id = note.TaskId });
            }
            catch
            {
                return View();
            }
        }






        #endregion


        #region Ajax for retrival
        [HttpPost]
        public JsonResult ClientProjects(int clientId)
        {

                IEnumerable<Project> projectList = _applicationRepository.ProjectList(clientId);
                return Json(projectList);
            
        }
        [HttpPost]
        public JsonResult ProjectDataBases(int projectId)
        {
                IEnumerable<DataBase> projectList = _applicationRepository.DataBaseList(projectId).ToList();
                return Json(projectList);
        }


        #endregion

        #region Message
        public IActionResult SuccessMessage()
        {
            if (_contxt.HttpContext.Session.GetString("UserInfo") != null)
            {
                TempData["LoggedIn"] = 1;
                TempData["EmpName"] = JsonConvert.DeserializeObject<Employee>(_contxt.HttpContext.Session.GetString("UserInfo")).EmpName;
                return View();
            }
            
            return RedirectToAction("Login");
        }

        public IActionResult ErrorMessage()
        {
            if (_contxt.HttpContext.Session.GetString("UserInfo") != null)
            {
                TempData["LoggedIn"] = 1;
                TempData["EmpName"] = JsonConvert.DeserializeObject<Employee>(_contxt.HttpContext.Session.GetString("UserInfo")).EmpName;
                return View();
            }
           
            return RedirectToAction("Login");
        }
        #endregion











    }
}
