using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace BankSystem.BL.Controllers
{
    public class BaseController
    {
        public event Action<string> Messages;
        protected void Save(string fileName, object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate))
            {

                formatter.Serialize(file, obj);
            }
        }
        protected T Load<T>(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (file.Length > 0 && formatter.Deserialize(file) is T items)
                {
                    return items;
                }
                else
                {
                    return default(T);
                }
            }
        }
        protected void MessagesToSend(string message)
        {
            Messages?.Invoke(message);
        }
    }
}
