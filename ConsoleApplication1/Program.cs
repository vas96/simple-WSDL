using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Soap;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using HodStudio.XitSoap;
using HodStudio.XitSoap.Authentication;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceReference1.WebService1SoapClient client = new ServiceReference1.WebService1SoapClient();
            //var a = client.HelloWorld();
            //Console.WriteLine(a);

            //var a2 = client.HelloWorld2("123");
            //Console.WriteLine(a2);
            // Console.ReadLine();

            XmlDocument soapEnvelopeXml = new XmlDocument();

            HttpWebRequest request = CreateWebRequest("HelloWorld2");                       
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soap:Body>
                                <HelloWorld2 xmlns=""http://tempuri.org/"">
                                    <str>123</str>
                                </HelloWorld2>
                              </soap:Body>
                            </soap:Envelope>");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                   // Console.WriteLine(soapResult);
                
                }
            }
            
            HttpWebRequest addRequest = CreateWebRequest("Add");         
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soap:Body>
                                <Add xmlns=""http://tempuri.org/"">
                                    <val1>123</val1>
                                    <val2>123</val2>
                                </Add>
                              </soap:Body>
                            </soap:Envelope>");

            using (Stream stream = addRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
            
            using (WebResponse response = addRequest.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                //  Console.WriteLine(soapResult);
                    soapEnvelopeXml.LoadXml(soapResult);
                    Console.WriteLine(soapEnvelopeXml.InnerText);
                }             
                Console.WriteLine((response as HttpWebResponse).StatusCode);
                Console.WriteLine(response.ResponseUri);
                Console.WriteLine(response.ContentLength);
                Console.WriteLine(response.ContentType);
                Console.WriteLine("--------------------------------");

            }

            HttpWebRequest divisionRequest = CreateWebRequest("Division");
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soap:Body>
                                <Division xmlns=""http://tempuri.org/"">
                                    <val1>123</val1>
                                    <val2>23</val2>
                                </Division>
                              </soap:Body>
                            </soap:Envelope>");

            using (Stream stream = divisionRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
            
            using (WebResponse response = divisionRequest.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    string soapResult = rd.ReadToEnd();
                    soapEnvelopeXml.LoadXml(soapResult);
                    Console.WriteLine(soapEnvelopeXml.InnerText);                                  
                }
                Console.WriteLine(response.ResponseUri);
                Console.WriteLine("--------------------------------");
            }

            HttpWebRequest multiplicationRequest = CreateWebRequest("Multiplication");
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soap:Body>
                                <Multiplication xmlns=""http://tempuri.org/"">
                                    <val1>123</val1>
                                    <val2>23</val2>
                                </Multiplication>
                              </soap:Body>
                            </soap:Envelope>");

            using (Stream stream = multiplicationRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = multiplicationRequest.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    string soapResult = rd.ReadToEnd();
                    soapEnvelopeXml.LoadXml(soapResult);
                    Console.WriteLine(soapEnvelopeXml.InnerText);
                }
                Console.WriteLine(response.ResponseUri);
                Console.WriteLine("--------------------------------");
            }


            HttpWebRequest percentRequest = CreateWebRequest("Percent");
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soap:Body>
                                <Percent xmlns=""http://tempuri.org/"">
                                    <val1>123</val1>
                                    <val2>23</val2>
                                </Percent>
                              </soap:Body>
                            </soap:Envelope>");

            using (Stream stream = percentRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = percentRequest.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    string soapResult = rd.ReadToEnd();
                    soapEnvelopeXml.LoadXml(soapResult);
                    Console.WriteLine(soapEnvelopeXml.InnerText);
                }
                Console.WriteLine(response.ResponseUri);
                Console.WriteLine("--------------------------------");
            }

            HttpWebRequest subtractRequest = CreateWebRequest("Subtract");
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soap:Body>
                                <Subtract xmlns=""http://tempuri.org/"">
                                    <val1>123</val1>
                                    <val2>23</val2>
                                </Subtract>
                              </soap:Body>
                            </soap:Envelope>");

            using (Stream stream = subtractRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = subtractRequest.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    string soapResult = rd.ReadToEnd();
                    soapEnvelopeXml.LoadXml(soapResult);
                    Console.WriteLine(soapEnvelopeXml.InnerText);
                }
                Console.WriteLine(response.ResponseUri);
                Console.WriteLine("--------------------------------");
            }

            Console.ReadLine();
        }

        public static HttpWebRequest CreateWebRequest(string func)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://localhost:51839/Calculator.asmx?op="+func+"");
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
    }
}
