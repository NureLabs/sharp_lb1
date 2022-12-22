using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharp_lb1
{
    enum taryfs
    {
        Basic,
        Advanced,
        Unlium
    }
    enum services
    {
        UnliumMessenger = 40,
        UnliumVideo = 10,
        ExtraInternet = 73,
        ExtraMinutes = 50
    }

    interface IAccount
    {
        public void Change_Taryf(taryfs new_taryf);
        public double Make_Call(double amount_of_minites_in_call, long outgoing_number);
        public void Connect_Service(services service_to_connect);
        public void Disconnect_Service(int service_to_disconnect);
        public void Replenish_Account(double replanish_amount);
    }
    class MobileAccount : IAccount
    {
        private taryfs current_taryf;
        private long[] call_history;

        private double one_minute_cost;
        private double balance;
        private int phone_number;
        private services[] connected_services; //list service

        public MobileAccount()
        {
            current_taryf = taryfs.Basic;
            call_history = new long[0];

            one_minute_cost = 1;
            balance = 0;
            phone_number = 0951234567;
            connected_services = new services[0];
        }
        public MobileAccount(taryfs current_taryf, double one_minute_cost, double balance, int phone_number)
        {
            this.current_taryf = current_taryf;
            call_history = new long[0];

            this.one_minute_cost = one_minute_cost;
            this.balance = balance;
            this.phone_number = phone_number;
            connected_services = new services[0];
        }

        public void Change_Taryf(taryfs new_taryf)
        {
            current_taryf = new_taryf;
        }

        public double Make_Call(double amount_of_minites_in_call, long outgoing_number)
        {
            double call_cost = amount_of_minites_in_call * one_minute_cost;
            if (balance < call_cost)
            {
                throw new ArgumentException($"Недостатньо коштiв! Поповнiть рахунок на {call_cost} гривень.");
            }
            else
            {
                balance -= call_cost;
                Array.Resize(ref call_history, call_history.Length + 1);
                call_history[call_history.Length - 1] = outgoing_number;
                return call_cost;
            }
        }

        public void Connect_Service(services service_to_connect)
        {
            if (balance < (int)service_to_connect)
            {
                throw new ArgumentException($"Недостатньо коштiв! Поповнiть рахунок на {(int)service_to_connect} гривень.");
            }
            Array.Resize(ref connected_services, connected_services.Length + 1);
            connected_services[connected_services.Length - 1] = service_to_connect;
            balance -= (int)service_to_connect;
        }
        public void Disconnect_Service(int service_to_disconnect)
        {
            if (connected_services.Length > 0)
            {
                for (int i = service_to_disconnect; i < connected_services.Length - 1; i++)
                {
                    connected_services[i] = connected_services[i + 1];
                }
                Array.Resize(ref connected_services, connected_services.Length - 1);
            }
            else
            {
                throw new ArgumentException("Вiдсутнi пiдключенi послуги!");
            }
        }

        public void Replenish_Account(double replanish_amount)
        {
            balance += replanish_amount;
        }

        public double Get_Balance
        {
            get { return balance; }
        }
        public long[] Get_History
        {
            get { return call_history; }
        }
        public services[] Get_Connected_Services
        {
            get { return connected_services; }
        }
        public taryfs Get_Taryf
        {
            get { return current_taryf; }
        }
    }
}