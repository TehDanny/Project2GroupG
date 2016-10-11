using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AbsenceRegistration.ARservice;

namespace AbsenceRegistrationClient {
    class WebController {
        //References
        private View view = new View();
        private AbsenceRegistration.ARservice.AbsenceRegistrationClient client = new AbsenceRegistration.ARservice.AbsenceRegistrationClient();
        private Thread autoCheckIn;
        //Properties
        private bool isTeacher;
        private bool tryAgain = true;
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
            view.PrintMessage(View.Message.Disclaimer);
            view.PrintMessage(View.Message.Start);
            tryAgain = true;
            while (tryAgain) {
                switch (view.GetCommand()) {
                    case View.Command.Login: login(); tryAgain = false; break;
                    case View.Command.Register: register(); tryAgain = false; break;
                    default: view.PrintMessage(View.Message.WrongInput); break;
                }
            }
            autoCheckIn = new Thread(new ThreadStart(autoCheckInMethod));
            while (menu()) ;
        }
        private void register() {
            tryAgain = true;
            while (tryAgain) {
                try {
                    string[] userInfo = view.Register().Split(';');
                    client.CreateUser(userInfo[0], userInfo[1], userInfo[2], userInfo[3], userInfo[4]);
                    tryAgain = false;
                } catch (Exception e) {
                    view.PrintErrorMessage(e.Message);
                }
            }
        }
        private void login() {
            tryAgain = true;
            while (tryAgain) {
                try {
                    string[] credentials = view.Login().Split(';');
                    isTeacher = client.LoginUser(credentials[0], credentials[1]);
                    tryAgain = false;
                } catch (Exception e) {
                    view.PrintErrorMessage(e.Message);
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
                        history();

                        break;
                    case View.Command.Current:
                        break;
                    case View.Command.Quit:
                        view.PrintMessage(View.Message.ByeBye);
                        return false;
                    default:
                        view.PrintMessage(View.Message.WrongInput);
                        break;
                };
            } else {
                view.PrintStudentMenu();
                switch (view.GetCommand()) {
                    case View.Command.CheckIn:
                        checkIn();
                        break;
                    case View.Command.Quit:
                        view.PrintMessage(View.Message.ByeBye);
                        return false;
                    case View.Command.History:
                        view.PrintMessage(View.Message.NoPrivilige);
                        break;
                    case View.Command.Current:
                        view.PrintMessage(View.Message.NoPrivilige);
                        break;
                    default:
                        view.PrintMessage(View.Message.WrongInput);
                        break;
                }
            }
            return true;
        }
        private void history() {
            UserPresence[] history = client.GetAllUsersHistory();
            DateTime start = history[0].dt;
            DateTime end = history[0].dt;
            foreach(UserPresence item in history) {
                if (start.Date > item.dt.Date) {
                    start = item.dt;
                }
                if (end.Date < item.dt.Date) {
                    end = item.dt;
                }
            }
            string dayName = string.Empty;
            List<string> emails = new List<string>();
            List<Tuple<string, int>> topList = new List<Tuple<string, int>>();
            Tuple<string, int> refTL = new Tuple<string, int>("",0);
            for (var date = start.Date; date <= end.Date; date.AddDays(1)) {
                dayName = date.DayOfWeek.ToString();
                emails = new List<string>();
                foreach(UserPresence historyItem in history) {
                    if (historyItem.dt.Date == date) {
                        if (!emails.Contains(historyItem.email)) emails.Add(historyItem.email);
                        bool found = false;
                        foreach(var topListItem in topList) {
                            if (topListItem.Item1 == historyItem.email) {
                                found = true;
                                refTL = topListItem;
                            }
                        }
                        if (!found) topList.Add(Tuple.Create(historyItem.email,1));
                        else refTL = Tuple.Create(refTL.Item1, refTL.Item2 + 1);
                    }
                }
                view.PrintHistoryDay(dayName, emails);
            }
            for (int i = 1; i < topList.Count; ++i) {
                for (int j = topList.Count-1; j >= i; --j) {
                    if (topList[j].Item2 > topList[j - 1].Item2) {
                        refTL = topList[j];
                        topList[j] = topList[j-1];
                        topList[j - 1] = refTL;
                    }
                }
            }
            foreach(var topListItem in topList) {
                view.PrintHistoryTopListLine(topListItem.Item1, topListItem.Item2);
            }
            view.ClearScreen();
        }
        private void checkIn() {
            try {
                client.CheckIn();
            } catch (Exception e) {
                view.PrintErrorMessage(e.Message);
                return;
            }
            view.PrintMessage(View.Message.SuccessfulCheckIn);
        }

    }
}
