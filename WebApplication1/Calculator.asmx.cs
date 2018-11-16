using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string HelloWorld2(string str)
        {
            return "Hello World " + str;
        }

        [WebMethod]
        public string Add(double val1, double val2)
        {
            return Math.Round((val1 + val2), 4).ToString();
        }

        [WebMethod]
        public string Subtract(double val1, double val2)
        {
            return Math.Round((val1 - val2), 4).ToString();
        }
       
        [WebMethod]
        public string Division(double val1, double val2)
        {
            double result = 0;
            if (val2 == 0)
            {
                result = 0;
            }
            else
            {
                result = Math.Round((val1 / val2), 4);
            }

            return result.ToString();
        }

        [WebMethod]
        public string Multiplication(double val1, double val2)
        {
            return Math.Round((val1 * val2), 4).ToString();
        }

        [WebMethod]
        public string Percent(double val1, double val2)
        {
            string eror = "some ting Wong";
            double result = 0;
            if (val2 > 100 || val2 < 0)
            {
                return eror;
            }
            else
            {
                result = Math.Round((val1 * val2 / 100), 4);
                return result.ToString();
            }
        }
    }
}
