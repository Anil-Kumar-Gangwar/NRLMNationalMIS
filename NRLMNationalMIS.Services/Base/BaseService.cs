using NRLMNationalMIS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Xml.Serialization;

namespace NRLMNationalMIS.Services
{
    public class BaseService : IDisposable, IBaseService
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void Dispose()
        {
        }
        public string GetPercentage(int outOfTotal, int Total)
        {
            int Percentage = 0;
            try
            {
                if (Total > 0)
                {
                    Percentage = ((outOfTotal * 100) / Total);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Percentage.ToString();
        }

        //public void SendEmail(Common.EmailMessage message)
        //{
        //    try
        //    {
        //        
        //        string emailFriendlyName = AppSettingService.GetAppSettingByKey("EmailFriendlyName");
        //        string emailFrom = AppSettingService.GetAppSettingByKey("EmailFrom");
        //        string smtpClientHost = AppSettingService.GetAppSettingByKey("SmtpClientHost");
        //        message.From = emailFrom;
        //        message.SmtpClientHost = smtpClientHost;
        //        message.FriendlyName = emailFriendlyName;
        //        //
        //       // common.EmailHelper.SendEmail(message);
        //        common.EmailHelper.SendEmailTest(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("Message- " + ex.Message + ", StackTrace-" + ex.StackTrace + ", DateTimeStamp-" + DateTime.Now);
        //        throw;
        //    }
        //}
        public int ColumnLetterToColumnIndex(string columnLetter)
        {
            return columnLetter.Select((c, i) => ((c - 'A' + 1) * ((int)Math.Pow(26, columnLetter.Length - i - 1)))).Sum();
        }
        public string GetLettersOnly(string input)
        {
            return new String(input.Where(c => Char.IsLetter(c) && Char.IsUpper(c)).ToArray());
        }
        public int GetNumbersOnly(string input)
        {
            var numberString = new String(input.Where(c => Char.IsNumber(c)).ToArray());
            return string.IsNullOrEmpty(numberString) ? -1 : int.Parse(numberString);
        }
    }
}
