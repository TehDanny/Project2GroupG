﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AbsenceRegistration.ARservice {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserPresence", Namespace="http://schemas.datacontract.org/2004/07/AbsenceRegistrationService")]
    [System.SerializableAttribute()]
    public partial class UserPresence : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime dtField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string emailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ipField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string macField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime dt {
            get {
                return this.dtField;
            }
            set {
                if ((this.dtField.Equals(value) != true)) {
                    this.dtField = value;
                    this.RaisePropertyChanged("dt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string email {
            get {
                return this.emailField;
            }
            set {
                if ((object.ReferenceEquals(this.emailField, value) != true)) {
                    this.emailField = value;
                    this.RaisePropertyChanged("email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ip {
            get {
                return this.ipField;
            }
            set {
                if ((object.ReferenceEquals(this.ipField, value) != true)) {
                    this.ipField = value;
                    this.RaisePropertyChanged("ip");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string mac {
            get {
                return this.macField;
            }
            set {
                if ((object.ReferenceEquals(this.macField, value) != true)) {
                    this.macField = value;
                    this.RaisePropertyChanged("mac");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ARservice.IAbsenceRegistration")]
    public interface IAbsenceRegistration {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAbsenceRegistration/CreateUser", ReplyAction="http://tempuri.org/IAbsenceRegistration/CreateUserResponse")]
        bool CreateUser(string email, string fisrtname, string surname, string password, string confirmPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAbsenceRegistration/CreateUser", ReplyAction="http://tempuri.org/IAbsenceRegistration/CreateUserResponse")]
        System.Threading.Tasks.Task<bool> CreateUserAsync(string email, string fisrtname, string surname, string password, string confirmPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAbsenceRegistration/LoginUser", ReplyAction="http://tempuri.org/IAbsenceRegistration/LoginUserResponse")]
        bool LoginUser(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAbsenceRegistration/LoginUser", ReplyAction="http://tempuri.org/IAbsenceRegistration/LoginUserResponse")]
        System.Threading.Tasks.Task<bool> LoginUserAsync(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAbsenceRegistration/CheckIn", ReplyAction="http://tempuri.org/IAbsenceRegistration/CheckInResponse")]
        void CheckIn(string ip, string mac);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAbsenceRegistration/CheckIn", ReplyAction="http://tempuri.org/IAbsenceRegistration/CheckInResponse")]
        System.Threading.Tasks.Task CheckInAsync(string ip, string mac);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAbsenceRegistration/GetAllUsersHistory", ReplyAction="http://tempuri.org/IAbsenceRegistration/GetAllUsersHistoryResponse")]
        AbsenceRegistration.ARservice.UserPresence[] GetAllUsersHistory();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAbsenceRegistration/GetAllUsersHistory", ReplyAction="http://tempuri.org/IAbsenceRegistration/GetAllUsersHistoryResponse")]
        System.Threading.Tasks.Task<AbsenceRegistration.ARservice.UserPresence[]> GetAllUsersHistoryAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAbsenceRegistration/GetUserPresent", ReplyAction="http://tempuri.org/IAbsenceRegistration/GetUserPresentResponse")]
        bool GetUserPresent(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAbsenceRegistration/GetUserPresent", ReplyAction="http://tempuri.org/IAbsenceRegistration/GetUserPresentResponse")]
        System.Threading.Tasks.Task<bool> GetUserPresentAsync(string email);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAbsenceRegistrationChannel : AbsenceRegistration.ARservice.IAbsenceRegistration, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AbsenceRegistrationClient : System.ServiceModel.ClientBase<AbsenceRegistration.ARservice.IAbsenceRegistration>, AbsenceRegistration.ARservice.IAbsenceRegistration {
        
        public AbsenceRegistrationClient() {
        }
        
        public AbsenceRegistrationClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AbsenceRegistrationClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AbsenceRegistrationClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AbsenceRegistrationClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool CreateUser(string email, string fisrtname, string surname, string password, string confirmPassword) {
            return base.Channel.CreateUser(email, fisrtname, surname, password, confirmPassword);
        }
        
        public System.Threading.Tasks.Task<bool> CreateUserAsync(string email, string fisrtname, string surname, string password, string confirmPassword) {
            return base.Channel.CreateUserAsync(email, fisrtname, surname, password, confirmPassword);
        }
        
        public bool LoginUser(string email, string password) {
            return base.Channel.LoginUser(email, password);
        }
        
        public System.Threading.Tasks.Task<bool> LoginUserAsync(string email, string password) {
            return base.Channel.LoginUserAsync(email, password);
        }
        
        public void CheckIn(string ip, string mac) {
            base.Channel.CheckIn(ip, mac);
        }
        
        public System.Threading.Tasks.Task CheckInAsync(string ip, string mac) {
            return base.Channel.CheckInAsync(ip, mac);
        }
        
        public AbsenceRegistration.ARservice.UserPresence[] GetAllUsersHistory() {
            return base.Channel.GetAllUsersHistory();
        }
        
        public System.Threading.Tasks.Task<AbsenceRegistration.ARservice.UserPresence[]> GetAllUsersHistoryAsync() {
            return base.Channel.GetAllUsersHistoryAsync();
        }
        
        public bool GetUserPresent(string email) {
            return base.Channel.GetUserPresent(email);
        }
        
        public System.Threading.Tasks.Task<bool> GetUserPresentAsync(string email) {
            return base.Channel.GetUserPresentAsync(email);
        }
    }
}
