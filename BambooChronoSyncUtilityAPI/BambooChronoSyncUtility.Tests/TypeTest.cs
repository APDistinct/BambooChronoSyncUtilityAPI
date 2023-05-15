using BambooChronoSyncUtility.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooChronoSyncUtility.Tests
{
    public class TypeTest
    {
        private void SetBamboo(IEnumerable<IUserIdBamboo> userIdBambooList) 
        {
            int i = 0;
            foreach(var u in userIdBambooList) 
            { 
                u.UserIdBamboo = i++; u.UserName = i.ToString(); 
            }
        }
        private void SetChrono(IEnumerable<IUserIdChrono> userIdChronoList) 
        {
            int i = 10;
            foreach (var u in userIdChronoList)
            {
                u.UserIdChrono = i++;
            }
            
        }
        [Fact]
        public void Test() 
        {
            var list = new List<UserIdName>();
            for(int i = 0; i < 10; i++)
            {
                list.Add(new UserIdName() { UserIdBamboo = -1, UserIdChrono = -1, UserName = ""});
            }
            Assert.Equal(10, list.Count);
            SetBamboo(list);
            list.ForEach(u => Assert.NotEqual(-1, u.UserIdBamboo));
            list.ForEach(u => Assert.Equal(-1, u.UserIdChrono));
            SetChrono(list);
            list.ForEach(u => Assert.NotEqual(-1, u.UserIdBamboo));
            list.ForEach(u => Assert.NotEqual(-1, u.UserIdChrono));

        }
    }
}
