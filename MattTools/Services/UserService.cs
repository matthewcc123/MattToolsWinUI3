using MattTools.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.Storage;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.UI.Xaml;

namespace MattTools.Services
{
    public class UserService : IUserService
    {

        private UserSetting _userSetting;

        public UserService() 
        { 
            _userSetting = new UserSetting();
        }

        public async Task<bool> UpdateExpiredDate()
        {
            try
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage respone = await client.GetAsync("https://raw.githubusercontent.com/matthewcc123/JsonTest/main/ToolsStatus.json");

                if (respone.IsSuccessStatusCode)
                {
                    string result = respone.Content.ReadAsStringAsync().Result;
                    ToolStatus toolStatus = JsonConvert.DeserializeObject<ToolStatus>(result);

                    if (toolStatus.Status != "active")
                    {
                        //Set Expired All the Time
                        _userSetting.App.ExpiredDate = DateTime.Now;
                    }
                    else
                    {
                        //Update Expired Date
                        _userSetting.App.ExpiredDate = DateTime.Now.AddDays(7);
                    }

                    await SaveUserSetting();
                }
                client.Dispose();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task LoadUserSetting()
        {

            try
            {
                string appDataRoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                StorageFolder roamingFolder = await StorageFolder.GetFolderFromPathAsync(appDataRoamingPath);
                StorageFolder mattFolder = await roamingFolder.CreateFolderAsync("MattTools", CreationCollisionOption.OpenIfExists);

                StorageFile file = await mattFolder.GetFileAsync("UserSetting.json");

                string userSettingString = await FileIO.ReadTextAsync(file);

                _userSetting = JsonConvert.DeserializeObject<UserSetting>(userSettingString);
            }
            catch
            {

                _userSetting = new UserSetting();

            }

        }

        public async Task SaveUserSetting()
        {

            string appDataRoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            StorageFolder roamingFolder = await StorageFolder.GetFolderFromPathAsync(appDataRoamingPath);
            StorageFolder mattFolder = await roamingFolder.CreateFolderAsync("MattTools", CreationCollisionOption.OpenIfExists);

            StorageFile file = await mattFolder.CreateFileAsync("UserSetting.json", CreationCollisionOption.ReplaceExisting);

            string userSettingString = JsonConvert.SerializeObject(_userSetting);

            try
            {
                await FileIO.WriteTextAsync(file, userSettingString);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public async void SetUserSetting(string propertyName, object value) 
        {

            string[] propertyParts = propertyName.Split('.');
            object currentObject = _userSetting;

            for (int i = 0; i < propertyParts.Length; i++)
            {
                //Get Property from CurrentObject by Name
                var propertyInfo = currentObject.GetType().GetProperty(propertyParts[i]);

                if (propertyInfo == null)
                    throw new ArgumentException($"Invalid property name: {propertyName}");

                //If the last party of the parts
                if ( i == propertyParts.Length - 1 )
                {
                    propertyInfo.SetValue( currentObject, value );
                }
                else
                {
                    // If not the last part, update currentObject to the nested property
                    currentObject = propertyInfo.GetValue(currentObject);
                }

            }

            await SaveUserSetting();

        }

        public T GetUserSetting<T>(string propertyName)
        {
            string[] propertyParts = propertyName.Split('.');
            object currentObject = _userSetting;

            try
            {
                for (int i = 0; i < propertyParts.Length; i++)
                {
                    //Get Property from CurrentObject by Name
                    var propertyInfo = currentObject.GetType().GetProperty(propertyParts[i]);

                    if (propertyInfo == null)
                        throw new ArgumentException($"Invalid property name: {propertyName}");

                    //If the last party of the parts
                    if (i == propertyParts.Length - 1)
                    {
                        return (T)propertyInfo.GetValue(currentObject);
                    }
                    else
                    {
                        // If not the last part, update currentObject to the nested property
                        currentObject = propertyInfo.GetValue(currentObject);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

            throw new ArgumentException($"Invalid property name: {propertyName}");

        }

    }

}
