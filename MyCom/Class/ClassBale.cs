using BaleBotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaleBotNet.Methods;
using BaleBotNet.Types;

namespace MyCom.Class
{
    public class ClassBale
    {
        private readonly string _tokenBot;
        private readonly string _usernameChanel;
        BaleBotClient _bot;

        public class ModelMsg
        {
            public string Msg { get; set; }
            public string AddressImage { get; set; }

        }

        public ClassBale(string tokenBot, string usernameChanel)
        {
            _tokenBot = tokenBot;
            _usernameChanel = usernameChanel;
            _bot = new BaleBotClient(_tokenBot);
        }

        public async Task<Message> SendMessage(ModelMsg modelMsg)
        {
            //await _bot.SendMessage("1474053162".ToString(), "✅ پیام تست با Chat ID ذخیره شده!");
            var result = await _bot.SendMessage(
                chatId: _usernameChanel, // Chat ID کانال
                text: modelMsg.Msg // متن پیام
            );
            return result;
        }

        public async Task SendPhoto(ModelMsg modelMsg)
        {
            var message = await _bot.SendPhoto(
                chatId: _usernameChanel,
                fileInfo: new FileInfo(modelMsg.AddressImage),
                caption: modelMsg.Msg

            );
            try
            {
                await _bot.Close(true);
            }
            catch (Exception e)
            {
                
            }
        }

    }
}