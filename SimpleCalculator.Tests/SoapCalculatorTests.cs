using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Xml;
using Xunit;

namespace SimpleCalculator.Tests
{
    public class SoapCalculatorTests
    {
        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

        XmlDocument soapEnvelopeXml = new XmlDocument();

        public static HttpWebRequest CreateWebRequest(string func)
        {
            HttpWebRequest webRequest =
                (HttpWebRequest) WebRequest.Create(@"http://localhost:51839/Calculator.asmx?op=" + func + "");
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        public static IEnumerable<object[]> GetData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] {"100", "20"},
                new object[] {"10", "0,5"},
                new object[] {"-2", "-200"},
                new object[] {(int.MaxValue).ToString(), (int.MinValue).ToString()},
            };
            return allData.Take(numTests);
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: 3)]
        public void AddTest(string value1, string value2)
        {
            HttpWebRequest request = CreateWebRequest("Division");
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soap:Body>
                                <Add xmlns=""http://tempuri.org/"">
                                    <val1>" + value1 + @"</val1>
                                    <val2>" + value1 + @" </val2>
                                </Add>
                              </soap:Body>
                            </soap:Envelope>");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd =
                    new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    string soapResult = rd.ReadToEnd();
                    soapEnvelopeXml.LoadXml(soapResult);
                    Assert.NotEmpty(soapEnvelopeXml.InnerText);

                    using (StreamWriter w = File.AppendText("log.txt"))
                    {
                        Log("AddTest; " + "Values:" + value1 + ";" + value2 +"; Status code: " + (response as HttpWebResponse).StatusCode.ToString() + "; Result: "+ Environment.NewLine + soapResult , w);
                    }
                }

                Assert.Equal(((response as HttpWebResponse).StatusCode), HttpStatusCode.OK);
                Assert.NotNull(response.ContentLength);
                Assert.Equal((response.ContentType), "text/xml; charset=utf-8");



            }
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: 3)]
        public void SubtractTest(string value1, string value2)
        {
            HttpWebRequest request = CreateWebRequest("Division");
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soap:Body>
                                <Subtract xmlns=""http://tempuri.org/"">
                                    <val1>" + value1 + @"</val1>
                                    <val2>" + value1 + @" </val2>
                                </Subtract>
                              </soap:Body>
                            </soap:Envelope>");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd =
                    new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    string soapResult = rd.ReadToEnd();
                    soapEnvelopeXml.LoadXml(soapResult);
                    Assert.NotEmpty(soapEnvelopeXml.InnerText);

                    using (StreamWriter w = File.AppendText("log.txt"))
                    {
                        Log("SubtractTest; " + "Values:" + value1 + ";" + value2 + "; Status code: " + (response as HttpWebResponse).StatusCode.ToString() + "; Result: " + Environment.NewLine + soapResult, w);
                    }
                }

                Assert.Equal(((response as HttpWebResponse).StatusCode), HttpStatusCode.OK);
                Assert.NotNull(response.ContentLength);
                Assert.Equal((response.ContentType), "text/xml; charset=utf-8");

            }
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: 4)]
        public void DivisionTest(string value1, string value2)
        {
            HttpWebRequest divisionRequest = CreateWebRequest("Division");
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soap:Body>
                                <Division xmlns=""http://tempuri.org/"">
                                    <val1>" + value1 + @"</val1>
                                    <val2>" + value1 + @" </val2>
                                </Division>
                              </soap:Body>
                            </soap:Envelope>");

            using (Stream stream = divisionRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = divisionRequest.GetResponse())
            {
                using (StreamReader rd =
                    new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    string soapResult = rd.ReadToEnd();
                    soapEnvelopeXml.LoadXml(soapResult);
                    Assert.NotEmpty(soapEnvelopeXml.InnerText);

                    using (StreamWriter w = File.AppendText("log.txt"))
                    {
                        Log("DivisionTest; " + "Values:" + value1 + ";" + value2 + "; Status code: " + (response as HttpWebResponse).StatusCode.ToString() + "; Result: " + Environment.NewLine + soapResult, w);
                    }
                }

                Assert.Equal(((response as HttpWebResponse).StatusCode), HttpStatusCode.OK);
                Assert.NotNull(response.ContentLength);
                Assert.Equal((response.ContentType), "text/xml; charset=utf-8");

            }
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: 4)]
        public void MultiplicationTest(string value1, string value2)
        {
            HttpWebRequest divisionRequest = CreateWebRequest("Division");
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soap:Body>
                                <Multiplication xmlns=""http://tempuri.org/"">
                                    <val1>" + value1 + @"</val1>
                                    <val2>" + value1 + @" </val2>
                                </Multiplication>
                              </soap:Body>
                            </soap:Envelope>");

            using (Stream stream = divisionRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = divisionRequest.GetResponse())
            {
                using (StreamReader rd =
                    new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    string soapResult = rd.ReadToEnd();
                    soapEnvelopeXml.LoadXml(soapResult);
                    Assert.NotEmpty(soapEnvelopeXml.InnerText);

                    using (StreamWriter w = File.AppendText("log.txt"))
                    {
                        Log("MultiplicationTest; " + "Values:" + value1 + ";" + value2 + "; Status code: " + (response as HttpWebResponse).StatusCode.ToString() + "; Result: " + Environment.NewLine + soapResult, w);
                    }
                }

                Assert.Equal(((response as HttpWebResponse).StatusCode), HttpStatusCode.OK);
                Assert.NotNull(response.ContentLength);
                Assert.Equal((response.ContentType), "text/xml; charset=utf-8");

            }
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: 2)]
        public void PercentTest(string value1, string value2)
        {
            HttpWebRequest divisionRequest = CreateWebRequest("Division");
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                            <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soap:Body>
                                <Percent xmlns=""http://tempuri.org/"">
                                    <val1>" + value1 + @"</val1>
                                    <val2>" + value1 + @" </val2>
                                </Percent>
                              </soap:Body>
                            </soap:Envelope>");

            using (Stream stream = divisionRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = divisionRequest.GetResponse())
            {
                using (StreamReader rd =
                    new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    string soapResult = rd.ReadToEnd();
                    soapEnvelopeXml.LoadXml(soapResult);
                    Assert.NotEmpty(soapEnvelopeXml.InnerText);

                    using (StreamWriter w = File.AppendText("log.txt"))
                    {
                        Log("PercentTest; " + "Values:" + value1 + ";" + value2 + "; Status code: " + (response as HttpWebResponse).StatusCode.ToString() + "; Result: " + Environment.NewLine + soapResult, w);
                    }
                }

                Assert.Equal(((response as HttpWebResponse).StatusCode), HttpStatusCode.OK);
                Assert.NotNull(response.ContentLength);
                Assert.Equal((response.ContentType), "text/xml; charset=utf-8");

            }
        }
    }
}
