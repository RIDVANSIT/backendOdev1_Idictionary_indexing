using Example.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.services
{
   
    public static class MicrosoftExtensions
    {
       public static IList<IUser>? FindByIndex(this string ar,string search, IDictionary<string, IList<string>> indexes, IDictionary<string, IUser> fastList)
        {

            if (indexes.ContainsKey(search))
            {
                var findedKeys = indexes[search];
                return findedKeys.Select(key => fastList[key]).ToList();
            }
            return null;
        }
        public static List<T> FindAll<T>(this IList<T> values, Predicate<T> predicate)
        {
            return values.ToList().FindAll(predicate);
        }
        public static T? Find<T>(this IList<T> values, Predicate<T> predicate)
        {
            return values.ToList().Find(predicate);
        }



        //fastList.AddToDictionary(personalUsers.Select(user=>(user as IUser)).ToList(),indexes);
        public static void AddToDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> values,//IDictionary<string, IUser> fastList = new Dictionary<string, IUser>();
            IList<TValue> users,//personalUsers.Select(user=>(user as IUser)).ToList()  var personalUsers = JsonConvert.DeserializeObject<IList<Personal>>(Datas.PersonalJson);
            IDictionary<TKey, IList<TKey>> indexes//IDictionary<string,IList<string>> indexes =new Dictionary<string,IList<string>>();
            )
            where TValue : IUser
            where TKey : notnull
        {
            TKey castToKey(object key)
            {
                return (TKey)key;
            };
            void addIndex(object findKeyObject, TKey dataKey)// user.firstname ile tkey türünden username buraya geldi
            {
                TKey findKey = castToKey(findKeyObject);//tkey türünden user.firstname findkeye atıldı
                if (indexes.ContainsKey(findKey))// indexin içinde tkey türünden user.firstname arandı 
                {
                    indexes[findKey].Add(dataKey);//indexin firstname alanına username altında user ozellikleri eklendi e
                }
                else
                {
                    indexes.Add(findKey, new List<TKey>() { dataKey });// yoksa indexte firstname diye bir alan oluşturup içine usernamei  bir listeye ekledik
                }
            };
            users?.ToList().ForEach(user =>//personel listedeki userları birer birer gez
            {
                TKey key = castToKey(user.UserName);//username Tkey türüne çevir ve keye at
                values.Add(key, user);//fastliste ekle
                addIndex(user.FirstName, key);// fistname özelliğine göre usrrname kısımlarını indexliyor
                addIndex(user.LastName, key);// last name özelliğine göre usrrname kısımlarını indexliyor
                string tcno = user.TCNo.ToString();
                addIndex(tcno, key);

                var personal = user.CastTo<IPersonal>();
                var student = user.CastTo<IStudent>();
                var jobber = user.CastTo<IJobber>();
                if (personal != null)
                {
                    addIndex(personal.SSN, key);//SSN key  özelliğine göre usrrname kısımlarını indexliyor
                    string salary = personal.Salary.ToString();
                    addIndex(salary, key);
                }
                if (student != null)
                {
                    string absenteeism = student.Absenteeism.ToString();
                    addIndex(absenteeism, key);
                    string studentNo = student.StdNumber.ToString();
                    addIndex(studentNo, key);
                    string marks = student.Marks.ToString();
                    addIndex(marks, key);

                }
                if (jobber != null)
                {
                    string workArea = jobber.WorkArea.ToString();
                    addIndex(workArea, key);
                    string carNo = jobber.CarNo.ToString();
                    addIndex(carNo, key);

                }

            });
        }
        public static TObject? CastTo<TObject>(this object value)
            where TObject : class
        {
            if (value is TObject)
            {
                return value as TObject;
            }
            return null;
        }
    }

}
