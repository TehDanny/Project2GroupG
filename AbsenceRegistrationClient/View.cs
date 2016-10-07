using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsenceRegistrationClient {
    class View {
        /// <summary>
        /// Clears the whole console after a given time.
        /// </summary>
        /// <param name="waitTime"> In milliseconds. </param>
        public void ClearScreen(int waitTime) {
            System.Threading.Thread.Sleep(waitTime);
            Console.Clear();
        }
        public void PrintStatus(List<DateTime> regList) {
            Console.WriteLine("Presence status");
            foreach (DateTime reg in regList) {
                Console.WriteLine("Checked at " + reg.ToShortTimeString());
            }
            if (regList.Last().Hour == 14) Console.WriteLine("Registration is done for today. Next registration will be tomorrow.");
            else Console.WriteLine("Next registration will be at " + regList.Last().Hour+1);
        }
        public void PrintStudentMenu() {
            Console.WriteLine("1. Manual presence registration");
        }
        public void PrintTeacherMenu() {
            Console.WriteLine("1. Manual presence registration");
            Console.WriteLine("2. Get history of all students");
            Console.WriteLine("3. Check current presence of a student/teacher");
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
            //Should we check stuff?
            //Should we care about encrypting the password?
            return credentials;
        }
        public enum Message {
            NoPrivilige
        }
        public void PrintMessage(Message msg) {
            switch (msg) {
                case 0: Console.WriteLine("You!"); break;
            }
        }
        public enum Command {
            CheckIn,
            History,
            Current
        }
        public Command GetCommand() {
            while (true) {
                Console.Write(">");
                switch (Console.ReadLine()) {
                    case "1": return Command.CheckIn;
                    case "2": return Command.History;
                    case "3": return Command.Current;
                    default: Console.WriteLine("Wrong input. Try again."); break;
                }
            }
        }
        public void PrintString(string msg) {
            Console.WriteLine(msg);
        }
    }
}
