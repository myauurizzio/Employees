using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmpApp2.Models;
using EmpApp2.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace EmpApp2.Controllers
{
    public class EmpController : Controller
    {
        IEmpRepository empRepository;

        public EmpController(IEmpRepository _empRepository)
        {
            empRepository = _empRepository;
        }

        [Authorize]
        [Route("")]
        [Route("Emp/Index")]
        public IActionResult Index(List<Emps> emps, Vals vals, Emps newemps)
        {
            if (!empRepository.CheckUser(User))
            {
                return View("RejectLogin");
            }

            bool isAdmin = empRepository.CheckAdmin(User);
            ViewBag.isAdmin = isAdmin;

            ViewBag.mode = "";

            if (emps.Count == 0)
            {
                emps = empRepository.LoadEmps();
            }

            if (!String.IsNullOrEmpty(vals.Operation) && vals.Operation == "CHARTS")
            {
                ViewBag.mode += " mode: " + vals.Operation;
                ViewBag.title = "Диаграммы";
                ViewBag.chartDecades = empRepository.GetJSON(emps, "DECADES");
                ViewBag.chartGenders = empRepository.GetJSON(emps, "GENDERS");
                return View("ShowCharts", emps);
            }



            if (isAdmin)
            {

                if (!String.IsNullOrEmpty(vals.Operation))
                {
                    if ((vals?.SourceID != null) && (vals.SourceID != 0))
                    {
                        ViewBag.mode = "SourceID: " + vals.SourceID.ToString();

                    }
                    else
                    {
                        ViewBag.mode = "SourceID is not set";
                    }

                    switch (vals.Operation)
                    {
                        case "DELETE":
                            if ((vals.SourceID > 0) && (vals.SourceID <= emps.Count) && (vals.SourceROWID != null))
                            {
                                if (emps[vals.SourceID - 1].ROWID == vals.SourceROWID)
                                {
                                    emps.Remove(emps[vals.SourceID - 1]);
                                }
                                
                            }

                            int res = empRepository.SaveEmps(emps);
                            emps = empRepository.LoadEmps();
                            ViewBag.mode += " mode: " + vals.Operation + " emps.Count: " + emps.Count;
                            
                            break;
                        case "EDIT":
                            ViewBag.mode += " mode: " + vals.Operation;
                            ViewBag.title = "Добавление сотрудника";
                            ViewBag.position = empRepository.GetPositions(emps[vals.SourceID - 1].Position);
                            return View("EditEmp", emps[vals.SourceID - 1]);
                            //break;
                        case "ADD":
                            ViewBag.mode += " mode: " + vals.Operation;
                            ViewBag.title = "Редактирование сотрудника";
                            ViewBag.position = empRepository.GetPositions(String.Empty);
                            return View("EditEmp", new Emps());
                        //break;
                        case "RENEW":
                            emps = empRepository.InitEmps(null);
                            res = empRepository.SaveEmps(emps);
                            break;

                        default:
                            ViewBag.mode += " mode: " + vals.Operation;
                            break;
                    }
                }






            }

            ViewBag.title = "Главная";
            return View(emps);
        }

        [Authorize]
        [Route("EditRecord")]
        [Route("Emp/EditRecord")]
        public IActionResult EditRecord(Vals vals, Emps newemps)
        {
            List<Emps> emps = empRepository.LoadEmps();

            switch (vals.Operation)
            {
                case "EDIT":
                    emps[newemps.ID - 1] = newemps;
                    break;
                default: // ADD
                    newemps.ROWID = Guid.NewGuid().ToString();
                    emps.Add(newemps);
                    break;
            }
            

            int i = empRepository.SaveEmps(emps);

            return RedirectToAction("Index");
        }

       
    }
}