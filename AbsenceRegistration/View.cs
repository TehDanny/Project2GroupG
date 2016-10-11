using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsenceRegistrationClient {
    class View {
        /// <summary>
        /// Clears the whole console after a given time or keypress from user.
        /// </summary>
        /// <param name="waitTime"> In milliseconds. Pass 0 to manual clear. </param>
        public void PrintStatus(List<DateTime> regList) {
            Console.WriteLine("Presence status");
            foreach (DateTime reg in regList) {
                Console.WriteLine("Checked at " + reg.ToShortTimeString());
            }
            if (regList.Last().Hour == 14) Console.WriteLine("Registration is done for today. Next registration will be tomorrow.");
            else Console.WriteLine("Next registration will be at " + regList.Last().Hour+1);
        }
        public void PrintStudentMenu() {
            Console.WriteLine("1. Manual presence registration\n");
        }
        public void PrintTeacherMenu() {
            Console.WriteLine("1. Manual presence registration");
            Console.WriteLine("2. Get history of all students");
            Console.WriteLine("3. Check current presence of a student/teacher\n");
        }
        /// <summary>
        /// Gets user to login
        /// </summary>
        /// <returns> A string containing both username and password with a ';' separated. </returns>
        public string Login() {
            string credentials = string.Empty;
            Console.WriteLine("Login");
            Console.Write("Username: ");
            credentials += Console.ReadLine();
            Console.Write("Password: ");
            credentials += ";" + Console.ReadLine();
            return credentials;
        }
        public string Register() {
            string userInfo = string.Empty;
            Console.WriteLine("Register");
            Console.Write("Email: ");
            userInfo += Console.ReadLine();
            Console.Write("First name: ");
            userInfo += ";" + Console.ReadLine();
            Console.Write("Surname: ");
            userInfo += ";" + Console.ReadLine();
            Console.Write("Email: ");
            userInfo += ";" + Console.ReadLine();
            Console.Write("Password: ");
            userInfo += ";" + Console.ReadLine();
            Console.Write("Confirm password: ");
            userInfo += ";" + Console.ReadLine();
            return userInfo;
        }
        public void PrintHistoryDay(string dayName, List<string> emails) {
            Console.WriteLine(dayName + ":");
            foreach(string email in emails) {
                Console.WriteLine("- " + email); 
            }
            Console.WriteLine();
        }
        public void PrintHistoryTopListLine(string email, int count) {
            Console.WriteLine(email + " - " + count);
        }
        public enum Message {
            Start,
            Disclaimer,
            NoPrivilige,
            WrongInput,
            SuccessfulCheckIn,
            ByeBye
        }
        public void PrintMessage(Message msg) {
            switch (msg) {
                case View.Message.Start:
                    Console.WriteLine("Would you like to register? (y/n)");
                    Console.WriteLine("You'll be forwarded to login if no.");
                    break;
                case Message.Disclaimer:
                    Console.WriteLine("DISCLAIMER: This program checks and sends your MAC/IP address to the server.");
                    Console.WriteLine("Only with the purpose of checking your location,");
                    Console.WriteLine("therefore approving the legitimacy of your actions.");
                    break;
                case Message.NoPrivilige: Console.WriteLine("You have no privilige to pick a teacher's feature.!"); break;
                case Message.ByeBye: Console.WriteLine("Quitting the program..."); break;
                case Message.SuccessfulCheckIn: Console.WriteLine("Check-in was successful!"); break;
                case Message.WrongInput: Console.WriteLine("Wrong input. Try again!"); break;
            }
            ClearScreen();
        }
        public enum Command {
            Login,
            Register,
            CheckIn,
            History,
            Current,
            Quit
        }
        public Command GetCommand() {
            while (true) {
                Console.Write(">");
                switch (Console.ReadLine()) {
                    case "0": return Command.Quit;
                    case "1": return Command.CheckIn;
                    case "2": return Command.History;
                    case "3": return Command.Current;
                    case "y": return Command.Register;
                    case "n": return Command.Login;
                    default: PrintMessage(Message.WrongInput); break;
                }
            }
        }
        public void PrintErrorMessage(string errorMessage) {
            Console.WriteLine(errorMessage);
            ClearScreen();
        }
        public void ClearScreen() {
            Console.ReadKey();
            Console.Clear();
        }
    }
}
