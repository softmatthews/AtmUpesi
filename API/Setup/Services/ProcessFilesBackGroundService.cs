using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using Application.Core;
using ValidationException = Application.Core.Exceptions.ValidationException;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Rebus.Messages;
using Application.Interfaces.Settings;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using Application.Core.Exceptions;
using Domain.Enums;

namespace API.Setup.Services
{
    public class ProcessFilesBackGroundService : BackgroundService
    {

        public IFormFile ReturnFormFile(FileStreamResult result)
        {
            var ms = new MemoryStream();
            try
            {
                result.FileStream.CopyTo(ms);
                return new FormFile(ms, 0, ms.Length, "test", "something");
            }
            catch
            {
                ms.Dispose();
                throw;
            }
            finally
            {
                ms.Dispose();
            }

        }


        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ProcessFilesBackGroundService(IServiceScopeFactory serviceScopeFactory)
        {

            _serviceScopeFactory = serviceScopeFactory;
        }



        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            using var scope = _serviceScopeFactory.CreateScope();
            WriteToFile("Start redeading Files");
           
        }

        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using StreamWriter sw = File.CreateText(filepath);
                sw.WriteLine(Message);
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }


    }
}