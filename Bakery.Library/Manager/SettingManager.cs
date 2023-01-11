using Bakery.Library.Entity;
using Bakery.Library.Services;
using OrionDAL.OAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Library.Manager
{
    public class SettingManager : ISettingService
    {
        public void Create(Setting setting)
        {
            setting.Save();
        }

        public Setting Get()
        {
            return Transaction.Instance.Read<Setting>("Select top 1 * from Setting")?? new Setting();
        }
    }
}
