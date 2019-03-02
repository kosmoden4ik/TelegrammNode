using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using Telegram;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApp1
{
    class Program
    {
        static TelegramBotClient botClient;

        static void Main(string[] args)
        {
            try
            {
                botClient = new TelegramBotClient("798990333:AAHZgOc9Bk4XZUJt9zsmIGD9YTK9VL3hWqM");
                var bot = botClient.GetMeAsync().Result;
                Console.WriteLine(bot.Username);

                botClient.OnMessage += getMessage;
                botClient.OnCallbackQuery += getCallBack;
                botClient.StartReceiving();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }

        private static async void getCallBack(object sender, CallbackQueryEventArgs e)
        {
            await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"You send to me\t\t {e.CallbackQuery.Message.Text}");

            //   await botClient.EditMessageReplyMarkupAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId, null);
        }

        private static void getMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
                switch (e.Message.Text.ToLower())
                {
                    case "/start":
                        botClient.SendPhotoAsync(e.Message.Chat.Id, "https://drive.google.com/open?id=1uHdGP7xPEH3IM90e-wkN8m9kTEN6qFDZ");
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, getUserInfo(e.Message));
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, "Добро пожаловать в мир Дикого Севера");

                        break;
                    case "/weather":
                        var markupchik = new ReplyKeyboardMarkup(new[]{
                                    new KeyboardButton("/kiev"),

                                    new KeyboardButton("/dnipro"),
                                    new KeyboardButton("/lviv"),
                                    new KeyboardButton("/kharkiv"),
                                  //  new KeyboardButton("/odessa"),
                                   // new KeyboardButton("/rivne"),
                                 //   new KeyboardButton("/ternopol"),

                                });

                        markupchik.OneTimeKeyboard = true;
                        // markupchikq.OneTimeKeyboard = true;
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, e.Message.Text, replyMarkup: markupchik);

                        // botClient.SendTextMessageAsync(e.Message.Chat.Id, get_weather());
                        break;
                    case "/date":
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, DateTime.Now.ToString());
                        break;
                    case "/kiev":
                        botClient.SendTextMessageAsync(e.Message.Chat.Id,"Kiev");
                        break;
                    case "/dnipro":
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, "Dnipro");
                        break;
                    case "/lviv":
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, "Lviv");
                        break;
                    case "/kharkiv":
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, "Kharkiv");
                        break;
                    case "/odessa":
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, "Odessa");
                        break;
                    case "/rivne":
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, "Rivne");
                        break;
                    case "/ternopol":
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, "Ternopil");
                        break;
                    case "/reply":
                        var markup = new ReplyKeyboardMarkup(new[]{
                                    new KeyboardButton("/weather"),
                                    new KeyboardButton("/date")
                                });
                        markup.OneTimeKeyboard = true;
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, e.Message.Text, replyMarkup: markup);
                        break;
                    case "/inline":
                        InlineKeyboardButton b = new InlineKeyboardButton();
                        InlineKeyboardButton c = new InlineKeyboardButton();
                        b.Text = "weather";
                        b.CallbackData = "/weather";
                        c.Text = "Date";
                        c.CallbackData = "/date";

                        var markup1 = new InlineKeyboardMarkup(new[] {
                           b
                        });
                        var markup2 = new InlineKeyboardMarkup(new[] {
                           c
                        });
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, b.CallbackData, replyMarkup: markup1);
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, c.CallbackData, replyMarkup: markup2);
                        //   botClient.SendTextMessageAsync(e.Message.Chat.Id, c.CallbackData, replyMarkup: markup1);

                        break;
                    default:
                        botClient.SendTextMessageAsync(e.Message.Chat.Id, e.Message.Text);


                        break;
                }
            else
                //botClient.SendTextMessageAsync(e.Message.Chat.Id,$"Type '{getMessageType(e.Message)}' is not supporting!");
                //botClient.SendTextMessageAsync(e.Message.Chat.Id, $"Type '{e.Message.Type.ToString()}' is not supporting!");
                botClient.SendPhotoAsync(e.Message.Chat.Id, "https://images.techhive.com/images/article/2016/12/error-100700406-large.jpg", $"??Type '{e.Message.Type.ToString()}' is not supporting!");


            /*
             * смайлы брать тут
             * https://ru.piliapp.com/facebook-symbols/
             */
        }

        /// <summary>
        /// getting info about this user
        /// </summary>
        /// <param name="chat">User chat ident</param>
        /// <returns></returns>
        private static string getUserInfo(Message chat)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Я свободолюбивый эльф Добби\t\t");
            sb.Append(chat.Date.ToShortDateString());
            sb.Append("\n");
            sb.Append(chat.Chat.Username);
            sb.Append("Мой хозяин - ");
            if (chat.Chat.FirstName != null)
            {
                sb.Append(chat.Chat.FirstName);
                sb.Append("\t");
            }
            if (chat.Chat.LastName != null)
                sb.Append(chat.Chat.LastName);

            return sb.ToString();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
      
        /////////////////////////////////////////////////////////////////////////////////////////
        //private static string getMessageType(Message msg)
        //{
        //    if (msg.Video != null) return "Video";
        //    if (msg.Voice != null) return "Voice";
        //    if (msg.Photo != null) return "Photo";
        //    if (msg.Document != null) return "Document";

        //    return "Text";
        //}
    }
}
