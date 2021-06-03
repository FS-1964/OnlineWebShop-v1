using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.PL.ViewModels
{
    public class ContactViewModel
    {
        public string UserName { get; set; }
        public string TelNr { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public IFormFileCollection AttachmentPath { get; set; }
    }
}
