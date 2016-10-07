using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AbsenceRegistrationClient.AbsenceRegistrationService;

namespace AbsenceRegistrationClient {
    class WebController {
        private View view = new View();
        private AbsenceRegistrationService.AbsenceRegistrationClient client = new AbsenceRegistrationService.AbsenceRegistrationClient();
        private bool isTeacher;
        private Thread autoCheckIn;
        private List<DateTime> registrationList = new List<DateTime>();
        private static void autoCheckInCaller() {
            WebController nonStatic = new WebController();
            nonStatic.autoCheckInMethod();
        }
        /// <summary>
        /// Automatcally calls the check-in method every hour from 8 to 14.
        /// </summary>
        private void autoCheckInMethod() {
            int lastHour = 7;
            Thread.Sleep(10000);
            if (DateTime.Now.Hour != lastHour) {
                client.CheckIn();
                lastHour = DateTime.Now.Hour;
                registrationList.Add(DateTime.Now);
            }
        }

        static void Main(string[] args) {
            WebController nonStatic = new WebController();
            nonStatic.run();
        }
        private void run() {
            login();
            autoCheckIn = new Thread(new ThreadStart(autoCheckInMethod));
            menu();
        }
        private void login() {
            string[] credentials = view.Login().Split(';');
            isTeacher = client.LoginUser(credentials[0], credentials[1]);
            
        }
        private void menu() {
            if (isTeacher) {
                view.PrintTeacherMenu();
                switch (view.GetCommand()) {
                    case View.Command.CheckIn:

                        break;
                    case View.Command.History:
                        break;
                    case View.Command.Current:
                        break;
                };
            }
            else {
                view.PrintStudentMenu();
                if (view.GetCommand() == View.Command.CheckIn) {

                } else view.PrintMessage(View.Message.NoPrivilige);
            }
        }
    }
}
