using System;

namespace sharp_lb1
{
    class Program
    {
        static void Main(string[] args)
        {
            //services[] serv = new services[2] { services.UnliumVideo, services.UnliumMessenger };
            MobileAccount account = new MobileAccount(taryfs.Basic, 2, 100, 0957845654);
            int user_choise;
            do
            {
                Console.WriteLine("Оберiть дiю:\n" +
                    "1. Змiнити тариф\n" +
                    "2. Зробити дзвiнок\n" +
                    "3. Пiдключити послугу\n" +
                    "4. Вiдключити послугу\n" +
                    "5. Поповнити баланс\n" +
                    "6. Дiзнатись iсторiю викликiв\n" +
                    "7. Перевiрити баланс\n" +
                    "8. Дiзнатись поточний тариф\n" +
                    "0. Закрити програму\n");
                user_choise = int.Parse(Console.ReadLine());
                try
                {
                    switch (user_choise)
                    {
                        case 1:
                            Console.WriteLine("Доступнi тарифи:\n" +
                                "1. Basic\n" +
                                "2. Advanced\n" +
                                "3. Unlium\n");
                            int new_taryf_choise = int.Parse(Console.ReadLine());
                            account.Change_Taryf((taryfs)new_taryf_choise - 1);
                            Console.WriteLine("Тариф успiшно змiнено!\n");
                            break;

                        case 2:
                            Console.WriteLine("Введiть номер телефону, на який " +
                                "хочете здiйснити дзвiнок та кiлькiсть хвилин у розмовi.");

                            Console.Write("Номер телефону: ");
                            long new_outgoing_number = long.Parse(Console.ReadLine());
                            Console.Write("К-сть хвилин: ");
                            double mins_in_new_call = double.Parse(Console.ReadLine());
                            Console.WriteLine($"Вартість дзвінка: {account.Make_Call(mins_in_new_call, new_outgoing_number)}");
                            break;

                        case 3:
                            Console.WriteLine("Оберiть послугу, яку хочете пiдключити. Введiть цифру вiд 1 до 4:");
                            foreach (var item in Enum.GetValues(typeof(services)))
                            {
                                Console.WriteLine(item);
                            }
                            int service_to_connect = int.Parse(Console.ReadLine());

                            switch (service_to_connect)
                            {
                                case 1:
                                    account.Connect_Service(services.UnliumMessenger);
                                    Console.WriteLine($"Послугу пiдключено! З вашого балансу знято {(int)services.UnliumMessenger} гривень.");
                                    break;

                                case 2:
                                    account.Connect_Service(services.UnliumVideo);
                                    Console.WriteLine($"Послугу пiдключено! З вашого балансу знято {(int)services.UnliumVideo} гривень.");
                                    break;

                                case 3:
                                    account.Connect_Service(services.ExtraInternet);
                                    Console.WriteLine($"Послугу пiдключено! З вашого балансу знято {(int)services.ExtraInternet} гривень.");
                                    break;

                                case 4:
                                    account.Connect_Service(services.ExtraMinutes);
                                    Console.WriteLine($"Послугу пiдключено! З вашого балансу знято {(int)services.ExtraMinutes} гривень.");
                                    break;
                            }
                            break;

                        case 4:
                            Console.WriteLine("Оберiть послугу, яку хочете вiдключити:");
                            for (int i = 0; i < account.Get_Connected_Services.Length; i++)
                            {
                                Console.WriteLine($"{(i + 1)}. {account.Get_Connected_Services[i]}");
                            }
                            int service_to_disconnect = int.Parse(Console.ReadLine());
                            account.Disconnect_Service(service_to_disconnect - 1);
                            Console.WriteLine("Послуга успiшно вiдключена!\n");
                            break;

                        case 5:
                            Console.Write("Введiть суму поповнення: ");
                            double replanish_amount = double.Parse(Console.ReadLine());
                            account.Replenish_Account(replanish_amount);
                            Console.WriteLine("Баланс успiшно поповнено!");
                            break;

                        case 6:
                            Console.WriteLine("Вашi останнi виклики:");
                            for (int i = 0; i < account.Get_History.Length; i++)
                            {
                                Console.WriteLine($"{(i + 1)}: {account.Get_History[i]}");
                            }
                            break;

                        case 7:
                            Console.WriteLine($"Ваш поточний рахунок: {account.Get_Balance} гривень.");
                            break;

                        case 8:
                            Console.WriteLine($"Ваш поточний тариф: {account.Get_Taryf}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }

                //Console.Clear();
            } while (user_choise > 0);
        }
    }
}