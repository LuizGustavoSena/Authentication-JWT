using AuthenticationJWT.Api.VielModel;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationJWT.Api.Services
{
    public static class redisService
    {
        public static void SetRedis(loginTokenViewModel login)
        {
            using (var redisClient = new RedisClient("localhost:6379"))
            {
                redisClient.Set<loginTokenViewModel>(login.Token.ToString(), login);
            }
        }

        public static loginTokenViewModel GetRedis(string token)
        {
            using(var redisClient = new RedisClient("localhost:6379"))
            {
                return redisClient.Get<loginTokenViewModel>(token);
            }
        }
    }
}
