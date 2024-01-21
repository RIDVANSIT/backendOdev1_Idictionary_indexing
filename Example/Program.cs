using Example.models;
using Example.services;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

var personalUsers = JsonConvert.DeserializeObject<IList<Personal>>(Datas.PersonalJson);
var studentUsers = JsonConvert.DeserializeObject<IList<Student>>(Datas.StudentJson) ;
var jobberUsers = JsonConvert.DeserializeObject<IList<Jobber>>(Datas.JobberJson);
IDictionary<string,IList<string>> indexes =new Dictionary<string,IList<string>>();
IDictionary<string, IUser> fastList = new Dictionary<string, IUser>();


fastList.AddToDictionary(personalUsers.Select(user=>(user as IUser)).ToList(),indexes);
fastList.AddToDictionary(studentUsers.Select(user => (user as IUser)).ToList(), indexes);
fastList.AddToDictionary(jobberUsers.Select(user => (user as IUser)).ToList(), indexes);
var findAll = FindByIndex("35102972287");
Console.WriteLine(JsonConvert.SerializeObject(findAll));
Console.ReadKey();

IList<IUser>? FindByIndex(string search)
{
  
    if (indexes.ContainsKey(search))
    {
        var findedKeys=indexes[search];
        return findedKeys.Select(key => fastList[key]).ToList();
    }
    return null;
}

