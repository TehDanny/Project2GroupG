using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsenceRegistrationClient {
    class View {
        public void ClearScreen() {
            Console.Clear();
        }
        public void PrintStatus(List<DateTime> regList, DateTime nextReg) {
            Console.WriteLine("Presence status");
            foreach (DateTime reg in regList) {
                Console.WriteLine("Checked at " + reg.ToShortTimeString());
            }
            Console.WriteLine("Next registration will be at " + nextReg.ToShortTimeString());
        }
        public void PrintStudentMenu() {
            Console.WriteLine("1. Manual presence registration");
        }
        public void PrintTeacherMenu() {
            Console.WriteLine("1. Manual presence registration");
            Console.WriteLine("2. Get history of all students");
            Console.WriteLine("3. Check current presence of a student/teacher");
        }
        public enum Message {
            Success
        }
        public void PrintMessage(Message msg) {
            switch (msg) {
                case 0: Console.WriteLine("Yay!"); break;
            }
        }
        public enum Command {
            CheckIn
        }
        public Command GetCommand() {
            while (true) {
                Console.Write(">");
                switch (Console.ReadLine()) {
                    case "1": return Command.CheckIn;
                    default: Console.WriteLine("Wrong input. Try again."); break;
                }
            }
            
        }
    }
}
