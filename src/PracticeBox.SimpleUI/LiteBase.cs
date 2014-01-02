using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ServiceStack.OrmLite;

namespace PracticeBox.SimpleUI
{
    public class LiteBase : INotifyPropertyChanged
    {
        public List<T> GetList<T>(Expression<Func<T, bool>> thefunction) where T : new()
        {
            var objects = new List<T>();
            using (var conn = DataManager.Manager.OpenDbConnection())
            {
                objects = conn.Where<T>(thefunction);
            }
            return objects;
        }

        public T GetItem<T>(Expression<Func<T, bool>> thefunction) where T : new()
        {
            T obj = new T();
            using (var conn = DataManager.Manager.OpenDbConnection())
            {
                obj = conn.Where<T>(thefunction).FirstOrDefault<T>();
            }
            return obj;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
