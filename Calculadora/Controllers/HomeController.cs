using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Calculadora.Models;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {

        string operando;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// apresenta a view com a calculadora, no primeiro pedido - HTTP GET
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.Visor = "0";
            ViewBag.LimparVisor = "false";
            return View();
        }

        [HttpPost]
        public IActionResult Index(string visor, string bt, string operando, string operador, bool limparVisor)
        {
            switch (bt)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    if (visor == "0" || limparVisor) visor = bt;
                    else visor += bt;
                    //impedir o visor de ser limpo
                    limparVisor = false;
                    break;

                case "+/-":
                    //inverter o valor do Visor
                    // feito de duas formas
                    // multiplicar por -1 => converter valor do visor para número
                    // - processar a string : visor.StartsWith("-").ToString().Subtring().Length
                    visor = Convert.ToDouble(visor) * -1 + "";

                    break;
                case ",":
                    if (!visor.Contains(",")) visor += bt;
                    break;

                case "+":
                case "-":
                case ":":
                case "x":
                case "=":


                    if (operador != null)
                    {
                        // executar a operação
                        //variáveis auxiliares
                        double operando1 = Convert.ToDouble(operando);
                        double operando2 = Convert.ToDouble(visor);
                        switch (operador)
                        {
                            case "+":
                                visor = operando1 + operando2 + "";
                                break;
                            case "-":
                                visor = operando1 - operando2 + "";
                                break;
                            case "x":
                                visor = operando1 * operando2 + "";
                                break;
                            case ":":
                                visor = operando1 / operando2 + "";
                                break;
                        }
                    }
                    //guardar valores para memória futura
                    if (bt != "=") operador = bt;
                    else operador = "";
                    operando = visor;
                    limparVisor = true;
                    break;

                case "C":
                    visor = "0";
                    operador = "";
                    operando = "";
                    limparVisor = true;

                    break;
            }
            ViewBag.Visor = visor;
            ViewBag.Operador = operador;
            ViewBag.Operando = operando;
            ViewBag.LimparVisor = limparVisor + "";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
