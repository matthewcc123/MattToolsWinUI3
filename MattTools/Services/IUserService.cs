using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattTools.Services
{
    public interface IUserService
    {
        Task LoadUserSetting();
        Task SaveUserSetting();
        Task<bool> UpdateExpiredDate();
        void SetUserSetting(string propertyName, object value);
        T GetUserSetting<T>(string propertyName);


    }
}
