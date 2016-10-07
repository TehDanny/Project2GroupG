using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AbsenceRegistrationClient.AbsenceRegistrationService;

namespace AbsenceRegistrationClient {
    class WebController {
        //References
        private View view = new View();
        private AbsenceRegistrationService.AbsenceRegistrationClient client = new AbsenceRegistrationService.AbsenceRegistrationClient();
        private Thread autoCheckIn;
        //Properties
        private bool isTeacher;
        private static void autoCheckInCaller() {
            WebController nonStatic = new WebController();
            nonStatic.autoCheckInMethod();
        }
        /// <summary>
        /// Automatcally calls the check-in method every hour from 8 to 14.
        /// </summary>
        private void autoCheckInMethod() {
            List<DateTime> registrationList = new List<DateTime>();
            int lastHour = 7;
            Thread.Sleep(10000);
            if (DateTime.Now.Hour != lastHour) {
                checkIn();
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
            while (menu());
        }
        private void login() {
            string[] credentials = view.Login().Split(';');
            bool tryAgain = true;
            while (tryAgain) { 
                try {
                    isTeacher = client.LoginUser(credentials[0], credentials[1]);
                    tryAgain = false;
                } catch (Exception e) {
                    view.PrintErrorMessage(e.Message);
                    view.ClearScreen(0);
                }
            }
        }
        private bool menu() {
            if (isTeacher) {
                view.PrintTeacherMenu();
                switch (view.GetCommand()) {
                    case View.Command.CheckIn:
                        checkIn();
                        break;
                    case View.Command.History:
                        break;
                    case View.Command.Current:
                        break;
                    case View.Command.Quit:
                        view.PrintMessage(View.Message.ByeBye);
                        view.ClearScreen(5000);
                        return false;
                };
            }
            else {
                view.PrintStudentMenu();
                switch (view.GetCommand()) {
                    case View.Command.CheckIn:
                        checkIn();
                        break;
                    case View.Command.Quit:
                        view.PrintMessage(View.Message.ByeBye);
                        view.ClearScreen(3000);
                        return false;
                    default:
                        view.PrintMessage(View.Message.NoPrivilige);
                        break;
                }
            }
            return true;
        }
        private void checkIn(){
            try {
                client.CheckIn();
            } catch (Exception e) {
                view.PrintErrorMessage(e.Message);
                view.ClearScreen(0);
                return;
            }
            view.PrintMessage(View.Message.SuccessfulCheckIn);
            view.ClearScreen(6000);
        }

    }
}
