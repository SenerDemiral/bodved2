using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bodved2.ViewModels;
using Starcounter;
using BDB2;

namespace bodved2.Api
{
    class HookHandlers : IHandler
    {
        public void Register()
        {
            /*
            Hook<CET>.CommitUpdate += (p, obj) =>
            {
                Session.RunTaskForAll((s, id) =>
                {
                    s.CalculatePatchAndPushOnWebSocket();
                });
            };

            Hook<MAC>.CommitUpdate += (p, obj) =>
            {
                Session.RunTaskForAll((s, id) =>
                {
                    s.CalculatePatchAndPushOnWebSocket();
                });
            };
            */
        }
    }
}
