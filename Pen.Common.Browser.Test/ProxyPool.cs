using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Common.Browser.Test
{
    /// <summary>
    /// TODO 临时使用、需完善
    /// </summary>
    public class ProxyPool
    {
        public static List<PenProxy> proxys = new List<PenProxy>();

        public static void Init(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    var contents = sr.ReadToEnd();
                    var list = contents.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries);

                    foreach (var l in list)
                    {

                        var tmp = l.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        proxys.Add(new PenProxy
                        {
                            Url = tmp[0] + ":" + tmp[1],
                            UserName = tmp[2],
                            Password = tmp[3],
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Loging.Error("代理文件异常：" + ex.Message);
            }
        }

        public static PenProxy GetRandomProxy()
        {
            if (proxys.Count > 1)
            {
                Random ran = new Random(GetRandomSeed());
                var index = ran.Next(0, proxys.Count - 1);

                return proxys[index];
            }

            return null;
        }

        private static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }




}
