using API.Controllers;
using Domain.Enums;
using MediatR;
using Serilog.Core;
using Serilog.Events;
using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Setup
{
    public class CustomSink : ILogEventSink
    {
        private readonly IMediator _mediator;        

        IFormatProvider _formatProvider;
                
        public CustomSink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public async void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider).Split("::");

            if (message.Count()>6)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Duplitae Transaction ID " + message[1]);
                Console.WriteLine("MESSAGE TYPE " + message[3]);
                Console.WriteLine("NOTIFICATION TYPE " + message[7]);               
            }

            Console.ResetColor();
        }
    }
}