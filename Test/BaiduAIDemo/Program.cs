using System;
using System.Collections.Generic;
using System.IO;

namespace BaiduAIDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 设置APPID/AK/SK
            var APP_ID = "11799753";
            var API_KEY = "fOf7AGB4Nhc7GDy9orq31VpG";
            var SECRET_KEY = "PlI8lhioiUxrhvs7lno53ZD7dbMwb1eP";

            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间

            SearchDemo(client);
            //IdcardDemo("C:\\front2.jpg", "front", client);
            IdcardDemo("C:\\front2.jpg", "front", client);
            Console.ReadKey();
        }

        public static void SearchDemo(Baidu.Aip.Ocr.Ocr client)
        {
           
            //var url = "https://img1.126.net/channel14/kanke0628/300-250.jpg";
            var url = "C:\\wzb.jpg";
            var image = File.ReadAllBytes(url);
            // 调用通用文字识别, 图片参数为远程url图片，可能会抛出网络等异常，请使用try/catch捕获
            //var result = client.GeneralBasicUrl(url);
             
            // 如果有可选参数
            var options = new Dictionary<string, object>{
        {"language_type", "CHN_ENG"},
        {"detect_direction", "true"},
        {"detect_language", "true"},
        {"probability", "true"}
    };
            var result = client.GeneralBasic(image, options);
            // 带参数调用通用文字识别, 图片参数为远程url图片
            result = client.GeneralBasicUrl(url, options);
            Console.WriteLine(result);

        }

        public static void IdcardDemo(string imagePath,string idCardSide, Baidu.Aip.Ocr.Ocr client)
        {
            var image = File.ReadAllBytes(imagePath);
            //var idCardSide = "back";

            // 调用身份证识别，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.Idcard(image, idCardSide);
            
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
        {"detect_direction", "true"},
        {"detect_risk", "false"}
    };
            // 带参数调用身份证识别
            result = client.Idcard(image, idCardSide, options);
            Console.WriteLine(result);
        }
    }
}
