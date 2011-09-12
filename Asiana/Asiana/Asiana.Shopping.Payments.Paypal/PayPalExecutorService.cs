using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Asiana.Shopping.Payments.Paypal
{
    public class PayPalExecutorService
    {
        private const string SIGNATURE = "SIGNATURE";
        private const string PWD = "PWD";
        private const string ACCT = "ACCT";

        private string host;

        public string Host
        {
            get { return host; }
            set { host = value; }
        }
        private string endPoint;

        private string userName;
        private string password;
        private string signature;

        public PayPalExecutorService(string host, 
            string endPoint, 
            string userName, 
            string password, 
            string signature)
        {
            this.host = host;
            this.endPoint = endPoint;
            this.userName = userName;
            this.password = password;
            this.signature = signature;
        }

        private void GetCredentials(NVPCodec parameters) {
  
            if (!String.IsNullOrWhiteSpace(userName))
                parameters["USER"] = userName;

            if (!String.IsNullOrWhiteSpace(password))
                parameters[PWD] = password;

            if (!String.IsNullOrWhiteSpace(signature))
                parameters[SIGNATURE] = signature;

            //if (!IsEmpty(Subject))
            //    codec["SUBJECT"] = Subject;

            parameters["VERSION"] = "2.3";
        }

        public NVPCodec Execute(NVPCodec parameters)
        {

            GetCredentials(parameters);

            //string url = pendpointurl;

            ////To Add the credentials from the profile
            //string strPost = NvpRequest + "&" + buildCredentialsNVPString();
            //strPost = strPost + "&BUTTONSOURCE=" + HttpUtility.UrlEncode(BNCode);

            var content = parameters.Encode();

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(endPoint);
            objRequest.Timeout = 30000;
            objRequest.Method = "POST";
            objRequest.ContentLength = content.Length;

            try
            {
                using (StreamWriter myWriter = new StreamWriter(objRequest.GetRequestStream()))
                {
                    myWriter.Write(content);
                }
            }
            catch (Exception e)
            {
                /*
                if (log.IsFatalEnabled)
                {
                    log.Fatal(e.Message, this);
                }*/
            }

            //Retrieve the Response returned from the NVP API call to PayPal
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            string result;
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            //Logging the response of the transaction
            /* if (log.IsInfoEnabled)
             {
                 log.Info("Result :" +
                           " Elapsed Time : " + (DateTime.Now - startDate).Milliseconds + " ms" +
                          result);
             }
             */

            NVPCodec decoder = new NVPCodec();
            decoder.Decode(result);
            return decoder;
        }
    }
}
