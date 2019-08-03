using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using EmpApp2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EmpApp2.Repositories
{

    public interface IEmpRepository
    {
        int SaveEmps(List<Emps> emps);
        List<Emps> InitEmps(int? cnt);
        List<Emps> LoadEmps();

        bool CheckAdmin(ClaimsPrincipal user);
        bool CheckUser(ClaimsPrincipal user);
        string GetPositions(string position);
        string GetJSON(List<Emps> emps, string src);

    }
    public class EmpRepository : IEmpRepository
    {
        //readonly string initFile = "InitialEmps.json";
        readonly string prodFile = "EmpsDB.json";

        readonly List<string> positions = new List<string> {
            #region POSITIONS
            "Бухгалтер",
            "Генеральный директор",
            "Главный бухгалтер",
            "Дворник",
            "Диспетчер",
            "Заведующий складом",
            "Инженер",
            "Менеджер по продажам",
            "Программист",
            "Юрисконсульт",
            #endregion
        };

        readonly List<string> firstNamesM = new List<string>
        {
            #region NAMES_M
            "Александр",
            "Дмитрий",
            "Максим",
            "Сергей",
            "Андрей",
            "Алексей",
            "Артём",
            "Илья",
            "Кирилл",
            "Михаил",
            "Никита",
            "Матвей",
            "Роман",
            "Егор",
            "Арсений",
            "Иван",
            "Денис",
            "Евгений",
            "Даниил",
            "Тимофей",
            "Владислав",
            "Игорь",
            "Владимир",
            "Павел",
            "Руслан",
            "Пётр",
#endregion
        };

        readonly List<string> firstNamesF = new List<string>
        {
            #region NAMES_F
            "Анастасия",
            "Анна",
            "Мария",
            "Елена",
            "Дарья",
            "Алина",
            "Ирина",
            "Екатерина",
            "Арина",
            "Полина",
            "Ольга",
            "Юлия",
            "Татьяна",
            "Наталья",
            "Виктория",
            "Елизавета",
            "Ксения",
            "Милана",
            "Вероника",
            "Алиса",
            "Валерия",
            "Александра",
            "Ульяна",
            "Кристина",
            "София",
            #endregion
        };

        readonly List<string> givenNamesM = new List<string>
        {
            #region GIVEN_M
            "Артёмович",
            "Ильич",
            "Кириллович",
            "Михаилович",
            "Никитович",
            "Матвеевич",
            "Романович",
            "Егорович",
            "Арсениевич",
            "Александрович",
            "Дмитриевич",
            "Максимович",
            "Сергеевич",
            "Андреевич",
            "Алексеевич",
            "Иванович",
            "Денисович",
            "Евгениевич",
            "Даниилович",
            "Тимофеевич",
            "Владиславович",
            "Игоревич",
            "Владимирович",
            "Павлович",
            "Русланович",
            "Романович",
            "Тимофеевич",
#endregion
        };

        readonly List<string> givenNamesF = new List<string>
        {
            #region GIVEN_F
            "Романовна",
            "Егоровна",
            "Сергеевна",
            "Арсениевна",
            "Ивановна",
            "Денисовна",
            "Евгениевна",
            "Данииловна",
            "Евгениевна",
            "Тимофеевна",
            "Владиславовна",
            "Игоревна",
            "Владимировна",
            "Павловна",
            "Игоревна",
            "Руслановна",
            "Александровна",
            "Дмитриевна",
            "Максимовна",
            "Сергеевна",
            "Андреевна",
            "Алексеевна",
            "Артёмовна",
            "Ильинична",
            "Кирилловна",
            "Ивановна",
            "Михайловна",
            "Никитична",
            "Матвеевна",
            #endregion
        };

        readonly List<string> lastNamesM = new List<string>
        {
            #region LASTNAMES
            "Воробьев",
            "Богданов",
            "Виноградов",
            "Голубев",
            "Семенов",
            "Павлов",
            "Зайцев",
            "Васильев",
            "Соловьев",
            "Волков",
            "Петров",
            "Морозов",
            "Новиков",
            "Козлов",
            "Лебедев",
            "Соколов",
            "Попов",
            "Кузнецов",
            "Иванов",
            "Смирнов",
#endregion
        };



        public int SaveEmps(List<Emps> emps)
        {

            string path = prodFile;

            for (int i = 0; i < emps.Count; i++)
            {
                emps[i].ID = i + 1;
            }
            
            try
            {
                string src = JsonConvert.SerializeObject(emps);

                File.WriteAllText(path, src);
            }
            catch (Exception e)
            {
                return e.HResult;
            }

            return 0;
        }


        public List<Emps> InitEmps(int? cnt)
        {
            List<Emps> emps = new List<Emps>();

            if (cnt == null)
            {
                cnt = 50;
            }

            for (int i = 1; i <= cnt; i++)
            {
                Emps emp = new Emps();
                emp.ID = i;
                emp.Gender = (i % 2) == 0 ? "М" : "Ж";
                if (emp.Gender == "М")
                {
                    emp.FirstName = firstNamesM[i % firstNamesM.Count]; // "Имя" + i.ToString();
                    emp.GivenName = givenNamesM[i % givenNamesM.Count]; // "Отчество" + i.ToString();
                    emp.LastName = lastNamesM[i % lastNamesM.Count];    // "Фамилия" + i.ToString();
                }
                else
                {
                    emp.FirstName = firstNamesF[i % firstNamesF.Count]; // "Имя" + i.ToString();
                    emp.GivenName = givenNamesF[i % givenNamesF.Count]; // "Отчество" + i.ToString();
                    emp.LastName = lastNamesM[(i + 10) % lastNamesM.Count] + "а";    // "Фамилия" + i.ToString();
                }
                emp.Position = positions[(i + 3) % 10]; //"Должность" + i.ToString();
                int yr;
                switch (i % 8)
                {
                    case 1: yr = 1941; break;
                    case 2: yr = 1951; break;
                    case 3: yr = 1962; break;
                    case 4: yr = 1975; break;
                    case 5: yr = 1980; break;
                    case 6: yr = 1999; break;
                    case 7: yr = 2003; break;
                    default: yr = 2005; break;
                }
                emp.BirthDate = DateTime.ParseExact(yr.ToString() + "-" + (((i+9) % 12)+1).ToString().PadLeft(2,'0') +
                    "-" + (((i+17) % 27)+1).ToString().PadLeft(2, '0'), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                emp.Phone = "+7(90" + (i % 10).ToString() + ")" + (i % 5).ToString() + (i % 10).ToString() + (i % 7).ToString() + "-" + (i % 8).ToString() + (i % 3).ToString() + "-" + (i % 4).ToString() + (i % 9).ToString();

                emp.ROWID = Guid.NewGuid().ToString(); // emp.ID.ToString() + emp.FirstName + emp.GivenName + emp.LastName + emp.BirthDate.ToString("ddMMyyyy") + emp.Position;

                emps.Add(emp);


            }



            return emps;
        }

        public List<Emps> LoadEmps()
        {
            string path = prodFile;
            bool readed = false;
            List<Emps> emps = new List<Emps>();

            try
            {
                StreamReader streamReader = File.OpenText(path);
                string src = streamReader.ReadToEnd();
                streamReader.Close();

                //List<Emps> emps2 = 
                
                emps = JsonConvert.DeserializeObject<List<Emps>>(src);

                readed = true;
            }
            catch
            {
                readed = false;
            }

            if (!readed || emps.Count == 0)
            {
                emps = InitEmps(null);
                if (SaveEmps(emps) != 0)
                {
                    return new List<Emps>();
                }

            }

            return emps;

        }

        public bool CheckAdmin(ClaimsPrincipal user)
        {
            #region AUTH
            //string appPrincipalID = "62b4b0eb-ef3e-4c28-7777-2c7777776593";
            //string appKey = "AAAAAAAAAAAAAAAAAAAAAAAAAAAA/Q8=";

            //string upn = incomingPrincipal.FindFirst(
            //  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn").Value;
            //string tenantID = incomingPrincipal.FindFirst(
            //           "http://schemas.microsoft.com/ws/2012/10/identity/claims/tenantid").Value;

            //string requestUrl =
            //         string.Format("https://graph.windows.net/{0}/Users('{1}')/MemberOf",
            //                       "da603b9b-e4f1-4e9a-81af-dd9bb3418dab",
            //                       user.Identity.Name);


            //HttpWebRequest webRequest =
            //      WebRequest.Create(requestUrl) as HttpWebRequest;
            //webRequest.Method = "GET";
            ////webRequest.PreAuthenticate = true;
            //webRequest.Headers["Authorization"] = ;
            //webRequest.Headers["x-ms-dirapi-data-contract-version"] = "0.8";
            //string jsonText;
            //var httpResponse = (HttpWebResponse)webRequest.GetResponse();
            //using (var streamReader =
            //         new StreamReader(httpResponse.GetResponseStream()))
            //{
            //    jsonText = streamReader.ReadToEnd();
            //}
            //JObject jsonResponse = JObject.Parse(jsonText);
            //var roles =
            //    from r in jsonResponse["d"]["results"].Children()
            //    select (string)r["DisplayName"];

            //// add a role claim for every membership found
            ////foreach (string s in roles)
            ////    ((ClaimsIdentity)user.Identity).AddClaim(
            ////            new Claim(ClaimTypes.Role, s, ClaimValueTypes.String, "GRAPH"));

            //bool ret = false;


            // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-2.2
            #endregion

            if (user.Identity.Name == "emp-admin@ru-memorials.ru" )
            {
                return true;
            }

            return false;
        }
        public bool CheckUser(ClaimsPrincipal user)
        { 
            //temporary hardcode. Correct way is checking AzureAD group. 
            if ((user.Identity.Name == "emp-admin@ru-memorials.ru") || (user.Identity.Name == "emp-user@ru-memorials.ru"))
            {
                return true;
            }

            return false;

        }

        public string GetPositions(string position)
        {
            string result = String.Empty;
            foreach (string pos in positions)
            {
                result += " <option ";
                if (position == pos)
                {
                    result += "selected";
                }
                result += ">" + pos + "</option>";
            }

            return result;
        }

         

        public string GetJSON(List<Emps> emps, string src)
        {
            string result = String.Empty;

            Dictionary<string, int> arr = new Dictionary<string, int>();

            if (src == "DECADES")
            {
                arr.Add("1940", 0);
                arr.Add("1950", 0);
                arr.Add("1960", 0);
                arr.Add("1970", 0);
                arr.Add("1980", 0);
                arr.Add("1990", 0);
                arr.Add("2000", 0);

                //arr["2000"] = arr["2000"] + 1;


            }

            if (src == "GENDERS")
            {
                arr.Add("М", 0);
                arr.Add("Ж", 0);
            }

            foreach (Emps emp in emps)
            {
                string key = String.Empty;

                if (src == "DECADES")
                {
                    key = emp.BirthDate.ToString("yyyy").Substring(0, 3) + "0";
                }


                if (src == "GENDERS")
                {
                    key = emp.Gender;
                }


                arr[key] = arr[key] + 1;

            }

            result = "[";
            for(int i = 0; i < arr.Count; i++)
            {
                result += "{name: '" + arr.Keys.ElementAt(i) + "',";
                result += "y: " + arr[arr.Keys.ElementAt(i)].ToString();
                if (i == 0)
                {
                    result += ", sliced: true,selected: true";
                }
                result += "},";
            }

            result += "]";
            /*
                [
                {
                name: 'Male',
                y: 61.41,
                sliced: true,
                selected: true
            }
            , 
            {
                name: 'Female',
                y: 11.84
            }
            
            ]
             
             */




            return result;
        }

    }
    
}
