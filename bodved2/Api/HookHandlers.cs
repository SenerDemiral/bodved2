using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bodved2.ViewModels;
using Starcounter;

namespace bodved2.Api
{
    class HookHandlers : IHandler
    {
        public void Register()
        {
            /*
            Hook<BDB2.STAT>.CommitUpdate += (p, obj) =>
            {
                Session.RunTaskForAll((s, id) =>
                {

                    (s.Store[nameof(MasterPage)] as MasterPage).Data = null;
                    s.CalculatePatchAndPushOnWebSocket();
                });
            };
            */
        }
    }
}
